using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerRocket : MonoBehaviour
{
    [SerializeField]
    private float _maxSpeed = 100f;
    
    private GravityField _gravityField;
    private Vector2 _velocity = Vector3.zero;

    private void OnEnable()
    {
        _gravityField = FindObjectOfType<GravityField>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) _velocity);
    }
    
    public void ApplyVelocity(float forward, float side, float lean)
    {
        Vector2 engineThrust = (transform.up * forward) + (transform.right * side);
        _velocity += (engineThrust * 0.01f) + _gravityField.GetAccelerationAtPosition(transform.position);
        _velocity = _velocity.normalized * Mathf.Min(_velocity.magnitude, _maxSpeed);
        Vector2 lookDirection = _velocity.normalized - (Vector2.Perpendicular(_velocity).normalized * lean);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation( Vector3.forward,  lookDirection), Time.deltaTime);
        transform.position += (Vector3) _velocity * Time.deltaTime;
    }

    public float GetSpeed()
    {
        return _velocity.magnitude;
    }
    
    public void ResetVelocity()
    {
        _velocity = Vector2.zero;
        transform.up = Vector2.up;
    }
}
