using Pixeye.Actors;

public class GameLayer : Layer<GameLayer>
{

    protected override void Setup()
    {

        Add<ProcessorCollider>();
        Add<ProcessorPlayer>();

        Game.Create.Board();
    }
}
