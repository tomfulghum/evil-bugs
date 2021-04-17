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

    Vector2 movementInput;

    Rigidbody rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var direction = new Vector3(movementInput.x, 0, movementInput.y);
        direction = Quaternion.LookRotation(Vector3.ProjectOnPlane(lookTransform.forward, Vector3.up)) * direction;

        var force = direction * acceleration;
        rb.AddForce(force, ForceMode.Acceleration);

        var planarVelocity = Vector3.ProjectOnPlane(rb.velocity, Vector3.up);
        planarVelocity = Vector3.ClampMagnitude(planarVelocity, maxVelocity);
        var verticalVelocity = Mathf.Clamp(rb.velocity.y, -maxUpwardVelocity, maxUpwardVelocity);
        rb.velocity = new Vector3(planarVelocity.x, verticalVelocity, planarVelocity.z);
    }

    public void OnFlap()
    {
        float forceMultiplier = 1f;
        if (rb.velocity.y > 0)
            forceMultiplier = 1f - Mathf.InverseLerp(0f, maxUpwardVelocity, rb.velocity.y);

        rb.AddForce(0, flapForce * forceMultiplier, 0, ForceMode.Acceleration); 
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
}
