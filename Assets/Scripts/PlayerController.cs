using System.Collections.Generic;
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

    private PlayerUIController _playerUIController;
    private PlayerInputManager _playerInputManager;
    private float _rightEngineTarget=0f;
    private float _rightEngineValue=0f;
    private float _leftEngineTarget =0f;
    private float _leftEngineValue=0f;
    [SerializeField]
    [Range(0f, 1f)]
    private float _sideThrustMultiplier = 0.3f;
    
    public void OnEnable()
    {
        foreach (GameObject sprite in _sprites)
        {
            sprite.SetActive(false);
        }
        _playerInputManager = FindObjectOfType<PlayerInputManager>();
        int playerCount = _playerInputManager.playerCount - 1;
        _sprites[playerCount].SetActive(true);
        transform.position += Vector3.right * playerCount;
        gameObject.name = "Player" + playerCount;
        _playerUIController = Instantiate(_playerUIControllerPrefab);
        _playerUIController.Initialize(_camera);
    }

    private void Update()
    {
        _rightEngineValue = Mathf.Lerp(_rightEngineValue, _rightEngineTarget, Time.deltaTime * 3f);
        _leftEngineValue = Mathf.Lerp(_leftEngineValue, _leftEngineTarget, Time.deltaTime * 3f);
        float forward = (_rightEngineValue + _leftEngineValue) * (1 - _sideThrustMultiplier);
        float side = (_leftEngineValue - _rightEngineValue) * _sideThrustMultiplier;
        _playerRocket.ApplyVelocity(forward, side);
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
}
