using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour
{
    private enum Direction
    {
        left,
        right,
    }

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Direction _direction;

    private Vector3 RotateDirection => _direction == Direction.left ? Vector3.up : Vector3.down; 

    private void Update()
    {
        transform.Rotate(RotateDirection * _speed * Time.deltaTime);
    }
}
