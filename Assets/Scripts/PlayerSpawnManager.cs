using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] _spawnPoints = Array.Empty<Transform>();

    public Vector3 GetSpawnPosition(int playerNumber)
    {
        return _spawnPoints[playerNumber].position;
    }
    
}
