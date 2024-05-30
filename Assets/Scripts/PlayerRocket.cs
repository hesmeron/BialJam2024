using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : MonoBehaviour
{
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
    
    public void ApplyVelocity(float forward, float side)
    {
        Vector2 engineThrust = (transform.up * forward) + (transform.right * side);
        _velocity += (engineThrust * 0.01f) + _gravityField.GetAccelerationAtPosition(transform.position);
        transform.up = Vector3.Lerp(transform.up, _velocity, Time.deltaTime * 4f);
        transform.position += (Vector3) _velocity * Time.deltaTime;
    }
}
