using Pixeye.Actors;

public class Tag : ITag
{
    [TagField]
    public const int None = 0;

    // Objects
    [TagField]
    public const int Item = 100;
    [TagField]
    public const int Player = 101;

}