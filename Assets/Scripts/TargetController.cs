using System;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] 
    private SphereCollision _sphereCollision;
    [SerializeField] 
    private float _rotationSpeed;

    private Transform _followedTarget;

    private void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, _rotationSpeed * Time.deltaTime * 360 * Mathf.Sin(Time.timeSinceLevelLoad)));
    }

    public bool TryCapture(Transform transform)
    {
        if (_sphereCollision.IsInRange(transform.position))
        {
            _followedTarget = transform;
            Destroy(this.gameObject);
            return true;
        }

        return false;
    }
}
