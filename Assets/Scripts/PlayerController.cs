using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private CameraController _cameraController;
    [SerializeField] 
    private PlayerRocket _playerRocket;
    [SerializeField] 
    private Camera _camera;
    [SerializeField] 
    private PlayerUIController _playerOneUIControllerPrefab;    
    [SerializeField] 
    private PlayerUIController _playerTwoUIControllerPrefab;
    [SerializeField] 
    private List<GameObject> _sprites = new List<GameObject>();
    [SerializeField]
    [Range(0f, 1f)]
    private float _sideThrustMultiplier = 0.3f;
    [SerializeField] 
    private SphereCollision[] _collisons = Array.Empty<SphereCollision>();
    [SerializeField]
    private float _respawnTime = 3f;

    private PlayerUIController _playerUIController;
    private float _rightEngineTarget=0f;
    private float _rightEngineValue=0f;
    private float _leftEngineTarget =0f;
    private float _leftEngineValue=0f;
    private float _leanTarget = 0f;
    private float _leanValue = 0f;
    private int _capturedTargets = 0;
    private GameMediator _mediator;
    private int _playerId;
    private bool _destroyed = false;
    private bool _isIntialized = false;

    private void OnEnable()
    {
        if (!_isIntialized)
        {
            Initialize();
        }
    }

    public void Initialize()
    {
        _isIntialized = true;
        _mediator = FindObjectOfType<GameMediator>();
        foreach (GameObject sprite in _sprites)
        {
            sprite.SetActive(false);
        }
        _playerId = _mediator.PlayerInputManager.playerCount - 1;
        _sprites[_playerId].SetActive(true);
        _playerRocket.transform.position = _mediator.PlayerSpawnManager.GetSpawnPosition(_playerId , this);
        gameObject.name = "Player" + _playerId;
        if (_playerId == 0)
        {
            _playerUIController = Instantiate(_playerOneUIControllerPrefab);
        }
        else
        {
            _playerUIController = Instantiate(_playerTwoUIControllerPrefab);
        }

        _playerUIController.Initialize(_camera);
        _mediator.JoinPhaseController.SetPlayerActive(_playerId);
    }

    private void Update()
    {
        if (!_destroyed)
        {
            HandleMovement();
            HandleCapturing();
            HandleCollisions();
        }
    }
    public void OnRightForward(InputValue value)
    {
        _rightEngineTarget= value.Get<float>();
    }
    
    public void OnRightBack(InputValue value)
    {
        _rightEngineTarget= -0.5f *  value.Get<float>();
    }
    
    public void OnLeftForward(InputValue value)
    {
        _leftEngineTarget= value.Get<float>();
    }
    
    public void OnLeftBack(InputValue value)
    {
        _leftEngineTarget= -0.5f *  value.Get<float>();
    }
    
    public void OnLean(InputValue value)
    {
        _leanTarget = value.Get<float>();
    }

    private void HandleMovement()
    {
        _rightEngineValue = Mathf.Lerp(_rightEngineValue, _rightEngineTarget, Time.deltaTime * 3f);
        _leftEngineValue = Mathf.Lerp(_leftEngineValue, _leftEngineTarget, Time.deltaTime * 3f);
        _leanValue = Mathf.Lerp(_leanValue, _leanTarget, Time.deltaTime);
        _playerRocket.ApplyVelocity(_leftEngineValue, _rightEngineValue, _leanValue);
        _playerUIController.DisplaySpeed(_playerRocket.GetSpeed());
        _playerUIController.DisplayEngines(_leftEngineValue, _rightEngineValue);
        _playerUIController.DisplayLean(_leanValue);
        _cameraController.UpdateSpeed(_playerRocket.GetSpeed());
    }

    private void HandleCapturing()
    {
        Vector2 targetPosition = _mediator.TargetManager.GetTargetPosition();
        Vector2 translation = targetPosition - (Vector2)_playerRocket.transform.position;
        _playerUIController.CompassController.VisualizeTarget(translation.normalized, _playerRocket.transform.up, translation.magnitude);

        if (_mediator.TargetManager.TryCaptureTheTarget(_playerRocket.transform))
        {
            _capturedTargets++;
            _playerUIController.ShowScore(_capturedTargets);
            if (_capturedTargets >= 3)
            {
                _mediator.EndgamePhaseController.EndGame(_playerId+1);
            }
        }
    }

    private void OnRestart()
    {
        Debug.Log("On restart");
    }

    private void HandleCollisions()
    {
        foreach (SphereCollision collision in _collisons)
        {
            if (_mediator.CollisionManager.IsInRange(collision))
            {
                Explode();
                break;
            }
        }
    }

    private void Explode()
    {
        Debug.Log("Explode");
        _destroyed = true;
        _playerUIController.ShowDestroyedPanel();
        _playerRocket.gameObject.SetActive(false);
        StartCoroutine(RespawnCoroutine());
    }
    
    private void Respawn()
    {
        _playerUIController.HideDestroyedPanel();
        _leanValue = 0f;
        _leftEngineValue = 0f;
        _rightEngineValue = 0f;
        _destroyed = false;
        _playerRocket.gameObject.SetActive(true);
        _playerRocket.transform.position = _mediator.PlayerSpawnManager.GetSpawnPosition(_playerId, this);
        _playerRocket.ResetVelocity();
    }

    public void DisplayStartingSequence(float time)
    {
        if (!_isIntialized)
        {
            Initialize();
        }
        Assert.IsNotNull(_playerUIController);
        _playerUIController.StartingSequenceUI.DisplayNumber(4 - Mathf.CeilToInt(time));
        if (time <= Single.Epsilon)
        {
            _playerUIController.StartingSequenceUI.TurnOff();
            _playerRocket.StartRace();
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        float timeSinceDestruction = 0f;
        while (_destroyed)
        {
            yield return null;
            timeSinceDestruction += Time.deltaTime;
            float timeLeft = _respawnTime - timeSinceDestruction;
            if (timeLeft <= 0)
            {
                Respawn();
                break;
            }
            _playerUIController.ShowRespawnTimeLeft(timeLeft);
        }
    }
}
