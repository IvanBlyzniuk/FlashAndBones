using UnityEngine;
using App.World.Entity.Player.PlayerComponents;
using App.World.UI;

namespace App.Systems.Input
{
    public class InputSystem : MonoBehaviour
    {
        
        private Camera mainCamera;
        private Player player;
        private Pauser pauser;

        private void Update()
        {
            if (HandlePauseInput()) return;
            HandleAimInput();
            HandleMoveInput();
            HandleShootInput();
        }
        public void Init(Camera mainCamera,Player player, Pauser pauser)
        {
            this.mainCamera = mainCamera;
            this.player = player;
            this.pauser = pauser;
        }

        private bool HandlePauseInput()
        {
            if (!UnityEngine.Input.GetKeyDown(KeyCode.Escape)) return pauser.IsPaused;

            if (pauser.IsPaused)
                pauser.Unpause();
            else
                pauser.Pause();

            return pauser.IsPaused;
        }

        private Vector3 GetMousePositionInWorld() 
        {
            Vector3 mouseOnScreenPos = UnityEngine.Input.mousePosition;
            mouseOnScreenPos = mainCamera.ScreenToWorldPoint(mouseOnScreenPos);
            mouseOnScreenPos.z = 0f;
            return mouseOnScreenPos;
        }
        private float GetDirectionAngle()
        {
            Vector3 lookDirection;
            if ((GetMousePositionInWorld() - player.WeaponAnchor.position).magnitude < (player.ShootPosition.position - player.WeaponAnchor.position).magnitude*2.2)
            {
                lookDirection = GetMousePositionInWorld() - player.WeaponAnchor.position;
            }
            else
            {
                lookDirection = GetMousePositionInWorld() - player.ShootPosition.position;
            }
                float rads = Mathf.Atan2(lookDirection.y, lookDirection.x);
                float direction = rads * Mathf.Rad2Deg;
            return direction;
        }
        private void HandleAimInput()
        {
            float aimAngle = GetDirectionAngle();
            float cursorPos = UnityEngine.Input.mousePosition.x;
            float playerPosInScreen = mainCamera.WorldToScreenPoint(player.PlayerTransform.position).x;
            player.AimEvent.CallAimEvent(aimAngle,playerPosInScreen,cursorPos);
        }
        private void HandleMoveInput()
        {
            float horizontalMove = UnityEngine.Input.GetAxis("Horizontal");
            float verticalMove = UnityEngine.Input.GetAxis("Vertical");

            Vector2 movingDirection = new Vector2(horizontalMove, verticalMove).normalized;


            
            if (movingDirection == Vector2.zero)
            {
                player.StandEvent.CallStandEvent();
               
            }
            else
            {
                player.MovementEvent.CallMovementEvent(movingDirection,player.MovementSpeed);
                //player.MakeStepSound();
            }
        }
        private void HandleShootInput()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                Debug.Log("Shooting");
                player.Weapon.ShootEvent.CallShootEvent();
            }
        }
    }
}
