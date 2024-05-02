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

    public Transform bouncy;
    public Transform Hips;

    private void Start()
    {
        _Animator = GetComponent<Animator>();
        _Animator.enabled = true;

        setRigidbodyState(true);

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        MagioEffect = GetComponentInChildren<MagioObjectMaster>();
        MagioEffect.magioObjects.ToList().ForEach(o => o.enabled = false);
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

        AnimatorClipInfo[] currentClipInfo = _Animator.GetCurrentAnimatorClipInfo(0);

        if (_Animator.GetCurrentAnimatorStateInfo(0).IsName("StandUp 1") &&
            _Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            GetComponent<FollowPalyer>().Begin();
        }
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
        if(effect == EHitEffect.HeadHit)
        {
            if (_Animator.GetBool("Hit"))
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

    public void ActivateEffect(EStatusEffects effect)
    {
        if (effect == EStatusEffects.Fire)
        {
            StartCoroutine(EffectCoroutine(1));
        }
        else if (effect == EStatusEffects.Ice)
        {
            StartCoroutine(EffectCoroutine(0));
        }
    }

    public IEnumerator EffectCoroutine(int index)
    {
        MagioEffect.magioObjects[index].enabled = true;

        yield return new WaitForSeconds(4);

        MagioEffect.magioObjects[index].enabled = false;

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
