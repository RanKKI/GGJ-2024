using Pixeye.Actors;
using UnityEngine.Rendering;

public class GlobalVolumeController : MonoCached
{
    public Volume volume;

    protected override void Setup()
    {
        base.Setup();
        volume = GetComponent<Volume>();
    }
    
    public void SetWeight(float weight)
    {
        volume.weight = weight;
    }
}