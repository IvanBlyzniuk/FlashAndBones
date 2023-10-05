using App.World.Entity.Player.Events;
using UnityEngine;
namespace App.World.Entity.Player.PlayerComponents
{
    public class Aim : MonoBehaviour
    {
        [SerializeField]
        private Transform weaponAnchorTransform;
        [SerializeField]
        private AimEvent aimEvent;

        private void OnEnable()
        {
            aimEvent.OnAim += OnAimWeapon;
        }
        private void OnDisable()
        {
            aimEvent.OnAim -= OnAimWeapon;
        }
        private void OnAimWeapon(AimEvent ev, AimEventArgs args)
        {
            AimWeaponWithMouse(weaponAnchorTransform, args.angle, args.playerPos, args.mousePos);
        }
        private void AimWeaponWithMouse(Transform weaponAnchorTransform, float aimAngle, float playerPos, float cursorPos)
        {
            weaponAnchorTransform.eulerAngles = new Vector3(0f, 0f, aimAngle);
            if (cursorPos >= playerPos)
            {
                weaponAnchorTransform.localScale = new Vector3(1f, 1f, 0f);
            }
            else
            {
                weaponAnchorTransform.localScale = new Vector3(1f, -1f, 0f);
            }

        }



    }
}