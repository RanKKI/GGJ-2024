using System;
using Pixeye.Actors;

[Serializable]
public struct SignalChangeHappiness
{
    public ent target;
    public int count;
}