using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _animator;
    private AudioSource _footStepAudio;
    private float _turnSpeed = 20.0f;

    public Vector3 Direction { get; set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _footStepAudio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        LookAt(Direction);
        WalkTo(Direction);
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

        if(isWalking)
        {
            if (_footStepAudio.isPlaying) return;
            _footStepAudio.Play();
        }
        else
        {
            _footStepAudio.Stop();
        }
    }

    private void OnAnimatorMove()
    {
        _rb.MovePosition(_rb.position + Direction * _animator.deltaPosition.magnitude);
        _rb.MoveRotation(_rb.rotation);
    }
}
