using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody2D _inGameRb;
    
    public static PlayerController Instance;
    public GameObject player;
    public GameObject gameCamera;
    
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    [Header("Ground check")]
    public float groundCheckRadius = 0.5f;
    public LayerMask whatIsGround;
    public Transform groundCheck;

    [Header("Gun")] 
    public Transform bulletSpawner;
    public GameObject bulletPoolPrefab;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public Transform cursor;
    public float cooldown = 2f;

    [Header("Gun Animator")] 
    public Transform armRenderer;

    public float maxLaserDistance = 20f;
    private LineRenderer _lineRenderer;
    private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int VerticalSpeed  = Animator.StringToHash("Vertical Speed");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private float _horizontalInput;
    private bool _jumpPressed;
    private bool _shootPressed;
    private GameObject _bulletPool;
    private bool _liveBullets;
    private Vector2 _localGunPos;
    private float _cooldownTimer;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _inGameRb = GetComponent<Rigidbody2D>();
        _bulletPool = Instantiate(bulletPoolPrefab);
        _animator = GetComponentInChildren<Animator>();
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        _lineRenderer.useWorldSpace = true;
        _cooldownTimer = 0;
    }


    private void OnDestroy()
    {
        Destroy(_bulletPool);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Jump();
    }
    
    

    void Update()
    {
        if (_cooldownTimer>0)
        {
            _cooldownTimer -= Time.deltaTime;
        }

        Aim();
        _horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        { 
            _jumpPressed = true;
        }
        if (Input.GetButtonDown("Fire1") && _cooldownTimer<=0)
        {
            Shoot();
        }
        _liveBullets = BulletPool.Instance.IsLive();
        Animate();
    }

    public void Animate()
    {
        _animator.SetFloat(Speed, Mathf.Abs(_horizontalInput));
        if (_horizontalInput > 0){
            transform.localScale = new Vector3(1, 1, 1);
            armRenderer.localScale = new Vector3(1, 1, 1);
        }
        else if (_horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            armRenderer.localScale = new Vector3(-1, 1, 1);
        }
        _animator.SetBool(Grounded, IsGrounded());
        _animator.SetFloat(VerticalSpeed, _inGameRb.linearVelocityY);
    }

    public void Move()
    {
        _inGameRb.linearVelocity = new Vector2(_horizontalInput * moveSpeed, _inGameRb.linearVelocity.y);
    }

    public void Jump()
    {
        if (_jumpPressed && IsGrounded())
        {
            _inGameRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _jumpPressed = false;
        }
    }

    public void Aim()
    {
        var gunPosition = bulletSpawner.position;
        armRenderer.rotation = Quaternion.LookRotation(cursor.position - gunPosition) * Quaternion.Euler(0f,-90f, 0f);
        if (_cooldownTimer>0)
        {
            _lineRenderer.enabled = false;
            return;
        }
        _lineRenderer.enabled = true;
        RaycastHit2D hit = Physics2D.Raycast(gunPosition, bulletSpawner.right, maxLaserDistance);
        _lineRenderer.SetPosition(0, gunPosition);
        if (hit.collider != null)
        {
            _lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            Vector2 endPos = gunPosition + (bulletSpawner.right * maxLaserDistance);
            _lineRenderer.SetPosition(1, endPos);
        }
    }

    public bool IsLive()
    {
        return _liveBullets;
    }

    public void Shoot()
    {
        if (!GameController.Instance.TryDecreaseBullets())
        {
            return;
        }

        _cooldownTimer = cooldown;
        var gunPosition = bulletSpawner.position;
        Quaternion rot = Quaternion.LookRotation(cursor.position - gunPosition, Vector2.up);
        rot *= Quaternion.Euler(0f,-90f, 0f);
        //Instantiate(bulletPrefab, gunPosition, rot, bulletPool.transform);
        BulletPool.Instance.Add(bulletPrefab, gunPosition, rot);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

}
