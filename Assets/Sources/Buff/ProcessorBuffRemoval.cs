using System.Collections.Generic;
using System.Linq;
using Pixeye.Actors;
using UnityEngine;

sealed class ProcessorBuffRemoval : Processor, ITick
{

	readonly Group<ComponentObject, ComponentPlayer> players;

	public void Tick(float dt)
	{
		var now = UnityEngine.Time.timeAsDouble;
		for (int i = 0; i < players.length; i++)
		{
			var cPlayer = players[i].ComponentPlayer();
			List<Buff> buffs = cPlayer.buffs.ToList();
			for (int j = 0; j < buffs.Count; j++)
			{
				var buff = cPlayer.buffs[j];
				var isExpired = buff.validTo > 0 && buff.validTo < now;
				if (isExpired)
				{
					GameLayer.Send(new SignalBuffRemoved
					{
						buff = buff,
						player = players[i]
					});
					buffs.RemoveAt(j);
					j--;
				}
			}
			cPlayer.buffs = buffs.ToArray();
		}
	}

}