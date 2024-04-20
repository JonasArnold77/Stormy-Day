using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public Transform TipOfTheSword;
    private Vector3 hitStartPosition;
    private Vector3 hitEndPosition;
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
            hitStartPosition = WeaponManager.Instance.ActualWeapon.TipOfTheSword.position;
            collision.gameObject.GetComponent<EnemyAnimation>().StartPointOfHit = collision.contacts[0].point;
            collision.gameObject.GetComponent<EnemyAnimation>().PlayingHitAnimation();
            StartCoroutine(collision.gameObject.GetComponent<EnemyAnimation>().KillExecution());
        } 
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            hitEndPosition = WeaponManager.Instance.ActualWeapon.TipOfTheSword.position;
            //collision.gameObject.GetComponent<EnemyAnimation>().EndPointOfHit = collision.contacts.LastOrDefault().point;
            //collision.gameObject.GetComponent<EnemyAnimation>().GetHitDirection(collision.gameObject.transform.forward);
            
        }
    }
}
