using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : GravitySource
{
    [SerializeField]
    private float _maxDistance;
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 0f, 1f, 0.4f);
        Gizmos.DrawSphere(transform.position, _maxDistance);
        Gizmos.color = new Color(0f, 0f, 1f, 1f);
        Gizmos.DrawWireSphere(transform.position, _maxDistance);
    }
    public override Vector2 GetAccelerationAtPosition(Vector3 position)
    {
        Vector2 direction = transform.position - position;
        return direction.normalized / Mathf.Max(0.001f, _maxDistance - position.magnitude);
    }
}
