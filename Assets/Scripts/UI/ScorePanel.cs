using System;
using UnityEngine;

public class ScorePanel : MonoBehaviour
{
    [SerializeField]
    private ScoreSlotUI[] _scoreSlots = Array.Empty<ScoreSlotUI>();

    public void ShowScore(int count)
    {
        foreach (ScoreSlotUI slot in _scoreSlots)
        {
            slot.Deactivate();
        }

        int activatedCount = Mathf.Min(count, _scoreSlots.Length);
        for (int i = 0; i < activatedCount; i++)
        {
            _scoreSlots[i].Activate();
        }
    }
}
