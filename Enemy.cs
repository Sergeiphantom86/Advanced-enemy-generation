using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform _targetPosition;
    private float _speed = 0.1f;

    private void FixedUpdate()
    {
        transform.LookAt(_targetPosition);

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition.position, _speed);
    }

    public void SetTargetPosition(Transform target)
    {
        _targetPosition = target;
    }
}