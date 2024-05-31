using UnityEngine;
using UnityEngine.UI;

public class JoinPhaseController : MonoBehaviour
{
    [SerializeField] 
    private Image _playerOnePanel;
    [SerializeField] 
    private Image _playerTwoPanel;

    public void SetPlayerActive(int number)
    {
        if (number == 0)
        {
            _playerOnePanel.gameObject.SetActive(false);
        }
        else
        {
            _playerTwoPanel.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}