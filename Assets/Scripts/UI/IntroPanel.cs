using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPanel : MonoBehaviour
{
    public AudioClip Clip;

    void Start()
    {
        SoundManager.Instance.PlaySound(Clip);
        StartCoroutine(WaitForHide());
    }

    public IEnumerator WaitForHide()
    {
        yield return new WaitForSeconds(2f);
        yield return new WaitUntil(() => !SoundManager.Instance._AudioSource.isPlaying);
        gameObject.SetActive(false);
    }
}
