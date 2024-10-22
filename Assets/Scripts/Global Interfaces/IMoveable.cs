using UnityEngine;

public interface IMoveable
{
    void Move();
    void SetMoveDirection(Vector2 direction);
    void Rotate(Vector3 targetPoint);
}
