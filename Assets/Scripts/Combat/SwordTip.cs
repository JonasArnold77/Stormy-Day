using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTip : MonoBehaviour
{
    private Vector3 LastPosition;

    public Vector3 _Direction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(LastPosition, transform.position)>=0.1f)
        {
            _Direction = (LastPosition - transform.position).normalized;
            _Direction = new Vector3(_Direction.x, 0, _Direction.z);
            LastPosition = transform.position;
        }
    }
}
