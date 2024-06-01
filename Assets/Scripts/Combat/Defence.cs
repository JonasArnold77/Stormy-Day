using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defence : MonoBehaviour
{
    public Transform HipBoneTransform;
    public Transform PlayerTransform;

    public Animator _Animator;

    public Transform playerTransform; // Der Transform des Spielers

    public Coroutine DashCoroutine;

    public bool IsDoingDash;

    public bool IsInterupted;

    public bool InterruptAim;
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

        InterruptAim = false;
    }

    public IEnumerator Dash(float speed)
    {
        FindObjectOfType<ThirdPersonController>().MoveSpeed = 0f;
        FindObjectOfType<ThirdPersonController>().enabled = false;

        InterruptAim = true;


        // Calculate dash distance based on speed and time
        float dashDistance = speed * Time.deltaTime;

        // Save the current position
        Vector3 startPos = PlayerTransform.position;

        // Erstellen des Richtungsvektors
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction.Normalize();

        // Calculate target position based on forward direction and dash distance
        Vector3 targetPos = PlayerTransform.position + direction * 100;

        LookAt(targetPos);

        // Perform the dash
        while (PlayerTransform.position != targetPos)
        {
            if (IsInterupted)
            {
                yield break;
            }

            LookAt(targetPos);

            // Move towards the target position
            PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }


        // Ensure that the object ends exactly at the target position
        PlayerTransform.position = targetPos;
    }

    private void LookAt(Vector3 targetPoint)
    {
        // Calculate the direction from the player to the target point
        Vector3 directionToTarget = targetPoint - playerTransform.transform.position;

        // Keep the y-axis rotation constant
        directionToTarget.y = 0;

        // Rotate the player to look at the target point
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        playerTransform.transform.rotation = targetRotation;
    }
}
