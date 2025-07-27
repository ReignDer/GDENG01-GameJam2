using System;
using UnityEngine;
using UnityUtils;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputReader inputReader;
    
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float gravity = 30f;

    private Rigidbody rb;

    private Transform tr;

    private Vector3 savedVelocity, velocity, momentum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = transform;
        inputReader.EnablePlayerInput();
        //rb.useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = CalculateVelocity();
        HandleGravity();
        velocity += momentum;
        rb.linearVelocity = velocity;
        savedVelocity = velocity;
        if (Math.Abs(velocity.x) > 0.1f)
        {
            float targetAngle = velocity.x > 0 ? 90f : -90f;
            transform.localRotation = Quaternion.Euler(0, targetAngle, 0);
        }

    }

    private void HandleGravity()
    {
        Vector3 verticalMomentum = VectorMath.ExtractDotVector(momentum, tr.up);
        Vector3 horizontalMomentum = momentum - verticalMomentum;
        verticalMomentum -= tr.up * (gravity * Time.deltaTime);
        if (VectorMath.GetDotProduct(verticalMomentum, tr.up) < 0f)
        {
            Debug.Log("Grounded");
            verticalMomentum = Vector3.zero;
        }
        momentum = horizontalMomentum + verticalMomentum;
    }
    public Vector3 GetMovementVelocity() => savedVelocity;
    private Vector3 CalculateVelocity() => CalculateDirection() * movementSpeed;

    private Vector3 CalculateDirection()
    {
        Vector3 direction = inputReader.Direction.x * Vector3.right;

        return direction.magnitude > 1f ? direction.normalized : direction;
    }
}
