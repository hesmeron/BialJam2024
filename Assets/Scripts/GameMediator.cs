using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMediator : MonoBehaviour
{
    [SerializeField]
    private TargetManager _targetManager;
    [SerializeField] 
    private PlayerInputManager _playerInputManager;

    public TargetManager TargetManager => _targetManager;

    public PlayerInputManager PlayerInputManager => _playerInputManager;
}
