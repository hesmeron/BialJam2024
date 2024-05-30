using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private List<GameObject> _sprites = new List<GameObject>();
    
    private PlayerInputManager _playerInputManager;
    public void OnRightForward(InputValue value)
    {
        float valueFloat = value.Get<float>();
        Debug.Log("Right " + valueFloat);
    }
    
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
    }
}
