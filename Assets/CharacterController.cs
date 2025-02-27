using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public gamemanager _gamemanager;
    public float acceleration = 50f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 20f;
    public float jumpBoostForce = 8f;

    [Header("Debug Stuff")] 
    public bool isGrounded;
    
    Rigidbody rb;

    private Collider _collider;
    private Animator _animator;
    private bool canJumpDestroy = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAmount = Input.GetAxis("Horizontal");
        rb.linearVelocity += Vector3.right * (horizontalAmount * Time.deltaTime * acceleration);

        float horizontalSpeed = rb.linearVelocity.x;
        horizontalSpeed = Mathf.Clamp(horizontalSpeed, -maxSpeed, maxSpeed);

        Vector3 newVelocity = rb.linearVelocity;
        newVelocity.x = horizontalSpeed;
        rb.linearVelocity = newVelocity;

        // Test if character is on ground surface
        Vector3 startPoint = transform.position;
        float castDistance = _collider.bounds.extents.y + 0.03f;
        
        isGrounded = Physics.Raycast(startPoint, Vector3.down, castDistance);
        
        Color color = isGrounded ? Color.green : Color.red;
        Debug.DrawLine(startPoint, startPoint + castDistance * Vector3.down, color, 0f, false);
        Color colorJump = canJumpDestroy ? Color.green : Color.red;
        Debug.DrawLine(startPoint, startPoint + castDistance*2 * Vector3.up, colorJump, 0f, false);
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.VelocityChange);
            canJumpDestroy = true;
        }
        else if (Input.GetKey(KeyCode.Space) && !isGrounded)
        {
            if (rb.linearVelocity.y > 0)
            {
                rb.AddForce(Vector3.up * jumpBoostForce, ForceMode.Acceleration);
            }
            
            if (canJumpDestroy && Physics.Raycast(startPoint, Vector3.up, out RaycastHit hitInfo, castDistance*2))
            {
                if (hitInfo.transform.gameObject.CompareTag("Brick"))
                {
                    Destroy(hitInfo.transform.gameObject);
                    _gamemanager.addPoints(100);
                    canJumpDestroy = false;
                }
                else if (hitInfo.transform.gameObject.CompareTag("QuestionBlock"))
                {
                    _gamemanager.addCoin(1);
                    _gamemanager.addPoints(100);
                    canJumpDestroy = false;
                }
            }
        }

        if (horizontalAmount == 0f)
        {
            Vector3 decayedVelocity = rb.linearVelocity;
            decayedVelocity.x *= 1f - Time.deltaTime * 10f;
            rb.linearVelocity = decayedVelocity;
        }
        else
        {
            float yawRotation = (horizontalAmount > 0f ? 90f : -90f);
            Quaternion rotation = Quaternion.Euler(0f, yawRotation, 0f);
            transform.rotation = rotation;
        }
        
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        _animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        _animator.SetBool("InAir", !isGrounded);
    }
}
