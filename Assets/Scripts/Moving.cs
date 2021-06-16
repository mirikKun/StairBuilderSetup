using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private float smoothRotation = 0.1f;
    [SerializeField] private float gravityPower = 6f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Transform cam;
    private float _smoothVelocity;
    private Animator _animator;
    private CharacterController _controller;
    private Vector3 _moveDir = new Vector3(0, 0, 0);
    private Vector3 _direction;
    private static readonly int AnimationPar = Animator.StringToHash("AnimationPar");

    private void Start()
    {
        _controller = GetComponentInParent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Move();
        Gravity();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        _direction = new Vector3(x, 0, z).normalized;
        if (_direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle =
                Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _smoothVelocity, smoothRotation);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            _animator.SetInteger(AnimationPar, 1);
            _moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            _controller.Move(_moveDir * (speed * Time.deltaTime));
        }
        else
        {
            _animator.SetInteger(AnimationPar, 0);
        }
    }

    private void Gravity()
    {
        _controller.Move(Vector3.down * (gravityPower * Time.deltaTime));
    }
}