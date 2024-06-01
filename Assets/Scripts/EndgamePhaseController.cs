using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndgamePhaseController : MonoBehaviour
{
    [SerializeField] private Image _beaverOne;
    [SerializeField] private Image _beaverTwo;
    [SerializeField] 
    private Color _player1Color;
    [SerializeField] 
    private Color _player2Color;
    [SerializeField] 
    private InputActionAsset _inputActionMap;
    [SerializeField] 
    private Image _backgroundRoll;
    [SerializeField] 
    private TMP_Text _text;
    [SerializeField]
    private Image _restartButtonDisplay;
    [SerializeField] 
    private float _timeToRestart = 3f;

    [SerializeField]
    private float _timePassed = 0f;
    [SerializeField]
    private bool _isHolding = false;
    

    private void Start()
    {
        var action =  _inputActionMap.FindAction("Restart");
        action.started += OnRestartPerformed;
        action.canceled += OnRestartCanceled;
        _inputActionMap.Enable();
    }

    private void Update()
    {
        if (_isHolding)
        {
            _timePassed += Time.deltaTime;
        }
        else
        {
            _timePassed -= Time.deltaTime / 2f;
            _timePassed = Mathf.Max(0f, _timePassed);
        }

        _restartButtonDisplay.fillAmount = (_timePassed / _timeToRestart);
        if (_timePassed > _timeToRestart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnRestartCanceled(InputAction.CallbackContext obj)
    {
        Debug.Log("On restart canceled");
        _isHolding = false;
    }

    private void OnRestartPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("On restart");
        _isHolding = true;
    }

    public void EndGame(int victoriusPlayer)
    {
        _backgroundRoll.gameObject.SetActive(true);
        _text.text = $"Player {victoriusPlayer} won";
        _backgroundRoll.color = victoriusPlayer == 1 ? _player1Color : _player2Color;
        Image beaver = victoriusPlayer == 1 ? _beaverOne : _beaverTwo;
        beaver.gameObject.SetActive(true);
    }
}
