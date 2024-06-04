using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _animator;
    private float _turnSpeed = 20.0f;

    private Vector3 _direction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        ListenInput();
        LookAt(_direction);
        WalkTo(_direction);
    }

    private void ListenInput()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        _direction = new Vector3(horizontalInput, 0, verticalInput).normalized;
    }

    private void LookAt(Vector3 direction)
    {
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, direction, _turnSpeed * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(desiredForward);
    }

    private void WalkTo(Vector3 direction)
    {
        bool isWalking = direction != Vector3.zero;
        _animator.SetBool("IsWalking", isWalking);
    }

    private void OnAnimatorMove()
    {
        _rb.MovePosition(_rb.position + _direction * _animator.deltaPosition.magnitude);
        _rb.MoveRotation(_rb.rotation);
    }
}
