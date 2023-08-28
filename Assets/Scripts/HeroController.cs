using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _lives = 10f;
    [SerializeField] private float _jumpForce = 5f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private bool _isGround = false;

    public static HeroController Instance { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        Instance = this;
    }

    private States State
    {
        get { return (States)_animator.GetInteger("state"); }
        set { _animator.SetInteger("state", (int)value); }
    }

    private void FixedUpdate()
    {
        CheckGround();    
    }

    private void Update()
    {
        if (_isGround) State = States.Idle;

        if (Input.GetButton("Horizontal"))
            Run();
        if (_isGround && Input.GetButton("Jump"))
            Jump();
    }

    private void Run()
    {
        if (_isGround) State = States.Run;

        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);
        _spriteRenderer.flipX = direction.x < 0.0f;
    }

    private void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse); 
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        _isGround = collider.Length > 1;

        if (!_isGround) State = States.Jump; 
    }

    public void GetDamage()
    {

        _lives -= 1;
        Debug.Log("Количество жизней - " + _lives);
    }

    public enum States
    {
        Idle,
        Run,
        Jump
    }
}
