using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] 
    private StartingSequenceUI _startingSequenceUI;
    [SerializeField] 
    private LeanGauge _leftLeanGauge;
    [SerializeField]
    private LeanGauge _rightLeanGauge;
    [SerializeField]
    private EngineGaugeUI _leftEngineGauge;
    [SerializeField]
    private EngineGaugeUI _rightEngineGauge;
    [SerializeField] 
    private ShipDestroyedPanel _shipDestroyedPanel;
    [SerializeField] 
    private CompassController _compassController;
    [SerializeField] 
    private TMP_Text _speedDisplay;
    [SerializeField] 
    private ScorePanel _scorePanel;
    [SerializeField] 
    private Canvas _canvas;

    public CompassController CompassController => _compassController;

    public StartingSequenceUI StartingSequenceUI => _startingSequenceUI;

    public void Initialize(Camera camera)
    {
        _canvas.worldCamera = camera;
    }

    public void DisplayLean(float lean)
    {
        if (lean < 0)
        {
            _leftLeanGauge.DisplayLean(-lean);
            _rightLeanGauge.DisplayLean(0f);
        }
        else
        {
            _leftLeanGauge.DisplayLean(0f);
            _rightLeanGauge.DisplayLean(lean);
        }
    }

    public void DisplayEngines(float left, float right)
    {
        _leftEngineGauge.DisplayThrust(left);
        _rightEngineGauge.DisplayThrust(right);
    }

    public void DisplaySpeed(float speed)
    {
        _speedDisplay.text = "<mspace=0.5em>" + speed.ToString("F1") + "</mspace>";
    }

    public void ShowScore(int score)
    {
        _scorePanel.ShowScore(score);
    }

    public void ShowDestroyedPanel()
    {
        _shipDestroyedPanel.gameObject.SetActive(true);
    }   
    
    public void ShowRespawnTimeLeft(float time)
    {
        _shipDestroyedPanel.ShowRespawnTime(time);
    }
    
    public void HideDestroyedPanel()
    {
        _shipDestroyedPanel.gameObject.SetActive(false);
    }
}
