using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] 
    private PlayerRocket _playerRocket;
    [SerializeField] 
    private Camera _camera;
    [SerializeField] 
    private PlayerUIController _playerUIControllerPrefab;
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
    
    public void OnEnable()
    {
        _mediator = FindObjectOfType<GameMediator>();
        foreach (GameObject sprite in _sprites)
        {
            sprite.SetActive(false);
        }
        _playerId = _mediator.PlayerInputManager.playerCount - 1;
        _sprites[_playerId].SetActive(true);
        transform.position += Vector3.right * _playerId;
        gameObject.name = "Player" + _playerId;
        _playerUIController = Instantiate(_playerUIControllerPrefab);
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
        float forward = (_rightEngineValue + _leftEngineValue) * (1 - _sideThrustMultiplier);
        float side = (_leftEngineValue - _rightEngineValue) * _sideThrustMultiplier;
        
        _playerRocket.ApplyVelocity(forward, side, _leanValue);
        _playerUIController.DisplaySpeed(_playerRocket.GetSpeed());
    }

    private void HandleCapturing()
    {
        Vector2 targetPosition = _mediator.TargetManager.GetTargetPosition();
        Vector2 translation = targetPosition - (Vector2)_playerRocket.transform.position;
        _playerUIController.CompassController.VisualizeTarget(translation.normalized, translation.magnitude);

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
        _playerRocket.transform.position = _mediator.PlayerSpawnManager.GetSpawnPosition(_playerId);
        _playerRocket.ResetVelocity();
    }

    private IEnumerator RespawnCoroutine()
    {
        float timeSinceDestruction = 0f;
        while (_destroyed)
        {
            yield return null;
            timeSinceDestruction += Time.deltaTime;
            float timeLeft = _respawnTime - timeSinceDestruction;
            Debug.Log("Time left " + timeLeft);
            if (timeLeft <= 0)
            {
                Respawn();
                break;
            }
            _playerUIController.ShowRespawnTimeLeft(timeLeft);
        }
    }
}
