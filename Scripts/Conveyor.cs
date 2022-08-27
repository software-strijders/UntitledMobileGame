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

        private FaceDirection _faceDirection;

        public Conveyor()
        {
            _moveables = new List<Spatial>();
        }

        public override void _Ready()
        {
            var rotationsY = RotationDegrees.y <= 0 
                ? Math.Floor(RotationDegrees.y) 
                : Math.Ceiling(RotationDegrees.y);
            
            // Some stuff is going wrong here 
            switch (rotationsY)
            {
                case 0:
                    _faceDirection = FaceDirection.Top;
                    break;
                case 90:
                    _faceDirection = FaceDirection.Right;
                    break;
                case -180:
                    _faceDirection = FaceDirection.Bottom;
                    break;
                case -90:
                    _faceDirection = FaceDirection.Left;
                    break;
            }
        }

        public override void _PhysicsProcess(float delta)
        {
            foreach (var spatial in _moveables)
            {
                Convey(spatial, delta);
            }
        }

        private void Convey(Spatial spatial, float delta)
        {
            var translation = spatial.Translation;
            var amount = 1f * delta;
            switch (_faceDirection)
            {
                case FaceDirection.Top:
                    translation.z += Direction == ConveyDirection.Down ? amount : -amount;
                    break;
                case FaceDirection.Right:
                    translation.x += Direction == ConveyDirection.Down ? amount : -amount;
                    break;
                case FaceDirection.Bottom:
                    translation.z += Direction == ConveyDirection.Down ? -amount : amount;
                    break;
                case FaceDirection.Left:
                    translation.x += Direction == ConveyDirection.Down ? -amount : amount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            spatial.Translation = translation;
        }

        // ReSharper disable once UnusedMember.Local
        private void OnConveyorBodyEntered(Node body)
        {
            if (body.IsInGroup("Unmovable")) return;
            if (!(body is Spatial spatial)) return;
            if (body is Conveyor) return;

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

    public enum FaceDirection
    {
        Left,
        Right,
        Top,
        Bottom,
    }
}
