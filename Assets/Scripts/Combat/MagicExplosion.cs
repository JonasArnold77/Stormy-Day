using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicExplosion : MonoBehaviour
{
    private bool ColliderIsActive = true;
    public EStatusEffects statusEffect;
    public int Damage;

    private void Start()
    {
        StartCoroutine(WaitForExplosion());
    }

    private IEnumerator WaitForExplosion()
    {
        yield return new WaitForSeconds(1f);
        ColliderIsActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyAnimation>() && ColliderIsActive)
        {
            other.GetComponent<EnemyAnimation>().PlayingHitAnimation(EHitEffect.HeadHit);
            other.gameObject.GetComponent<EnemyHealth>().Health -= Damage;
        }
    }
}
