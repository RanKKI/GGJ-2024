
using System;
using Pixeye.Actors;

[Serializable]
public struct SignalChangeHealth
{
	public ent target;
	public float count;
	public bool ignoreDeath;
}