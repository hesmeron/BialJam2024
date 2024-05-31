using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private float _minSize = 5;
    [SerializeField] 
    private float _maxSize = 25;
    [SerializeField] 
    private float _maxSpeed;
    [SerializeField] 
    private AnimationCurve _animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [SerializeField] 
    private Transform _target;
    [SerializeField] 
    private Vector3 _offset = Vector2.zero;
    [SerializeField] 
    private Camera _camera;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, Time.deltaTime * 40f);
    }

    public void UpdateSpeed(float speed)
    {
        float delta = _animationCurve.Evaluate(speed / _maxSize);
        _camera.orthographicSize = _minSize + ((_maxSize - _minSize) * delta);
    }
}
