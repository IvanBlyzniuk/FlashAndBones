using App.World.Entity.Player.Events;
using UnityEngine;
namespace App.World.Entity.Player.PlayerComponents
{
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;
        [SerializeField]
        private MovementEvent moveEvent;

        private void OnEnable()
        {
            moveEvent.OnMove += OnEntityMove;
        }
        private void OnDisable()
        {
            moveEvent.OnMove -= OnEntityMove;
        }
        private void OnEntityMove(MovementEvent ev, MovementEventArgs args)
        {
            Move(rb, args.direction, args.speed);
        }
        private void Move(Rigidbody2D body, Vector2 direction, float speed)
        {
            body.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
    }
}
