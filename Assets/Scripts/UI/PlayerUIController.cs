using TMPro;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] 
    private CompassController _compassController;
    [SerializeField] 
    private TMP_Text _speedDisplay;
    [SerializeField] 
    private ScorePanel _scorePanel;
    [SerializeField] 
    private Canvas _canvas;

    public CompassController CompassController => _compassController;

    public void Initialize(Camera camera)
    {
        _canvas.worldCamera = camera;
    }

    public void DisplaySpeed(float speed)
    {
        _speedDisplay.text = "<mspace=0.5em>" + speed.ToString("F1") + "</mspace>";
    }

    public void ShowScore(int score)
    {
        _scorePanel.ShowScore(score);
    }
}
