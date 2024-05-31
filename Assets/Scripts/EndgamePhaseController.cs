using TMPro;
using UnityEngine;

public class EndgamePhaseController : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text _text;
    
    public void EndGame(int victoriusPlayer)
    {
        gameObject.SetActive(true);
        _text.text = $"Player {victoriusPlayer} won";
    }
}
