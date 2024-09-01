using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Vector3 hitPoint;
    public LineRenderer _LineRenderer;

    private PlayerAnimation playerAnimation;

    private void Start()
    {
        playerAnimation = FindObjectOfType<PlayerAnimation>();
        _LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        _LineRenderer.SetPosition(0, playerAnimation.PlayerTransform.position);
        _LineRenderer.SetPosition(1, DoPistolRaycast());
    }

    public Vector3 DoPistolRaycast()
    {
        // Einen Ray in die Vorwärtsrichtung des Objekts schießen
        Ray ray = new Ray(playerAnimation.PlayerTransform.position, playerAnimation.PlayerTransform.forward);
        RaycastHit hit;

        // Überprüfen, ob der Ray ein Objekt trifft
        if (Physics.Raycast(ray, out hit, 100000000))
        {
            // Den Punkt speichern, an dem der Ray das Objekt getroffen hat
            hitPoint = hit.point;
            return hitPoint;
        }

        return playerAnimation.PlayerTransform.position + playerAnimation.PlayerTransform.forward.normalized * 100;
    }
}
