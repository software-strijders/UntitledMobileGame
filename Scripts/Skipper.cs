using Godot;

namespace UntitledMobileGame.Scripts
{
    public class Skipper : KinematicBody
    {
        [Export] public int Gravity = 10;
    
        private Vector3 _velocity = Vector3.Zero;

        public override void _PhysicsProcess(float delta)
        {
            _velocity.y -= Gravity * delta;
            _velocity = MoveAndSlide(_velocity, Vector3.Up);
        }
    }
}
