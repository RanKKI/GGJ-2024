using Pixeye.Actors;

public class ProcessorEndBackground : Processor, IReceive<SignalEndChangeBack>
{
    public void HandleSignal(in SignalEndChangeBack arg)
    {
        var layer = arg.layer;
        UpdateSpotlightCharacter(layer, arg.SpotlightCharacter);
    }

    private void UpdateSpotlightCharacter(GameEndLayer layer, int character)
    {
        if (character == -2)
        {
            return;
        }
        if (character == -1)
        {
            layer.spotlightCharacter.sprite = null;
        }
        else
        {
            var asset = GalGameAssets.SpotlightCharacter[character];
            layer.spotlightCharacter.sprite = asset;
        }
    }
}
