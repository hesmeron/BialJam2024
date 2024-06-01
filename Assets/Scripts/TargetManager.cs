using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] 
    private List<Transform> _spawnPoints;
    [SerializeField] 
    private TargetController _targetPrefab;
    [SerializeField] 
    private TargetController _target;
    
    public Vector3 GetTargetPosition()
    {
        return _target.transform.position;
    }

    public bool TryCaptureTheTarget(Transform rocket)
    {
        bool success = _target.TryCapture(rocket);
        if (success)
        {
            SpawnTarget();
        }
        return success;
    }

    private void SpawnTarget()
    {
        _target = Instantiate(_targetPrefab);
        int index = Random.Range(0, _spawnPoints.Count);
        _target.transform.position = _spawnPoints[index].transform.position;
        _spawnPoints.RemoveAt(index);

    }
    
}
