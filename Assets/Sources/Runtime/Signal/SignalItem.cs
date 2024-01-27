
using Pixeye.Actors;
using UnityEngine;

public struct SignalHoldItem
{
	public ent item;
	public ent holder;
}


public struct SignalTouchItem
{
	public ent item;
	public ent player;
}

public struct SignalFireItem
{
	public ent item;
	public ent holder;
}


public struct SignalDisposeItem
{
	public ent item;

	public GameObject obj;

}
