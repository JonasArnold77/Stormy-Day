using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator _Animator;
    private bool IsInAttackingTimeWindow;
    private bool IsInComboTimeWindow;
    private bool FirstHit;

    private bool _IsPlayingAttack;

    private Coroutine _WaitForResettingCoroutineÍnstance;
    private Coroutine _AttackIsHappenCoroutineInstance;

    private bool _CheckForComboTimeWindowCorutineIsRunning;
    private bool _WaitForResettingCoroutineIsActive;
    private bool _ComboCoroutineIsRunning;

    private Vector3 StartPoint;
    public Vector3 EndPoint;
    public bool HitDone;

    public Transform playerTransform; // Der Transform des Spielers
    public float detectionDistance = 10f; // Die Entfernung, in der nach Gegnern gesucht wird
    public LayerMask enemyLayerMask; // Die LayerMask für Gegner

    public bool isDoingAttack;
    public Transform ActualEnemy;
    public Transform Player;

    private Coroutine WalkingCoroutine;

    public EHitEffect actualEffect;

    public float DashDistance;


    [Serializable]
    public struct TypeAndEffect
    {
        public EHitEffect effect;
        public EHitType type;
    }

    public static Attack Instance;


    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //InventoryManager.Instance.ActualWeapon.GetComponent<Collider>().enabled = false;
        _Animator = GetComponent<Animator>();


    }

    private void Update()
    {
        if (!_ComboCoroutineIsRunning)
        {
            StartCoroutine(CalculateCombo());
        }

        if (isDoingAttack && ActualEnemy != null)
        {
            LookAt(ActualEnemy);
        }

        GetEnemiesInFieldOfView();
    }

    public void DashForwardWhileAttack(float duration)
    {
        StartCoroutine(Dash(duration));
    }

    IEnumerator Dash(float distance)
    {
        float distanceTraveled = 0f;

        Vector3 direction = new Vector3();

        if(ActualEnemy != null)
        {
            direction = ActualEnemy.position - playerTransform.position;

            if(Vector3.Distance(ActualEnemy.position, playerTransform.position) < DashDistance)
            {
                //yield break;
            }
        }
        else
        {
            direction = playerTransform.forward;
        }

        direction = direction.normalized;

        while (distanceTraveled < DashDistance)
        {
            // Bewege das Objekt in der gegebenen Richtung mit der gegebenen Geschwindigkeit
            playerTransform.Translate(direction * 10f * Time.deltaTime, Space.World);

            // Aktualisiere die zurückgelegte Distanz
            distanceTraveled += 10f * Time.deltaTime;

            yield return null;
        }
    }

    public IEnumerator AimOnNextEnemy()
    {
        var allEnemies = FindObjectsOfType<EnemyAnimation>().Select(e => e.transform).Where(e => Vector3.Distance(playerTransform.position,e.position)<5).ToList();
        Transform pivotEnemy = allEnemies.FirstOrDefault();

        if (GetEnemiesInFieldOfView().Count > 0 && allEnemies.Count>0)
        {
            foreach (var e in GetEnemiesInFieldOfView())
            {
                if (Vector3.Distance(transform.position, e.position) < Vector3.Distance(transform.position, pivotEnemy.position))
                {
                    pivotEnemy = e;
                }
            }

            //FindObjectOfType<ThirdPersonController>().enabled = false;

            isDoingAttack = true;

            ActualEnemy = pivotEnemy;

            while (CheckIfPossibleToAttack() == true)
            {
                //LookAt(pivotEnemy);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(1f);

            isDoingAttack = false;

            //FindObjectOfType<ThirdPersonController>().enabled = true;
        }
        else if(allEnemies.Count > 0)
        {
            foreach (var e in allEnemies)
            {
                if (Vector3.Distance(transform.position, e.position) < Vector3.Distance(transform.position, pivotEnemy.position))
                {
                    pivotEnemy = e;
                }
            }

            //FindObjectOfType<ThirdPersonController>().enabled = false;

            isDoingAttack = true;

            ActualEnemy = pivotEnemy;

            while (CheckIfPossibleToAttack() == true)
            {
                //LookAt(pivotEnemy);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(1f);

            isDoingAttack = false;

            //FindObjectOfType<ThirdPersonController>().enabled = true;
        }
    }

    List<Transform> GetEnemiesInFieldOfView()
    {
        List<Transform> enemiesInFieldOfView = new List<Transform>();

        // Alle Collider im DetectionDistance Bereich abrufen
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionDistance, enemyLayerMask);

        foreach (var collider in colliders)
        {
            // Richtungsvektor vom Spieler zum Ziel (Gegner)
            Vector3 directionToEnemy = (collider.transform.position - transform.position).normalized;

            // Blickrichtung des Spielers
            Vector3 forwardDirection = playerTransform.forward;

            // Winkel zwischen Blickrichtung des Spielers und Richtungsvektor zum Ziel
            float angleToEnemy = Vector3.Angle(forwardDirection, directionToEnemy);

            // Prüfen, ob der Winkel im zulässigen Bereich liegt (-45 Grad nach links und 45 Grad nach rechts)
            if (angleToEnemy <= 45f && angleToEnemy >= -45f)
            {
                enemiesInFieldOfView.Add(collider.transform);
            }
        }

        if (enemiesInFieldOfView.Count > 0)
        {
            Debug.Log("Is In Sight");
            //LookAt(enemiesInFieldOfView.FirstOrDefault());
        }

        return enemiesInFieldOfView;
    }

    private void LookAt(Transform targetPoint)
    {
        // Calculate the direction from the player to the target point
        Vector3 directionToTarget = targetPoint.position - playerTransform.transform.position;

        // Keep the y-axis rotation constant
        directionToTarget.y = 0;

        // Rotate the player to look at the target point
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        playerTransform.transform.rotation = targetRotation;
    }

    public IEnumerator CalculateCombo()
    {
        _ComboCoroutineIsRunning = true;
        if (Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.LightHit)) && !CheckIfPossibleToAttack())
        {
            ComboManager.Instance.ResetAllComboCountersInsteadOfSelected(EControls.LightHit);
            if (_WaitForResettingCoroutineIsActive)
            {
                StopCoroutine(_WaitForResettingCoroutineÍnstance);
            }

            //have to be before attack coroutine
            _WaitForResettingCoroutineÍnstance = StartCoroutine(WaitForResettingCoroutine(EControls.LightHit));
            yield return StartCoroutine(PlayerAnimation.Instance.PlayNextAttack(EControls.LightHit));

            StartCoroutine(AimOnNextEnemy());

            ComboManager.Instance.CheckForSuperCombo(EControls.LightHit);
        }

        if (Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.HardHit)) && !CheckIfPossibleToAttack())
        {
            ComboManager.Instance.ResetAllComboCountersInsteadOfSelected(EControls.HardHit);
            if (_WaitForResettingCoroutineIsActive)
            {
                StopCoroutine(_WaitForResettingCoroutineÍnstance);
            }
            
            //have to be before attack coroutine
            _WaitForResettingCoroutineÍnstance = StartCoroutine(WaitForResettingCoroutine(EControls.HardHit));
            yield return StartCoroutine(PlayerAnimation.Instance.PlayNextAttack(EControls.HardHit));

            StartCoroutine(AimOnNextEnemy());

            ComboManager.Instance.CheckForSuperCombo(EControls.HardHit);
        }
        _ComboCoroutineIsRunning = false;
    }

    private IEnumerator WaitForResettingCoroutine(EControls control)
    {
        _WaitForResettingCoroutineIsActive = true;
        yield return new WaitForSeconds(2f);
        ComboManager.Instance.Combos.Where(c => c.InputType == control).FirstOrDefault().ResetCounter();
        InputManager.Instance.CollectingInputsList.Clear();
        _WaitForResettingCoroutineIsActive = false;
    }

    public void SetEffect(EHitEffect effect)
    {
        actualEffect = effect;
    }

    public void StartWalking()
    {
        WalkingCoroutine = StartCoroutine(WalkingRoutine());
    }

    public void StopWalking()
    {
        StopCoroutine(WalkingCoroutine);
    }

    private IEnumerator WalkingRoutine()
    {
        if (ActualEnemy != null)
        {
            while (true)
            {
                playerTransform.position = Vector3.Lerp(playerTransform.position, ActualEnemy.position, 0.04f);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (true)
            {
                playerTransform.position = Vector3.Lerp(playerTransform.position, playerTransform.position + transform.forward, 0.04f);
                yield return new WaitForEndOfFrame();
            }
        }
        
    }

    public void SetColliderActive(EHitType type)
    {
        //WeaponManager.Instance.GetWeaponComponent(typeandEffect.type).GetComponent<Sword>().hitEffect = typeandEffect.effect;
        WeaponManager.Instance.GetWeaponComponent(type).GetComponent<Collider>().enabled = true;
        IsInAttackingTimeWindow = true;
    }

    public void SetColliderInactive(EHitType type)
    {
        WeaponManager.Instance.GetWeaponComponent(type).GetComponent<Collider>().enabled = false;
        IsInAttackingTimeWindow = false;
    }

    //public Vector3 GetSwordDirection(EnemyAnimation enemyAnimation)
    //{
    //    var startPointFlat = new Vector3(StartPoint.x, StartPoint.y, 0);
    //    var endPointFlat = new Vector3(EndPoint.x, EndPoint.y, 0);
    //    Vector3 direction = endPointFlat - startPointFlat;
    //    direction = direction.normalized;

    //    //Decide between up and down and left and right
    //    if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
    //    {
    //        //Decide between Left and Right
    //        if(direction.x > 0)
    //        {
    //            Debug.Log("Hit to right");
    //            enemyAnimation.GetHitDirection(new Vector3(1, 0, 0));
    //            return new Vector3(1,0,0);
    //        }
    //        else
    //        {
    //            Debug.Log("Hit to left");
    //            enemyAnimation.GetHitDirection(new Vector3(-1, 0, 0));
    //            return new Vector3(-1, 0, 0);
    //        }
    //    }
    //    else
    //    {
    //        if (direction.y > 0)
    //        {
    //            Debug.Log("Hit to Up");
    //            enemyAnimation.GetHitDirection(new Vector3(0, 1, 0));
    //            return new Vector3(0, 1, 0);
    //        }
    //        else
    //            Debug.Log("Hit to Down");
    //            enemyAnimation.GetHitDirection(new Vector3(0, -1, 0));
    //            return new Vector3(0, -1, 0);
    //        }
    //    }

    private IEnumerator CheckForComboTimeWindow()
    {
        if (!_CheckForComboTimeWindowCorutineIsRunning)
        {
            _CheckForComboTimeWindowCorutineIsRunning = true;

            //if I hit before the combo countr should be resetted
            //yield return new WaitWhile

            yield return new WaitUntil(() => GetCurrentAnimatorTime() >= GetDurationOfAnimatorClip() / 2);
            IsInComboTimeWindow = true;
            yield return new WaitUntil(() => GetCurrentAnimatorTime()>=GetDurationOfAnimatorClip());

            yield return new WaitForSeconds(0.5f);
            IsInComboTimeWindow = false;
            ComboManager.Instance.ResetComboCounter();
            FirstHit = false;
            _CheckForComboTimeWindowCorutineIsRunning = false;
        }
    }

    public bool CheckIfPossibleToAttack(int layer = 1)
    {
        AnimatorStateInfo animState = _Animator.GetCurrentAnimatorStateInfo(layer);
        if (animState.IsTag("Attack"))
        {
            if(animState.normalizedTime > animState.length * 0.0f)
            {
                _IsPlayingAttack = false;
                return false;
            }

            _IsPlayingAttack = true;
            return true;
        }
        else
        {
            _IsPlayingAttack = false;
            return false;
        }
    }

    public float GetCurrentAnimatorTime(int layer = 1)
    {
        AnimatorStateInfo animState = _Animator.GetCurrentAnimatorStateInfo(layer);
        float currentTime = animState.normalizedTime;
        Debug.Log("Time: " + animState.normalizedTime);
        return currentTime;
    }

    public float GetDurationOfAnimatorClip(int layer = 1)
    {
        AnimatorStateInfo animState = _Animator.GetCurrentAnimatorStateInfo(layer);
        float duration = animState.length;
        return duration;
    }
}
