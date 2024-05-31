using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defence : MonoBehaviour
{
    public Transform HipBoneTransform;
    public Transform PlayerTransform;

    public Animator _Animator;

    public Coroutine DashCoroutine;

    public bool IsDoingDash;

    public bool IsInterupted;

    private void Start()
    {
        _Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Dash)))
        {
            _Animator.Play(ComboManager.Instance.DashClip.name);
            //MoveToLastFramePosition(ComboManager.Instance.DashClip.name);
        }

        CheckIfDash("Dash");
    }

    public bool CheckIfDash(string tag, int layer = 1)
    {
        AnimatorStateInfo animState = _Animator.GetCurrentAnimatorStateInfo(layer);
        if (animState.IsTag(tag))
        {
            //if (animState.normalizedTime > animState.length * 0.9f)
            //{
            //    _IsPlayingAttack = false;
            //    return false;
            //}
            IsDoingDash = true;
            IsInterupted = false;
            return true;
        }
        else if(!IsInterupted)
        {
            IsInterupted = true;
            IsDoingDash = false;
            StopDash();
            return false;
        }
        else
        {
            return false;
        }
    }



    public void SetDashTrue() 
    {
        IsDoingDash = true;
    }

    public void SetDashFalse()
    {
        IsDoingDash = false;
    }

    public void InitDash(float speed)
    {
        DashCoroutine = StartCoroutine(Dash(speed));
    }

    public void StopDash()
    {
        StopCoroutine(DashCoroutine);
        FindObjectOfType<ThirdPersonController>().MoveSpeed = 6f;
        FindObjectOfType<ThirdPersonController>().enabled = true;
    }

    public IEnumerator Dash(float speed)
    {
        FindObjectOfType<ThirdPersonController>().MoveSpeed = 0f;
        FindObjectOfType<ThirdPersonController>().enabled = false;

        // Calculate dash distance based on speed and time
        float dashDistance = speed * Time.deltaTime;

        // Save the current position
        Vector3 startPos = PlayerTransform.position;

        // Calculate target position based on forward direction and dash distance
        Vector3 targetPos = PlayerTransform.position + PlayerTransform.forward *100;

        // Perform the dash
        while (PlayerTransform.position != targetPos)
        {
            if (IsInterupted)
            {
                yield break;
            }
            // Move towards the target position
            PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }

        // Ensure that the object ends exactly at the target position
        PlayerTransform.position = targetPos;
    }
}
