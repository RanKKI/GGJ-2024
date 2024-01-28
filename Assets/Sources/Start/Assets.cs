using Pixeye.Actors;
using UnityEngine;

public class GalGameAssets
{
    public static Sprite[] Spotlights = Box.LoadAll<Sprite>("Dialog/spotlight", 4);

    public static Sprite[] SpotlightCharacter = Box.LoadAll<Sprite>("Dialog/spotlight_bg", 10);


    public static Sprite GamePlayTutu1 = Box.Load<Sprite>("Dialog/Tutorial");
    public static Sprite GamePlayTutu2 = Box.Load<Sprite>("Dialog/GamePlay_tut");
}
