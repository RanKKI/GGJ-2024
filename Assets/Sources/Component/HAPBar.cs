
using Pixeye.Actors;
using TMPro;
using UnityEngine;

public class HAPBar : MonoCached
{
    private TextMeshProUGUI hapTMP;

    /// <summary>
    /// Start in MonoCached
    /// </summary>
    protected override void Start()
    {
        base.Start();
        hapTMP = GetComponent<TextMeshProUGUI>();
    }
    
    // TODO: change to discrete or continuous bar
    public void SetValue(float value)
    {
        hapTMP.SetText($"HAP: {Mathf.RoundToInt(value)}");
    }
}
