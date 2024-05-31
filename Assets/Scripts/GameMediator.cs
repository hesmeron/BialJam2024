using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMediator : MonoBehaviour
{
    [SerializeField]
    private EndgamePhaseController _endgamePhaseController;
    [SerializeField]
    private TargetManager _targetManager;
    [SerializeField] 
    private PlayerInputManager _playerInputManager;
    [SerializeField] 
    private JoinPhaseController _joinPhaseController;

    public TargetManager TargetManager => _targetManager;

    public PlayerInputManager PlayerInputManager => _playerInputManager;

    public JoinPhaseController JoinPhaseController => _joinPhaseController;

    public EndgamePhaseController EndgamePhaseController => _endgamePhaseController;
}
