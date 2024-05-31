using TMPro;
using UnityEngine;

public class ShipDestroyedPanel : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text _text;

    public void ShowRespawnTime(float value)
    {
        _text.text = "Reinfiorcments will arrive in: " + value.ToString("F1");
    }
    
}
