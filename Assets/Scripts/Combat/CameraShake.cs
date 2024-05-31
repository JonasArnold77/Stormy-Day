using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(Shake(0.2f,0.3f));
        }
    }

    public IEnumerator Shake(float duration, float intensity)
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * intensity;
            float z = Random.Range(-1f, 1f) * intensity;

            transform.localPosition = new Vector3(transform.localPosition.x + x, originalPosition.z, transform.localPosition.z + z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}