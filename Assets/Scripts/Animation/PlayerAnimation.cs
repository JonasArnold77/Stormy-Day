using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator _Animator;
    private bool _IsPlayingAttack;

    private Coroutine _IsPlayingAttackCoroutine;

    public static PlayerAnimation Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") > 0.01f || Input.GetAxis("Horizontal") < -0.01f || Input.GetAxis("Vertical") > 0.01f || Input.GetAxis("Vertical") < -0.01f)
        {
            _Animator.SetBool("Run", true);
        }
        else
        {
            _Animator.SetBool("Run", false);
        }

        CheckIfAttackIsPlaying();
    }

    public IEnumerator PlayNextAttack(EControls control)
    {
        if (CheckIfAttackIsPlaying())
        {
            yield return new WaitUntil(() => !_IsPlayingAttack);
        }
        var combo = ComboManager.Instance.Combos.Where(c => c.InputType == control && c.WeaponType == WeaponManager.Instance.ActualWeaponType).FirstOrDefault();
        _Animator.Play(combo.ComboList[combo.Counter].name);
        combo.IncreaseCounter();
    }
    public bool CheckIfAttackIsPlaying(int layer = 1)
    {
        AnimatorStateInfo animState = _Animator.GetCurrentAnimatorStateInfo(layer);
        if (animState.IsTag("Attack"))
        {
            _IsPlayingAttack = true;
            return true;
        }
        else
        {
            _IsPlayingAttack = false;
            return false;
        }
    }
}
