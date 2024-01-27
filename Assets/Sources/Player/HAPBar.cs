
using Pixeye.Actors;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HAPBar : MonoCached
{
    private TextMeshProUGUI hapTMP;
    private Slider slider;

    /// <summary>
    /// Start in MonoCached
    /// </summary>
    protected override void Setup()
    {
        base.Start();
        hapTMP = GetComponent<TextMeshProUGUI>();
        slider = GetComponent<Slider>();
    }
    
    // TODO: change to discrete or continuous bar
    public void SetValue(float value)
    {
        hapTMP?.SetText($"HAP: {Mathf.RoundToInt(value)}");
        if (slider) slider.value = value;
    }
}
