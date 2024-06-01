using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerRocket : MonoBehaviour
{
    [SerializeField]
    private float _maxSpeed = 100f;
    [SerializeField] 
    private float _thrustRotation = 1f;
    
    private GravityField _gravityField;
    private Vector2 _velocity = Vector3.zero;

    private void OnEnable()
    {
        _gravityField = FindObjectOfType<GravityField>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position +  (Vector3) Vector2.Perpendicular(_velocity).normalized);
    }
    
    public void ApplyVelocity(float _rightEngineValue, float _leftEngineValue, float lean)
    {
        float friction = 0.15f;


        _velocity *= 1 - (friction * Time.deltaTime);

        Vector2 engineThrust = RotateVector(transform.up * _rightEngineValue, -_thrustRotation) +
                               RotateVector(transform.up * _leftEngineValue, _thrustRotation);
        Vector2 gravity = _gravityField.GetAccelerationAtPosition(transform.position) *
                          Mathf.Clamp(_velocity.magnitude / 20f, 0.25f, 1f);
        _velocity += (engineThrust * 0.025f) + gravity;
        _velocity = _velocity.normalized * Mathf.Min(_velocity.magnitude, _maxSpeed);
        
        Vector2 lookDirection = RotateVector(_velocity.normalized, (lean * -1f));
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation( Vector3.forward,  lookDirection), Time.deltaTime * 16f);
        transform.position += (Vector3) _velocity * Time.deltaTime;
    }

    private Vector2 RotateVector(Vector2 v, float degrees)
    {
        float x = (v.x * Mathf.Cos(degrees)) - v.y * Mathf.Sin(degrees);
        float y = (v.x * Mathf.Sin(degrees)) + v.y * Mathf.Cos(degrees);
        return new Vector2(x, y);
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
