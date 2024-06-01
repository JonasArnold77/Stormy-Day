using Magio;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum Direction
{
    Forward,
    Right,
    Left,
    Back
}


public class EnemyAnimation : MonoBehaviour
{
    public AnimationClip Hit;

    private Animator _Animator;

    public Vector3 StartPointOfHit;
    public Vector3 EndPointOfHit;

    public List<Rigidbody> NotAffectedBones = new List<Rigidbody>();

    private LineRenderer lineRenderer;

    private Coroutine WalkingCoroutine;

    bool running;
    public MagioObjectMaster MagioEffect;

    public Rigidbody _Rigidbody;
    private Vector3 previousPosition;

    public Transform bouncy;
    public Transform Hips;

    public NavMeshAgent Agent;

    private string lastAnimationTag;

    public ParticleSystem ScratchParticleSystem;

    public bool IsBoss = false;

    public List<string> AttackNames = new List<string>();

    private void Start()
    {
        _Animator = GetComponent<Animator>();
        _Animator.enabled = true;

        setRigidbodyState(true);

        SetIsTriggerOnAllBones(true);

        previousPosition = transform.position;

        Agent = GetComponent<NavMeshAgent>();

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        MagioEffect = GetComponentInChildren<MagioObjectMaster>();
        MagioEffect.magioObjects.ToList().ForEach(o => o.enabled = false);

        _Rigidbody = GetComponent<Rigidbody>();

        StartCoroutine(DoAttack());
    }

    //private void Update()
    //{
    //    var playerTransform = GameObject.FindGameObjectWithTag("Player").transform;//GetComponentInChildren<SkinnedMeshRenderer>().transform;


    //    lineRenderer.SetPosition(0, transform.position);
    //    lineRenderer.SetPosition(1, playerTransform.position);
    //}

    private void Update()
    {
        if (running)
        {
            bouncy.position = Hips.position;
        }

        if (_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Walking"))
        {
            Agent.speed = 3;
        }

        if (!_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            //GetComponent<EnemyAttack>().IsDoingAttack = false;
        }

        AnimatorClipInfo[] currentClipInfo = _Animator.GetCurrentAnimatorClipInfo(0);

        if (_Animator.GetCurrentAnimatorStateInfo(0).IsName("StandUp 1") &&
            _Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            GetComponent<FollowPalyer>().Begin();
        }


        if (Vector3.Distance(transform.position, FindObjectOfType<ThirdPersonController>().transform.position) > 4 && GetComponent<EnemyAttack>().IsDoingAttack == false) 
        {
            CheckIfWalking();
        }
    }

    public IEnumerator DoAttack()
    {
        var distance = Vector3.Distance(transform.position, FindObjectOfType<ThirdPersonController>().transform.position);
        var check = !FindObjectsOfType<EnemyAttack>().ToList().Any(ea => ea.IsDoingAttack);

        var enemiesss = FindObjectsOfType<EnemyAttack>().ToList();

        if (Vector3.Distance(transform.position, FindObjectOfType<ThirdPersonController>().transform.position) < 25 && !GetComponent<EnemyAttack>().IsDoingAttack /*&& !FindObjectsOfType<EnemyAttack>().ToList().Any(ea => ea.IsDoingAttack)*/)
        {
            GetComponent<EnemyAttack>().IsDoingAttack = true;
            yield return new WaitForSeconds(Random.Range(3f, 4f));
            _Animator.SetBool("Walk", false);

            var attackName = AttackNames[Random.Range(0, AttackNames.Count)];

            _Animator.SetBool(attackName, true);
        }
        yield return new WaitForEndOfFrame();

        StartCoroutine(DoAttack());
    }

    public IEnumerator WaitForEndOfAnimation(AttackItem item)
    {
        while (!AnimationFinished(item._Animation.name))
        {
            yield return null;
        }
    }

    bool AnimationFinished(string animationName)
    {
        // Prüfe, ob die angegebene Animation abgeschlossen ist
        AnimatorStateInfo stateInfo = _Animator.GetCurrentAnimatorStateInfo(1);
        return stateInfo.IsTag("Attack") && stateInfo.normalizedTime >= 1;
    }

    private void CheckIfWalking()
    {
        // Check if the object is moving by comparing its current position with the previous position
        if (transform.position != previousPosition)
        {
            _Animator.SetBool("Walk", true);
        }
        else
        {
            _Animator.SetBool("Walk", false);
        }

        // Update the previous position for the next frame
        previousPosition = transform.position;
    }

    private IEnumerator WaitForAnimation(string tag)
    {
        while (true)
        {
            string currentTag = GetAnimationTag(_Animator.GetCurrentAnimatorStateInfo(0).fullPathHash);
            if (currentTag != lastAnimationTag && currentTag != tag)
            {
                // Animation with a different tag than the specified one started
                
                lastAnimationTag = currentTag;
                yield break;
            }
            yield return null;
        }
    }

    private string GetAnimationTag(int fullPathHash)
    {
        //AnimatorStateInfo stateInfo = _Animator.GetCurrentAnimatorStateInfo(0);
        //foreach (AnimatorControllerParameter parameter in _Animator.parameters)
        //{
        //    if (Animator.StringToHash(parameter.name) == fullPathHash)
        //    {
        //        return parameter.name;
        //    }
        //}
        return "";
    }

    public void StartWalking()
    {
        WalkingCoroutine = StartCoroutine(WalkingRoutine());
        GetComponent<FollowPalyer>().Stop();
    }

    public void StopWalking()
    {
        StopCoroutine(WalkingCoroutine);
        
    }

    private IEnumerator WalkingRoutine()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position - transform.forward, 0.09f);
            yield return new WaitForEndOfFrame();
        }
    }

    void setRigidbodyState(bool state)
    {

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;

        }

        _Animator.enabled = state;

        //GetComponent<Rigidbody>().isKinematic = !state;

    }

    public void PlayingHitAnimation(EHitEffect effect)
    {
        if (!IsBoss)
        {
            if (effect == EHitEffect.HeadHit)
            {
                if (_Animator.GetBool("Hit") || _Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") || _Animator.GetCurrentAnimatorStateInfo(0).IsTag("Walking"))
                {
                    _Animator.SetBool("InterruptHit", true);
                }

                _Animator.SetBool("Hit", true);
            }
            else if (effect == EHitEffect.Push)
            {
                _Animator.SetBool("Push", true);
            }

        }
    }

    public void ActivateEffect(EStatusEffects effect)
    {
        if (effect == EStatusEffects.Fire)
        {
            StartCoroutine(EffectCoroutine(0));
        }
        else if (effect == EStatusEffects.Ice)
        {
            StartCoroutine(EffectCoroutine(1));
        }
    }

    public IEnumerator EffectCoroutine(int index)
    {
        MagioEffect.magioObjects[index].enabled = true;

        yield return new WaitForSeconds(4);

        MagioEffect.magioObjects[index].enabled = false;

    }

    public void SetIsTriggerOnAllBones(bool value) 
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            if (NotAffectedBones.Contains(rigidbody) || rigidbody.Equals(GetComponent<Rigidbody>()))
            {
                rigidbody.GetComponent<Collider>().isTrigger = false;
            }
            else
            {
                rigidbody.GetComponent<Collider>().isTrigger = value;
                //rigidbody.AddForce(-transform.up * 75, ForceMode.Impulse);
                //rigidbody.AddTorque(new Vector3(1, 1, 1) * 1000, ForceMode.Impulse);

            }
        }
    }

    public IEnumerator Dash(float distance)
    {
        float distanceTraveled = 0f;

        var direction = transform.position - FindObjectOfType<ThirdPersonController>().transform.position ;

        direction = direction.normalized;

        while (distanceTraveled < distance)
        {
            // Bewege das Objekt in der gegebenen Richtung mit der gegebenen Geschwindigkeit
            transform.Translate(direction * 10f * Time.deltaTime, Space.World);

            // Aktualisiere die zurückgelegte Distanz
            distanceTraveled += 10f * Time.deltaTime;

            yield return null;
        }
    }

    public void SetAnimatorSpeed(float speed)
    {
        _Animator.speed = speed;
    }

    public void ResetAnimatorSpeed()
    {
        _Animator.speed = 1;
    }

    public IEnumerator KillExecution()
    {
        yield return new WaitForSeconds(0.05f);

        var playerTransform = GameObject.FindGameObjectWithTag("Player").transform;//.GetComponentInChildren<SkinnedMeshRenderer>().transform;

        var hitDirection =  transform.position - playerTransform.position;

        running = true;

        hitDirection = new Vector3(hitDirection.x, hitDirection.y, hitDirection.z);

        setRigidbodyState(false);
        var mainRigidBody = GetComponent<Rigidbody>();
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        var x = GetComponent<Rigidbody>();
        x.isKinematic = false;

        GetComponentsInChildren<Transform>().ToList().ForEach(co => co.tag = "Dead");

        SetIsTriggerOnAllBones(false);

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            

            if(NotAffectedBones.Contains(rigidbody))
            {
                rigidbody.isKinematic = true;
            }
            else
            {
                if(GetComponent<NavMeshAgent>() != null)
                {
                    GetComponent<NavMeshAgent>().enabled = false;
                }
                rigidbody.isKinematic = false;
                //rigidbody.AddForce(-transform.up * 75, ForceMode.Impulse);
                rigidbody.AddForce(hitDirection.normalized * 30, ForceMode.Impulse);
                //rigidbody.AddTorque(new Vector3(1, 1, 1) * 1000, ForceMode.Impulse);

            }

            
        }
        ////x.AddForce(-transform.up * 30000, ForceMode.Impulse);
        //GetComponent<Rigidbody>().AddTorque((transform.position + hitDirection).normalized * 100, ForceMode.Force);
    }

    public void GetHitDirection(Vector3 hitVector)
    {
        hitVector = new Vector3(hitVector.x, hitVector.y, 0);

        var pAngle = Vector3.Angle(Attack.Instance.gameObject.transform.position - hitVector, transform.position);

        var angle = Vector3.Angle(hitVector, transform.forward);
    }

   
}
