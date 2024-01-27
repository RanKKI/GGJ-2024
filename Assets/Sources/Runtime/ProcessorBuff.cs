using Pixeye.Actors;
using UnityEngine;

sealed class ProcessorBuff : Processor, ITick, IReceive<SignalBuffAdded>
{

	readonly Group<ComponentObject, ComponentPlayer> players;

	public void Tick(float dt)
	{
		var now = UnityEngine.Time.timeAsDouble;
		for (int i = 0; i < players.length; i++)
		{
			var cPlayer = players[i].ComponentPlayer();
			for (int j = 0; j < cPlayer.buffs.Count; j++)
			{
				var buff = cPlayer.buffs[j];
				var isExpired = buff.validTo < now;
				if (isExpired)
				{
					cPlayer.buffs.RemoveAt(j);
					j--;
				}
			}
		}
	}

	public void HandleSignal(in SignalBuffAdded arg)
	{
		var cPlayer = arg.player.ComponentPlayer();
		for (int i = 0; i < arg.buffs.Count; i++)
		{
			var buff = arg.buffs[i];
			Debug.Log("Add buff " + buff.name + " to " + cPlayer.name);
			cPlayer.buffs.Add(buff.Start());
		}
	}

}