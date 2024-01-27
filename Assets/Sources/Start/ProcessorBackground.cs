using Pixeye.Actors;
using UnityEngine;

public class ProcessorBackground : Processor, IReceive<SignalChangeBack>
{
    public void HandleSignal(in SignalChangeBack arg)
    {
        var layer = arg.layer;
        UpdateSpotlight(layer, arg.spotlight);
        UpdateSpotlightCharacter(layer, arg.SpotlightCharacter);
    }

    private void UpdateSpotlight(StartLayer layer, SpotlightType spotlight)
    {

        if (spotlight == SpotlightType.Skip)
        {
            return;
        }
        if (spotlight == SpotlightType.None)
        {
            layer.spotlight.sprite = null;
        }
        else
        {
            var asset = GalGameAssets.Spotlights[(int)spotlight];
            layer.spotlight.sprite = asset;
        }
    }

    private void UpdateSpotlightCharacter(StartLayer layer, int character)
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