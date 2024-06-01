using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] 
    private List<PlayerController> _controllers = new List<PlayerController>();
    [SerializeField]
    private Transform[] _spawnPoints = Array.Empty<Transform>();

    private bool _initiatedStart = false;

    public Vector3 GetSpawnPosition(int playerNumber, PlayerController _controller)
    {        
        _controllers.Add(_controller);
        if (playerNumber == 1 && !_initiatedStart)
        {
            _initiatedStart = true;
            StartCoroutine(StartSequence());
        }
        return _spawnPoints[playerNumber].position;

    }

    IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(1f);
        float _timeToPass = 3f;
        while (_timeToPass > 0f )
        {
            _timeToPass -= Time.deltaTime;
            foreach (PlayerController controller in _controllers)
            {
                controller.DisplayStartingSequence(_timeToPass);
            }
            yield return null;
        }
    }
    
}
