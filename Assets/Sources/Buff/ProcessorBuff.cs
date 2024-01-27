using System.Collections.Generic;
using System.Linq;
using Pixeye.Actors;
using UnityEngine;

sealed class ProcessorBuff : Processor, IReceive<SignalBuffAdded>
{

	readonly Group<ComponentObject, ComponentPlayer> players;

	public void HandleSignal(in SignalBuffAdded arg)
	{
		var cPlayer = arg.player.ComponentPlayer();
		List<Buff> buffs = cPlayer.buffs.ToList();
		for (int i = 0; i < arg.buffs.Length; i++)
		{
			var buff = arg.buffs[i];
			Debug.Log("Add buff " + buff.name + " to " + cPlayer.name);
			buffs.Add(buff.Start());
		}
		cPlayer.buffs = buffs.ToArray();
	}

}