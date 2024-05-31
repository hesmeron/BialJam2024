using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] 
    private Transform _target;
    
    public Vector3 GetTargetPosition()
    {
        return _target.position;
    }
}
