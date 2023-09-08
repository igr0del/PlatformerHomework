using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Collider2D))]

public class HeroController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _lives = 10f;
    [SerializeField] private float _jumpForce = 5f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private Vector3 _input;
    private float _checkDistance = 0.3f;
    private bool _isGround = false;
    private bool _isLeft = false;
    private string _isRunning = "IsRunning";
    private string _isJumping = "IsJumping";

    public static HeroController Instance { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        Instance = this;
    }

    private void FixedUpdate()
    {
        CheckGround();    
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            Run();
            _animator.SetBool(_isRunning, true);
        }
        else
        {
            _animator.SetBool(_isRunning, false);
        }

        if (_isGround && Input.GetButton("Jump"))
        {
            Jump();
            _animator.SetBool(_isJumping, true);
        }
        else
        {
            _animator.SetBool(_isJumping, false);
        }
    }

    private void Run()
    {
        float direction = Input.GetAxis("Horizontal");
        _input = new Vector2(direction, 0);

        transform.position += _input * _speed * Time.deltaTime;

        if (_isLeft == true && direction > 0)
            Flip();
        else if (_isLeft == false && direction < 0)
            Flip();
    }

    private void Flip()
    {
        _isLeft = !_isLeft;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    private void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse); 
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, _checkDistance);
        _isGround = collider.Length > 1;
    }

    public void GetDamage()
    {

        _lives -= 1;
        Debug.Log("Количество жизней - " + _lives);
    }
}
