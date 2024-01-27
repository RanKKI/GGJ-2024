
using Pixeye.Actors;

sealed class ProcessorGameEnd : Processor, IReceive<SignalGameEnd>
{
	public void HandleSignal(in SignalGameEnd arg)
	{
		var winners = arg.winner;
		for (var i = 0; i < winners.Length; i++)
		{
			var winner = winners[i];
			var cPlayer = winner.ComponentPlayer();
			if (cPlayer == null) continue;
			cPlayer.isActive = false;
		}
		UnityEngine.Time.timeScale = 0;
	}

}