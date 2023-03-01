using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorDetector : MonoBehaviour
{
    [SerializeField] private Transform[] _rayOrigins;
    [SerializeField] private float _rayLength = 1.5f;
    [SerializeField] private LayerMask _groundMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        foreach (Transform t in _rayOrigins)
        {
            Gizmos.DrawRay(t.position, Vector3.down * _rayLength);
        }
    }

    public Vector3 AverageHeight()
    {
        int hitCount = 0;
        Vector3 combinedPosition = Vector3.zero;
        RaycastHit hit;

        foreach (Transform t in _rayOrigins)
        {
            if (Physics.Raycast(t.position, Vector3.down, out hit, _rayLength, _groundMask))
            {
                hitCount++;
                combinedPosition += hit.point;
            }
        }

        Vector3 averagePosition = Vector3.zero;

        if(hitCount> 0)
        {
            averagePosition = combinedPosition/hitCount;
        }

        return averagePosition;
    }
}
