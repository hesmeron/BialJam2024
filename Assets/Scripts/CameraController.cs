using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private Transform _target;
    [SerializeField] 
    private Vector3 _offset = Vector2.zero;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, Time.deltaTime * 40f);
    }
}
