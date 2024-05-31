using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] 
    private Transform[] _spawnPoints;
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
        _target.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position;

    }
    
}
