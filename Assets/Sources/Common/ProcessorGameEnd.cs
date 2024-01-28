using Pixeye.Actors;

sealed class ProcessorGameEnd : Processor, IReceive<SignalGameEnd>
{
    public void HandleSignal(in SignalGameEnd arg)
    {
        var winners = arg.winner;
        if (winners.Length <= 0)
            return;
        var cPlayer = winners[0].ComponentPlayer();
        Game.OnGameFinished(cPlayer.playerType);
    }
}
