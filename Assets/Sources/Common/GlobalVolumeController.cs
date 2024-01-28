using Pixeye.Actors;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolumeController : MonoCached
{
    public Volume volume;
    public ColorAdjustments colorAdjustments;

    protected override void Setup()
    {
        base.Setup();
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out colorAdjustments);
    }
    
    public void SetSaturation(float weight)
    {
        colorAdjustments.saturation.value = weight;
    }
}