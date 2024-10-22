using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerController
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour, IMoveable
    {
        [SerializeField, Range(0.1f, 20f)] private float _moveSpeed;

        private Camera _cam;
        private Rigidbody _rb;
        
        private Vector3 _moveDirection;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _cam = Camera.main;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Rotate(hitInfo.point);
            }
        }

        public void Move()
        {
            if (_moveDirection.sqrMagnitude < 0.1f)
                return;

            Vector3 offset = _moveDirection * _moveSpeed * Time.deltaTime;
            _rb.MovePosition(_rb.position + offset);
        }

        public void Rotate(Vector3 targetPoint)
        {
            Vector3 direction = (targetPoint - transform.position).normalized;

            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }

        public void SetMoveDirection(Vector2 direction)
        {
            _moveDirection = new Vector3(direction.x, 0, direction.y);
        }
    }
}


