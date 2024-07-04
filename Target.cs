using UnityEngine;

public class Target : MonoBehaviour
{
    private Vector3 _direction;
    private bool _leftmostPoint;
    private float _startingPosition;

    private void Start()
    {
        _startingPosition = transform.position.x;
        _leftmostPoint = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float speed = 0.1f;
        float lengthOfMovement = 10;

        if (_leftmostPoint)
        {
            MoveInOneDirection(speed * -1);
        
            if (transform.position.x <= _startingPosition)
            {
                _leftmostPoint = false;
            }
        }
        else if (_leftmostPoint == false)
        {
            MoveInOneDirection(speed);

            if (transform.position.x >= _startingPosition + lengthOfMovement)
            {
                _leftmostPoint = true;
            }
        }
    }

    private void MoveInOneDirection(float speed)
    {
        _direction.x = speed;
        
        transform.Translate(_direction);
    }
}