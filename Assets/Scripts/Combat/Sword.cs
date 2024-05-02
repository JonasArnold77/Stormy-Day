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

    public GameObject FireEffect;
    public GameObject IceEffect;

    private enum HitDirection { Front, Back, Right, Left }

    private void Start()
    {
        TipOfTheSword = gameObject.transform.GetChild(0);
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

            collision.gameObject.GetComponent<EnemyAnimation>().ActivateEffect(FindObjectOfType<StatusEffects>().ActualHitEffect);

            Debug.Log("Status Effetk:" + FindObjectOfType<PlayerAnimation>().ActualAttackItem.StatusEffect.ToString());



            //StartCoroutine(collision.gameObject.GetComponent<EnemyAnimation>().KillExecution());
        } 
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //hitEndPosition = WeaponManager.Instance.ActualWeapon.TipOfTheSword.position;
            //collision.gameObject.GetComponent<EnemyAnimation>().EndPointOfHit = collision.contacts.LastOrDefault().point;
            //collision.gameObject.GetComponent<EnemyAnimation>().GetHitDirection(collision.gameObject.transform.forward);
            
        }
    }
}
