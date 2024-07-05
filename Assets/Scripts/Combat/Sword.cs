using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public EHitEffect hitEffect;
    public Transform TipOfTheSword;
    private Vector3 LastPositionOfSword;

    public Rigidbody TipOfTheSwordRigidbody;

    public GameObject FireEffect;
    public GameObject IceEffect;

    public Vector3 velocity;
    public Vector3 previous;

    public GameObject BloodEffectObject;

    public AudioSource _AudioSource;

    public List<AudioClip> ÁudioClips = new List<AudioClip>();

    public int Damage;
    

    private HitDirection ActualDirection;

    private void Start()
    {
        //TipOfTheSword = gameObject.transform.GetChild(0);
        BloodEffectObject.SetActive(true);
        BloodEffectObject.GetComponentInChildren<TrailRenderer>().emitting = false;
        _AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //velocity = TipOfTheSword.position - previous;
        //previous = TipOfTheSword.position;

        //Debug.Log("Velo: " + velocity);

        
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {

    //        other.GetComponent<EnemyAnimation>().PlayingHitAnimation();
    //        other.GetComponent<EnemyAnimation>().StartPointOfHit = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        //other.GetComponent<EnemyAnimation>().EndPointOfHit = o;
    //        other.GetComponent<EnemyAnimation>().GetHitDirection(other.transform.forward);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //hitStartPosition = WeaponManager.Instance.ActualWeapon.TipOfTheSword.position;
            collision.gameObject.GetComponent<EnemyAnimation>().StartPointOfHit = collision.contacts[0].point;
            collision.gameObject.GetComponent<EnemyAnimation>().PlayingHitAnimation(FindObjectOfType<Attack>().actualEffect);
            StartCoroutine(collision.gameObject.GetComponent<EnemyAnimation>().Dash(2));
            StartCoroutine(collision.gameObject.GetComponent<FollowPalyer>().StopForTime(0.5f));

            //BloodEffectObject.SetActive(true);

            BloodEffectObject = Instantiate(PrefabManager.Instance.DarkBloodEffect, position: collision.contacts[0].point, Quaternion.identity);

            if(ComboManager.Instance.Combos.FirstOrDefault().Counter == 0)
            {
                FindObjectOfType<PlayerStatus>().ChangeMana(50);
            }
            else if (ComboManager.Instance.Combos.FirstOrDefault().Counter == 1)
            {
                FindObjectOfType<PlayerStatus>().ChangeMana(75);
            }
            else if (ComboManager.Instance.Combos.FirstOrDefault().Counter == 2)
            {
                FindObjectOfType<PlayerStatus>().ChangeMana(100);
            }
            else if (ComboManager.Instance.Combos.FirstOrDefault().Counter == 3)
            {
                FindObjectOfType<PlayerStatus>().ChangeMana(125);
            }
            else if (ComboManager.Instance.Combos.FirstOrDefault().Counter == 4)
            {
                FindObjectOfType<PlayerStatus>().ChangeMana(150);
            }

            //StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(0.2f,0.3f));


            //_AudioSource.pitch = Random.Range(0.9f, 1.1f);
            //_AudioSource.Stop();
            //_AudioSource.Play();

            PlayRandomSound();

            collision.gameObject.GetComponent<EnemyHealth>().Health -= Damage;

            //if(collision.gameObject.GetComponent<EnemyHealth>().Health <= 0)
            //{
            //    StartCoroutine(collision.gameObject.GetComponent<EnemyAnimation>().KillExecution());
            //    collision.gameObject.GetComponent<EnemyAnimation>().IsDead = true;
            //}

            collision.gameObject.GetComponent<EnemyAnimation>().ActivateEffect(FindObjectOfType<StatusEffects>().ActualHitEffect);

            collision.gameObject.GetComponent<EnemyAnimation>().ScratchParticleSystem.Play();

            //StartCoroutine(StopTimeCoroutine(0.1f));

            //StartCoroutine(DoBloodEffectCoroutine(collision.transform));

            //StartCoroutine(MoveObject(BloodEffectObject.transform, GetDirectionVector(FindObjectOfType<ThirdPersonController>().transform, collision.transform, FindObjectOfType<Attack>().ActualHitDirection), 8f, collision.transform));


            //StartCoroutine(MoveObject(-velocity));

            Debug.Log("Status Effetk:" + FindObjectOfType<PlayerAnimation>().ActualAttackItem.StatusEffect.ToString());

            //hitStartPosition = collision.contacts[0].point;

    //StartCoroutine(collision.gameObject.GetComponent<EnemyAnimation>().KillExecution());
        } 
    }

    public void PlayRandomSound()
    {
        _AudioSource.Stop();
        _AudioSource.clip = ÁudioClips[Random.Range(0, ÁudioClips.Count)];
        _AudioSource.Play();
    }

    private IEnumerator StopTimeCoroutine(float duration)
    {
        // Set time scale to 0 to stop time
        Time.timeScale = 0f;

        // Wait for the given duration (scaled by Time.unscaledDeltaTime)
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        // Restore time scale to 1 to resume time
        Time.timeScale = 1f;
    }

    public Vector3 GetDirectionVector(Transform playerPos, Transform enemyPos, HitDirection direction)
    {
        Vector3 directionVector = Vector3.zero;

        switch (direction)
        {
            case HitDirection.RightToLeft:
                directionVector = enemyPos.position - playerPos.position;
                directionVector = Quaternion.Euler(0, Random.Range(-105,-75), 0) * directionVector.normalized;
                break;
            case HitDirection.LeftToRight:
                directionVector = enemyPos.position - playerPos.position;
                directionVector = Quaternion.Euler(0, 90, 0) * directionVector.normalized;
                break;
            case HitDirection.TopToBottom:
                var playerToEnemy = enemyPos.position - playerPos.position;
                directionVector = Vector3.Cross(playerToEnemy, Vector3.up);
                break;
            case HitDirection.BottomToTop:
                directionVector = new Vector3(0, enemyPos.position.y - playerPos.position.y, 0);
                break;
            default:
                Debug.LogError("Ungültige Angriffsrichtung!");
                break;
        }

        directionVector.Normalize(); // Richtungsvektor normalisieren, um nur die Richtung zu erhalten
        return directionVector;
    }

    public IEnumerator DoBloodEffectCoroutine(Transform enemy)
    {
        //BloodEffectObject.GetComponentInChildren<TrailRenderer>().endWidth = 1;
        //BloodEffectObject.GetComponentInChildren<TrailRenderer>().startWidth = 1;

        //BloodEffectObject.transform.position = new Vector3(enemy.GetChild(0).position.x, BloodEffectObject.transform.position.y, enemy.GetChild(0).position.z);

        BloodEffectObject.GetComponentInChildren<TrailRenderer>().emitting = true;
        yield return new WaitForSeconds(0.1f);
        BloodEffectObject.GetComponentInChildren<TrailRenderer>().emitting = false;
        //BloodEffectObject.GetComponentInChildren<TrailRenderer>().endWidth = 0;
        //BloodEffectObject.GetComponentInChildren<TrailRenderer>().startWidth = 0;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //BloodEffectObject.SetActive(false);
            //hitEndPosition = collision.contacts.LastOrDefault().point;

            //var direction = hitEndPosition - hitStartPosition;

            //StartCoroutine(MoveObject(direction));

            //hitEndPosition = WeaponManager.Instance.ActualWeapon.TipOfTheSword.position;
            //collision.gameObject.GetComponent<EnemyAnimation>().EndPointOfHit = collision.contacts.LastOrDefault().point;
            //collision.gameObject.GetComponent<EnemyAnimation>().GetHitDirection(collision.gameObject.transform.forward);

        }
    }

    public IEnumerator MoveObject(Transform objectToMoveParent, Vector3 direction, float distance, Transform enemytransform)
    {
        var objectToMove = objectToMoveParent.GetComponentInChildren<TrailRenderer>().transform;

        objectToMove.GetComponent<TrailRenderer>().emitting = true;

        direction = new Vector3(direction.x,0, direction.z);

        var playertransform = FindObjectOfType<ThirdPersonController>().transform;
        var distanceToEnemy = Vector3.Distance(playertransform.position, enemytransform.position);
        var directionToEnemy = (enemytransform.position - playertransform.position);

        //objectToMove.position = new Vector3(objectToMove.position.x, playertransform.position.y, objectToMove.position.z)  + new Vector3(directionToEnemy.x,0, directionToEnemy.z);

        var hips = enemytransform.GetComponent<EnemyAnimation>().Hips;

        objectToMoveParent.transform.parent = null;
        objectToMoveParent.position = new Vector3(hips.position.x, hips.position.y, hips.position.z);

        objectToMove.transform.parent = null;
        objectToMove.position = new Vector3(hips.position.x, hips.position.y, hips.position.z);

        objectToMove.position = objectToMove.position - direction*4;

        

        // Normalize the direction vector to ensure consistent movement speed
        direction.Normalize();

        // Calculate the target position by adding the direction vector multiplied by the distance to the current position
        Vector3 targetPosition = objectToMove.position + direction * distance;

        // Loop until the object reaches the target position
        while (Vector3.Distance(objectToMove.position, targetPosition) > 0.01f)
        {
            var actualPos = new Vector3(objectToMove.position.x, objectToMove.position.y, hips.position.z);
            // Move the object towards the target position
            objectToMove.position = Vector3.MoveTowards(actualPos, targetPosition, 40 * Time.deltaTime);

            // Yield until the next frame
            yield return null;
        }

        objectToMove.GetComponentInChildren<TrailRenderer>().emitting = false;

        // Ensure the object is exactly at the target position
        objectToMove.position = targetPosition;
    }

}
