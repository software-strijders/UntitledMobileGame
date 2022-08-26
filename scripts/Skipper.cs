using Godot;
using System;

public class Skipper : KinematicBody
{
    [Export] public int Gravity = 10;
    
    private Vector3 _velocity = Vector3.Zero;

    public override void _PhysicsProcess(float delta)
    {
        _velocity.y -= Gravity;
        
        _velocity = MoveAndSlide(_velocity, Vector3.Up);
    }
}
