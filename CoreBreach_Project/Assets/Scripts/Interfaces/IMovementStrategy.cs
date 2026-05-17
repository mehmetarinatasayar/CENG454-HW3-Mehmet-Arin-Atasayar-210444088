using UnityEngine;

public interface IMovementStrategy
{
    void Move(Transform entityTransform, float speed);
}