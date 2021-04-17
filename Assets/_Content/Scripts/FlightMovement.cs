using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlightMovement : MonoBehaviour
{
    [Header("Flap")]
    [SerializeField] float flapForce = 10f;
    [SerializeField] float maxUpwardVelocity = 5f;

    [Header("Move")]
    [SerializeField] float acceleration = 1f;
    [SerializeField] float maxVelocity = 5f;
    [SerializeField] Transform lookTransform;

    [Header("Rotation")]
    [SerializeField] float facingTorque = 1f;
    [SerializeField] float uprightTorque = 1f;
    [SerializeField] float angularCorrectionTime = 0.1f;

    [Header("Ground Detection")]
    [SerializeField] float groundCheckDistance = 1f;
    [SerializeField] LayerMask groundMask;

    [Header("Animation")]
    [SerializeField] Animator animator;

    Vector2 movementInput;
    Vector3 facingDirection;
    Vector3 angularCorrectionVelocity;
    bool isGrounded;

    GameManager manager;
    Rigidbody rb;

    void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out _, groundCheckDistance, groundMask, QueryTriggerInteraction.Ignore);
        animator.SetBool("Grounded", isGrounded);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var direction = new Vector3(movementInput.x, 0, movementInput.y);
        direction = Quaternion.LookRotation(Vector3.ProjectOnPlane(lookTransform.forward, Vector3.up)) * direction;

        if (direction != Vector3.zero)
            facingDirection = direction;

        if (manager && manager.inputDisabled)
            return;

        var force = direction * acceleration;
        rb.AddForce(force, ForceMode.Acceleration);

        var planarVelocity = Vector3.ProjectOnPlane(rb.velocity, Vector3.up);
        planarVelocity = Vector3.ClampMagnitude(planarVelocity, maxVelocity);
        var verticalVelocity = Mathf.Clamp(rb.velocity.y, -maxUpwardVelocity, maxUpwardVelocity);
        rb.velocity = new Vector3(planarVelocity.x, verticalVelocity, planarVelocity.z);

        var facingRotation = Quaternion.FromToRotation(Vector3.ProjectOnPlane(transform.forward, Vector3.up), facingDirection);
        var uprightRotation = Quaternion.FromToRotation(transform.up, Vector3.up);
        var facingTorqueVector = new Vector3(facingRotation.x, facingRotation.y, facingRotation.z) * facingTorque;
        var uprightTorqueVector = new Vector3(uprightRotation.x, uprightRotation.y, uprightRotation.z) * uprightTorque;
        rb.AddTorque(facingTorqueVector);
        rb.AddTorque(uprightTorqueVector);

        var correctedAngularVelocity = Vector3.SmoothDamp(rb.angularVelocity, Vector3.zero, ref angularCorrectionVelocity, angularCorrectionTime);
        rb.angularVelocity = correctedAngularVelocity;
    }

    public void OnFlap()
    {
        if (manager && manager.inputDisabled)
            return;

        float forceMultiplier = 1f;
        if (rb.velocity.y > 0)
            forceMultiplier = 1f - Mathf.InverseLerp(0f, maxUpwardVelocity, rb.velocity.y);

        rb.AddForce(0, flapForce * forceMultiplier, 0, ForceMode.Acceleration);
        animator.SetTrigger("Flap");
    }

    public void OnMove(InputValue value)
    {
        if (manager && manager.inputDisabled)
            return;

        movementInput = value.Get<Vector2>();
    }
}
