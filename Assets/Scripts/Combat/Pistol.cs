using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Vector3 hitPoint;
    public LineRenderer _LineRenderer;

    public GameObject MuzzleFlashGO;
    public GameObject HitImpactGO;

    public GameObject laserCube;

    public LayerMask layerMask;

    public Vector3 Point1;
    public Vector3 Point2;

    public List<AudioClip> ÁudioClips = new List<AudioClip>();
    public AudioSource _AudioSource;

    public bool IsShooting;
    public bool ShootingIsDone;

    private PlayerAnimation playerAnimation;

    private void Start()
    {
        playerAnimation = FindObjectOfType<PlayerAnimation>();
        _LineRenderer = GetComponent<LineRenderer>();
        MuzzleFlashGO.SetActive(false);
    }

    private void Update()
    {
        //_LineRenderer.SetPosition(0, playerAnimation.PlayerTransform.position);
        //_LineRenderer.SetPosition(1, DoPistolRaycast());

        // Setze die Startposition des Lasers auf die Position des Spielers
        //_LineRenderer.SetPosition(0, playerAnimation.PlayerTransform.position);

        DoLaser();
        DoShot();

    }

    public void DoImpact(Vector3 HitPoint)
    {
        Instantiate(HitImpactGO, HitPoint, Quaternion.identity);

        var amount = Random.Range(4, 7);

        for (int i = 0; i < amount; i++)
        {
            Instantiate(PrefabManager.Instance.HealthItem, position: HitPoint + new Vector3(Random.Range(0.2f,3f),0, Random.Range(0.2f, 3f)),Quaternion.identity);
        }
    }

    public IEnumerator DoMuzzleFlash()
    {
        MuzzleFlashGO.SetActive(true);
        IsShooting = true;
        yield return new WaitForSeconds(0.7f);
        IsShooting = false;
        ShootingIsDone = false;
        MuzzleFlashGO.SetActive(false);
    }

    public void DoShot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !IsShooting)
        {
            PlayRandomSound();
            StartCoroutine(DoMuzzleFlash());
        }
    }

    public void PlayRandomSound()
    {
        _AudioSource.Stop();
        _AudioSource.clip = ÁudioClips[Random.Range(0, ÁudioClips.Count)];
        _AudioSource.Play();
    }

    public void DoLaser()
    {
        Point1 = new Vector3(playerAnimation.PlayerTransform.position.x, playerAnimation.PlayerTransform.position.y + 1, playerAnimation.PlayerTransform.position.z);

        Ray ray = new Ray(Point1, playerAnimation.PlayerTransform.forward * 200);
        RaycastHit hit;

        // Überprüfen, ob der Ray ein Objekt trifft
        if (Physics.Raycast(ray, out hit, 200, layerMask))
        {
            // Den Punkt speichern, an dem der Ray das Objekt getroffen hat
            hitPoint = hit.point;
            Point2 = hitPoint;
            Strech2(laserCube, Point1, hitPoint, false);
            //_LineRenderer.SetPosition(1, hitPoint);

            if (IsShooting && !ShootingIsDone && hit.transform.tag == "Enemy")
            {
                ShootingIsDone = true;
                DoImpact(hitPoint);
            }
            
            return;
        }

        // Berechne das Endpunkt des Lasers
        Vector3 laserEndPoint = playerAnimation.PlayerTransform.position + playerAnimation.PlayerTransform.forward * 20;

        Point2 = laserEndPoint;
        Strech2(laserCube, Point1, laserEndPoint, false);
        // Setze die Endposition des Lasers
        //_LineRenderer.SetPosition(1, laserEndPoint);
    }

    public void Strech2(GameObject _sprite, Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ)
    {
        Vector3 centerPos = (_initialPosition + _finalPosition) / 2f;
        _sprite.transform.position = centerPos;
        Vector3 direction = _finalPosition - _initialPosition;
        direction = Vector3.Normalize(direction);
        _sprite.transform.right = direction;
        if (_mirrorZ) _sprite.transform.right *= -1f;
        Vector3 scale = new Vector3(0.001f, 0.001f, 0.001f);
        scale.x = Vector3.Distance(_initialPosition, _finalPosition)/100;
        _sprite.transform.localScale = scale;
    }

    public Vector3 DoPistolRaycast()
    {
        // Einen Ray in die Vorwärtsrichtung des Objekts schießen
        Ray ray = new Ray(playerAnimation.PlayerTransform.position, GetMouseWorldPosition());
        RaycastHit hit;

        // Überprüfen, ob der Ray ein Objekt trifft
        if (Physics.Raycast(ray, out hit, 20, layerMask))
        {
            // Den Punkt speichern, an dem der Ray das Objekt getroffen hat
            hitPoint = hit.point;
            return hitPoint;
        }

        return playerAnimation.PlayerTransform.position/* + (GetMouseWorldPosition().normalized * 20)*/;
    }

    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Physics.Raycast(ray, out hit, 200);

        var MousePosition = hit.point;
        var FlatMousePosition = new Vector3(MousePosition.x, playerAnimation.PlayerTransform.transform.position.y, MousePosition.z);

        return FlatMousePosition;
    }
}
