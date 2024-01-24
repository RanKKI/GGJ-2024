using System.Collections.Generic;
using Pixeye.Actors;
using UnityEngine;

public class GameLayer : Layer<GameLayer>
{

    public new Collider2D collider2D;

    protected override void Setup()
    {

        Add(new ProcessorCollider(new List<Collider2D> { collider2D }));
        Add<ProcessorPlayer>();

        Game.Create.Board();
    }
}
