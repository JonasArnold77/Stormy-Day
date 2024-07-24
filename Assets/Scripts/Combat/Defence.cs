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

    public ParticleSystem ScratchParticleSystem;

    public bool FixHeight;

    private float desiredDistance = 1f; // Desired distance from the ground
    public LayerMask groundLayer;      // Layer of the ground


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

        //if (FixHeight)
        //{
        //    KeepDistance();
        //}
    }

    public bool CheckIfDash(string tag, int layer = 1)
    {
        AnimatorStateInfo animState = _Animator.GetCurrentAnimatorStateInfo(layer);
        if (animState.IsTag(tag))
        {
            //if (animState.normalizedTime > animState.length * 1f)
            //{
            //    FixHeight = false;
            //    SetOnGround();
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

    public void SetOnGround()
    {
        RaycastHit hit;

        // Cast a ray downwards from the player's position
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 3, groundLayer) || Physics.Raycast(transform.position, Vector3.up, out hit, 3, groundLayer))
        {
            // Set the player's position to the ground level
            Vector3 targetPosition = hit.point;
            transform.position = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
        }
    }

    public void KeepDistance()
    {
        RaycastHit hit;

        // Cast a ray downwards from the object's position
        if (Physics.Raycast(playerTransform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            // Calculate the current distance from the ground
            float currentDistance = hit.distance;

            // Calculate the difference needed to maintain the desired distance
            float distanceDifference = desiredDistance - currentDistance;

            // Adjust the object's position
            playerTransform.position += Vector3.up * distanceDifference;
        }
    }

    public void SetDashTrue() 
    {
        IsDoingDash = true;
        FixHeight = true;
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

        WeaponManager.Instance.GetWeaponComponent(EHitType.Sword).GetComponent<Collider>().enabled = false;

        InterruptAim = false;
    }

    public IEnumerator Dash(float speed)
    {
        var controller = FindObjectOfType<ThirdPersonController>();
        controller.MoveSpeed = 0f;
        controller.enabled = false;

        InterruptAim = true;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        RaycastHit hit;
        float dashDistance = 100f; // Fixed dash distance
        Vector3 targetPos = PlayerTransform.position + direction * dashDistance;

        // Perform a raycast to detect obstacles
        if (Physics.Raycast(PlayerTransform.position, direction, out hit, dashDistance))
        {
            targetPos = hit.point; // Set target position to hit point
        }

        LookAt(targetPos);

        while (Vector3.Distance(PlayerTransform.position, targetPos) > 0.1f)
        {
            if (IsInterupted)
            {
                yield break;
            }

            PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }

        WeaponManager.Instance.GetWeaponComponent(EHitType.Sword).GetComponent<Collider>().enabled = false;

        PlayerTransform.position = targetPos;
        controller.enabled = true;
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
