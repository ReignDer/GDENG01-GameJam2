using System;
using ImprovedTimers;
using Unity.VisualScripting;
using UnityEngine;
using UnityUtils;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputReader inputReader;
    
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float gravity = 30f;
    public float airFriction = 0.5f;
    public float groundFriction = 100f;
    
    private bool jumpInputIsLocked, jumpKeyWasPressed, jumpKeyWasReleased, jumpKeyIsPressed;
    public float jumpSpeed = 10f;
    public float jumpDuration= 10f;
    CountdownTimer jumpTimer;
    
    private Rigidbody rb;

    private Transform tr;
    
    private Vector3 savedVelocity, velocity, momentum;
    RaycastSensor sensor;
    CapsuleCollider col;
    
    bool isGrounded;
    float baseSensorRange;
    Vector3 currentGroundAdjustmentVelocity;
    int currentLayer;
    bool isUsingExtendedSensorRange = true;
    private float stepHeightRatio = 0.01f;
    
    public event Action<Vector3> OnJump = delegate { };
    public event Action<Vector3> OnLand = delegate { };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = transform;
        col = GetComponent<CapsuleCollider>();
        inputReader.EnablePlayerInput();
        rb.useGravity = false;
        RecalibrateSensor();
        jumpTimer = new CountdownTimer(jumpDuration);
        
        inputReader.Jump += HandleJump;
    }

    private void HandleJump(bool isKeyPressed)
    {
        if (!jumpKeyIsPressed && isKeyPressed)
        {
            jumpKeyWasPressed = true;
        }

        if (jumpKeyIsPressed && !isKeyPressed)
        {
            jumpKeyWasReleased = true;
            jumpInputIsLocked = false;
        }
        
        jumpKeyIsPressed = isKeyPressed;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckForGround();
        HandleGravity();
        velocity = CalculateVelocity();
        velocity += momentum;
        rb.linearVelocity = velocity;
        savedVelocity = velocity;
        if (Math.Abs(velocity.x) > 0.1f)
        {
            float targetAngle = velocity.x > 0 ? 0f : 180f;
            transform.localRotation = Quaternion.Euler(0, targetAngle, 0);
        }
        
        ResetJumpKeys();

    }
    private void HandleJumping()
    {
        
        momentum = VectorMath.RemoveDotVector(momentum, tr.up);
        momentum += tr.up * jumpSpeed;
        jumpInputIsLocked = true;
        OnJump.Invoke(momentum);
    }
    
    public void ResetJumpKeys()
    {
        jumpKeyWasReleased = false;
        jumpKeyWasPressed = false;
    }
    private void HandleGravity()
    {
        Vector3 verticalMomentum = VectorMath.ExtractDotVector(momentum, tr.up);
        Vector3 horizontalMomentum = momentum - verticalMomentum;
        verticalMomentum -= tr.up * (gravity * Time.deltaTime);
        
        if (VectorMath.GetDotProduct(verticalMomentum, tr.up) < 0f && isGrounded)
        {
            Debug.Log("Grounded");
            verticalMomentum = Vector3.zero;
        }
        float friction = isGrounded ? groundFriction : airFriction;
        horizontalMomentum = Vector3.MoveTowards(horizontalMomentum,Vector3.zero, friction * Time.deltaTime);
        
        momentum = horizontalMomentum + verticalMomentum;
        
        if((jumpKeyIsPressed || jumpKeyWasPressed) && !jumpInputIsLocked)
            HandleJumping();
    }
    

    public Vector3 GetMovementVelocity() => savedVelocity;
    private Vector3 CalculateVelocity() => CalculateDirection() * movementSpeed;

    private Vector3 CalculateDirection()
    {
        Vector3 direction = inputReader.Direction.x * Vector3.right;

        return direction.magnitude > 1f ? direction.normalized : direction;
    }
    private void RecalculateSensorLayerMask()
    {
        int objectLayer = gameObject.layer;
        int layerMask = Physics.AllLayers;

        for (int i = 0; i < 32; i++)
        {
            if (Physics.GetIgnoreLayerCollision(objectLayer, i))
            {
                layerMask &= ~(1 << i);
            }
            
            int ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");  
            layerMask &= ~(1 << ignoreRaycastLayer);
            
            sensor.layerMask = layerMask;
            currentLayer = objectLayer;
        }
    }
    void RecalibrateSensor()
    {
        sensor ??= new RaycastSensor(tr);
        sensor.SetCastOrigin(col.bounds.center);
        sensor.SetCastDirection(RaycastSensor.CastDirection.Down);
        RecalculateSensorLayerMask();

        const float safetyDistanceFactor = 0.001f; //Small factor added to prevent clipping issues
        float length = col.height * (1f-stepHeightRatio) * 0.5f + col.height * stepHeightRatio;
        baseSensorRange = length * (1f+safetyDistanceFactor) * tr.localScale.x;
        sensor.castLength = length * tr.localScale.x;
    }
    
    /*Raycast*/
    public void CheckForGround()
    {
        if (currentLayer != this.gameObject.layer)
        {
            RecalculateSensorLayerMask();
        }
        
        currentGroundAdjustmentVelocity = Vector3.zero;
        sensor.castLength = isUsingExtendedSensorRange ? 
            baseSensorRange + col.height * tr.localScale.x * stepHeightRatio 
            : baseSensorRange;
        sensor.Cast();
        
        isGrounded = sensor.HasDetectedHit();
        //Debug.Log(sensor.GetCollider().name);
        if (!isGrounded) return;
        
        float distance = sensor.GetDistance();
        float upperLimit = col.height * tr.localScale.x * (1 - stepHeightRatio) * 0.5f;
        float middle = upperLimit + col.height * tr.localScale.x * stepHeightRatio; 
        float distanceToGo = middle - distance;
        
        currentGroundAdjustmentVelocity = tr.up * (distanceToGo * Time.fixedDeltaTime);
    }
}
