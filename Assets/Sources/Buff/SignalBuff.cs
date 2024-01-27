
using Pixeye.Actors;

public struct SignalBuffAdded
{
	public Buff[] buffs;
	public ent player;
}



public struct SignalBuffRemoved
{
	public Buff buff;
	public ent player;
}
