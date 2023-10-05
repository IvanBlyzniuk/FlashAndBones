using App.World.Entity.Player.Events;
using UnityEngine;
namespace App.World.Entity.Player.PlayerComponents
{
    public class PlayerAnimationsController : MonoBehaviour
    {
        private Player player;

        private void Awake()
        {
            player = GetComponent<Player>();
        }
        private void OnEnable()
        {
            player.StandEvent.OnStand += AnimateOnStand;
            player.AimEvent.OnAim += AnimateOnAim;
            player.MovementEvent.OnMove += AnimateOnMove;
        }
        private void OnDisable()
        {
            player.StandEvent.OnStand -= AnimateOnStand;
            player.AimEvent.OnAim -= AnimateOnAim;
            player.MovementEvent.OnMove -= AnimateOnMove;
        }

        private void AnimateOnStand(StandEvent ev)
        {
            SetStandAnimationParams();
        }
        private void AnimateOnAim(AimEvent ev,AimEventArgs args)
        {
            SetAimAnimationParams(args.playerPos, args.mousePos);
        }
        private void AnimateOnMove(MovementEvent ev, MovementEventArgs args)
        {
            SetMovementAnimationParams();
        }
        public void SetStandAnimationParams()
        {
            player.PAnimator.SetBool("isIdle", true);
            player.PAnimator.SetBool("isMoving", false);
        }
        public void SetAimAnimationParams(float playerPos, float cursorPos)
        {
            if (cursorPos >= playerPos)
            {
                player.PAnimator.SetBool("aimRight", true);
                player.PAnimator.SetBool("aimLeft", false);
            }
            else
            {
                player.PAnimator.SetBool("aimRight", false);
                player.PAnimator.SetBool("aimLeft", true);
            }
        }
        public void SetMovementAnimationParams()
        {
            player.PAnimator.SetBool("isIdle", false);
            player.PAnimator.SetBool("isMoving", true);
        }
    }
}