using System;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] 
    private SphereCollision[] _collisions = Array.Empty<SphereCollision>();

    private void Start()
    {
        FillInColliders();
    }

    private void FillInColliders()
    {
        _collisions = gameObject.GetComponentsInChildren<SphereCollision>();
    }

    public bool IsInRange(SphereCollision targetCollision)
    {
        foreach (SphereCollision collision in _collisions)
        {
            if (collision.IsInRange(targetCollision.transform.position, targetCollision.Radius))
            {
                return true;
            }
        }

        return false;
    }
}
