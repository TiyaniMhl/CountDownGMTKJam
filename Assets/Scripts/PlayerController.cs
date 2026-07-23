using UnityEngine;

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
    public Transform gun;
    public GameObject bulletPool;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    

    private float _horizontalInput;
    private bool _jumpPressed;
    private bool _shootPressed;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _inGameRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        { 
            _jumpPressed = true;
        }
        if (Input.GetButtonDown("Fire1"))
        { 
            Shoot();
        }
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

    public void Shoot()
    {
        var gunPosition = gun.position;
        Quaternion rot = Quaternion.LookRotation((gunPosition - gameObject.transform.position), Vector2.up);
        rot *= Quaternion.Euler(0f,-90f, 0f);
        Instantiate(bulletPrefab, gunPosition, rot, bulletPool.transform);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

}
