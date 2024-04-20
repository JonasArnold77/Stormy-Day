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

    bool running;

    public Transform bouncy;
    public Transform Hips;

    private void Start()
    {
        _Animator = GetComponent<Animator>();
        _Animator.enabled = true;

        setRigidbodyState(true);

        lineRenderer = gameObject.AddComponent<LineRenderer>();


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

    public void PlayingHitAnimation()
    {
       if (_Animator.GetBool("Hit"))
        {
            _Animator.SetBool("InterruptHit", true);
        }

        _Animator.SetBool("Hit", true);

        
       
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
