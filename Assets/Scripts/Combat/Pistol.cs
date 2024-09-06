using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Vector3 hitPoint;
    public LineRenderer _LineRenderer;

    public LayerMask layerMask;

    private PlayerAnimation playerAnimation;

    private void Start()
    {
        playerAnimation = FindObjectOfType<PlayerAnimation>();
        _LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //_LineRenderer.SetPosition(0, playerAnimation.PlayerTransform.position);
        //_LineRenderer.SetPosition(1, DoPistolRaycast());

        // Setze die Startposition des Lasers auf die Position des Spielers
        _LineRenderer.SetPosition(0, playerAnimation.PlayerTransform.position);

        // Berechne das Endpunkt des Lasers
        Vector3 laserEndPoint = playerAnimation.PlayerTransform.position + playerAnimation.PlayerTransform.forward * 20;

        // Setze die Endposition des Lasers
        _LineRenderer.SetPosition(1, laserEndPoint);
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
