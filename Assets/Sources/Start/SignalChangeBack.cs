public enum SpotlightType
{
    Skip = -2,
    None = -1,
    Both = 0,
    Joker = 2,
    Monster = 3,
    MonsterOnly = 1,
}

public class SignalChangeBack
{
    public StartLayer layer;
    public SpotlightType spotlight = SpotlightType.Skip;
    public int SpotlightCharacter = -2;
}