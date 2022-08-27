using System;
using System.Collections.Generic;
using Godot;

namespace UntitledMobileGame.Scripts
{
    public class Conveyor : Area
    {
        [Export] public int Speed = 5;
        [Export] public ConveyDirection Direction = ConveyDirection.Down;

        private readonly List<Spatial> _moveables;

        public Conveyor()
        {
            _moveables = new List<Spatial>();
        }

        public override void _PhysicsProcess(float delta)
        {
            foreach (var spatial in _moveables)
            {
                var translation = spatial.Translation;

                // This implementation is pretty naive; it doesn't take into account in which direction the
                // conveyor is facing. Which means it doesn't work if the conveyor is rotated.
                switch (Direction)
                {
                    case ConveyDirection.Down:
                        translation.z += 1f * delta;
                        break;
                    case ConveyDirection.Up:
                        translation.z -= 1f * delta;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                spatial.Translation = translation;
            }
        }

        // ReSharper disable once UnusedMember.Local
        private void OnConveyorBodyEntered(Node body)
        {
            if (!(body is Spatial spatial)) return;
            
            _moveables.Add(spatial);
        }

        // ReSharper disable once UnusedMember.Local
        private void OnConveyorBodyExited(Node body)
        {
            if (!(body is Spatial spatial)) return;
            
            _moveables.Remove(spatial);
        }
    }

    /// <summary>
    /// Tells the conveyor which direction to move the body.
    ///
    /// Note: A better name for this is probably needed.
    /// </summary>
    public enum ConveyDirection
    {
        Up,
        Down,
    }
}
