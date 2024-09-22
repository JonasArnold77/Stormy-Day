using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator _Animator;
    private bool _IsPlayingAttack = true;

    private Coroutine _IsPlayingAttackCoroutine;

    public AttackItem ActualAttackItem;

    public static PlayerAnimation Instance;

    public AudioSource _AudioSource;

    public Transform PlayerTransform;

    public AudioClip Running;
    public AudioClip RunningInWater;

    public bool StartRunning;
    public bool IsInWater;

    private void Awake()
    {
        Instance = this;
        _AudioSource.loop = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();
        _AudioSource.clip = Running;
    }

   

    // Update is called once per frame
    void Update()
    {
        if(WeaponManager.Instance.ActualWeaponType == EWeaponType.OneHanded)
        {
            _Animator.SetBool("PistolRun", false);

            if (Input.GetAxis("Horizontal") > 0.01f || Input.GetAxis("Horizontal") < -0.01f || Input.GetAxis("Vertical") > 0.01f || Input.GetAxis("Vertical") < -0.01f)
            {
                _Animator.SetBool("Run", true);

                if (!StartRunning)
                {
                    _AudioSource.Stop();
                    StartRunning = true;

                    _AudioSource.Play();
                }


            }
            else
            {
                if (StartRunning)
                {
                    StartRunning = false;
                    _AudioSource.Stop();
                }
                _Animator.SetBool("Run", false);
            }
        }
        else if (WeaponManager.Instance.ActualWeaponType == EWeaponType.Pistol)
        {
            LookAtMousePosition();

            _Animator.SetBool("Run", false);

            if (Input.GetAxis("Horizontal") > 0.01f || Input.GetAxis("Horizontal") < -0.01f || Input.GetAxis("Vertical") > 0.01f || Input.GetAxis("Vertical") < -0.01f)
            {
                _Animator.SetBool("PistolRun", true);

                if (!StartRunning)
                {
                    _AudioSource.Stop();
                    StartRunning = true;

                    _AudioSource.Play();
                }


            }
            else
            {
                if (StartRunning)
                {
                    StartRunning = false;
                    _AudioSource.Stop();
                }
                _Animator.SetBool("PistolRun", false);
            }
        }
        

        CheckIfAttackIsPlaying();
        StopPlayerWhileDoingAttack();

    }

    public bool IsAnimationPlaying(string animationName)
    {
        // Prüft, ob der Animator und der AnimatorStateInfo gültig sind
        if (_Animator == null) return false;

        // Holt den aktuellen AnimatorStateInfo aus dem Base Layer (Layer 0)
        AnimatorStateInfo stateInfo = _Animator.GetCurrentAnimatorStateInfo(0);

        // Überprüft, ob der Name der aktuellen Animation mit dem angegebenen Namen übereinstimmt
        return stateInfo.IsName(animationName);
    }

    public string GetMovementDirection()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            if (horizontal > 0)
            {
                return "rechts";
            }
            else if (horizontal < 0)
            {
                return "links";
            }
        }
        else
        {
            if (vertical > 0)
            {
                return "vorwärts";
            }
            else if (vertical < 0)
            {
                return "rückwärts";
            }
        }

        return "keine Bewegung";
    }

    public void LookAtMousePosition()
    {
        // 1. Hole die Mausposition in der Welt
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(PlayerTransform.position).z; // Tiefe setzen
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        var flatMousePosition = new Vector3(worldMousePosition.x, PlayerTransform.position.y, worldMousePosition.z);

        // 2. Lass das Objekt zur Maus schauen
        PlayerTransform.LookAt(flatMousePosition);

        // 3. Optional: Wenn du möchtest, dass das Objekt sich nur um eine Achse dreht (z.B. nur um die Y-Achse)
        //PlayerTransform.rotation = Quaternion.Euler(0, PlayerTransform.eulerAngles.y, 0);
    }

    public IEnumerator PlayNextAttack(EControls control)
    {
        if(WeaponManager.Instance.ActualWeaponType == EWeaponType.Pistol)
        {
            yield break;
        }

        if(UIManager.Instance._ComboPanel.isActiveAndEnabled == true || UIManager.Instance._AttackPoolMenu.isActiveAndEnabled)
        {
            yield break;
        }
        //if (CheckIfAttackIsPlaying())
        //{
            yield return new WaitUntil(() => !_IsPlayingAttack);
        //}
        var combo = ComboManager.Instance.Combos.Where(c => c.InputType == control && c.WeaponType == WeaponManager.Instance.ActualWeaponType).FirstOrDefault();
        if (combo.ComboList != null && combo.ComboList.Count > 0)
        {
            _Animator.Play(combo.ComboList[combo.Counter]._Animation.name);     

            StartCoroutine(WaitForEndOfAnimation(combo.ComboList[combo.Counter]));

            combo.IncreaseCounter();
        }
    }

    public void PlayHitAnimation()
    {
        _Animator.Play("Hit 2");
    }

    public IEnumerator WaitForEndOfAnimation(AttackItem item)
    {
        var lastEffect = 
        ActualAttackItem = item;

        GetComponent<PlayerStatus>().ChangeEndurance(-item.EnduranceCost);

        //GetComponent<StatusEffects>().ActivateEffect(item.StatusEffect, item.Type);
        
        while (!AnimationFinished(item._Animation.name))
        {
            yield return null;
        }
        GetComponent<StatusEffects>().DeactivateEffect();
        ActualAttackItem = null;
    }

    bool AnimationFinished(string animationName)
    {
        // Prüfe, ob die angegebene Animation abgeschlossen ist
        AnimatorStateInfo stateInfo = _Animator.GetCurrentAnimatorStateInfo(1);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1;
    }

    //Wait for either animation ended or animation is at 70 percent
    public bool CheckIfAttackIsPlaying(int layer = 1)
    {
        AnimatorStateInfo animState = _Animator.GetCurrentAnimatorStateInfo(layer);
        if (animState.IsTag("Attack"))
        {
            //if (animState.normalizedTime > animState.length * 0.9f)
            //{
            //    _IsPlayingAttack = false;
            //    return false;
            //}
            _IsPlayingAttack = true;
            return true;
        }
        else
        {
            _IsPlayingAttack = false;
            return false;
        }
    }

    public void StopPlayerWhileDoingAttack()
    {
        // Überprüfe, ob die gewünschte Animation abgespielt wird
        if (_Animator.GetCurrentAnimatorStateInfo(1).IsTag("Attack"))
        {
            FindObjectOfType<ThirdPersonController>().MoveSpeed = 0;
            // Wenn die Animation abgespielt wird, überprüfe, ob sie abgeschlossen ist
            if (_Animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.7f)
            {
                FindObjectOfType<ThirdPersonController>().MoveSpeed = 6;

                // Animation ist abgeschlossen
                Debug.Log("Zielanimation abgeschlossen!");
            }
        }
    }

    
}
