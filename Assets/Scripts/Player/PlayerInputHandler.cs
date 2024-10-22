using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace PlayerController
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private IMoveable _moveable;
        private IWeapon _weapon;

        private bool _isAutoShoot;

        private void Awake()
        {
            _moveable = GetComponent<IMoveable>();
            _weapon = GetComponentInChildren<IWeapon>();
        }

        private void Update()
        {
            if (_isAutoShoot)
            {
                _weapon?.Shoot();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 inputDirection = context.ReadValue<Vector2>();
            _moveable.SetMoveDirection(inputDirection);
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            switch(_weapon)
            {
                case BasicPistol:
                    _isAutoShoot = false;
                    break;
                case BasicRifle:
                    _isAutoShoot = true;
                    break;
            }

            if(context.started && !_isAutoShoot)
            {
                _weapon.Shoot();
            }else if (context.canceled && _isAutoShoot)
            {
                _isAutoShoot = false;
            }                
        }

        public void OnReload(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _weapon.Reload();
            }
        }
    }
}