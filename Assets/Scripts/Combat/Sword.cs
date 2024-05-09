using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public EHitEffect hitEffect;
    public Transform TipOfTheSword;
    private Vector3 hitStartPosition;
    private Vector3 hitEndPosition;

    public Rigidbody TipOfTheSwordRigidbody;

    public GameObject FireEffect;
    public GameObject IceEffect;

    public Vector3 velocity;
    public Vector3 previous;

    public GameObject BloodEffectObject;

    private enum HitDirection { Front, Back, Right, Left }

    private HitDirection ActualDirection;

    private void Start()
    {
        //TipOfTheSword = gameObject.transform.GetChild(0);
        BloodEffectObject.SetActive(true);
        BloodEffectObject.GetComponentInChildren<TrailRenderer>().emitting = false;
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

            collision.gameObject.GetComponent<EnemyAnimation>().ActivateEffect(FindObjectOfType<StatusEffects>().ActualHitEffect);

            StartCoroutine(DoBloodEffectCoroutine(collision.transform));

            //BloodEffectObject = Instantiate(PrefabManager.Instance.DarkBloodEffect, position: collision.contacts[0].point, Quaternion.identity);

            //StartCoroutine(MoveObject(-velocity));

            Debug.Log("Status Effetk:" + FindObjectOfType<PlayerAnimation>().ActualAttackItem.StatusEffect.ToString());

            //hitStartPosition = collision.contacts[0].point;

    //StartCoroutine(collision.gameObject.GetComponent<EnemyAnimation>().KillExecution());
        } 
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        BloodEffectObject.SetActive(true);
    //    }
    //}

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

    IEnumerator MoveObject(Vector3 direction)
    {
        while (true)
        {
            // Berechne die Bewegung pro Frame basierend auf der Geschwindigkeit und der Richtung
            Vector3 movement = direction.normalized * 0.03f;

            // Bewege das Objekt
            BloodEffectObject.transform.Translate(movement);

            // Warte bis zum nächsten Frame
            yield return null;
        }
    }
}
