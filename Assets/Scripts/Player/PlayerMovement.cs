using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform combatCamera;
    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerObj;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform combatLookAt;

    [Header("Speed")]
    
    [SerializeField] float rotationSpeed = 5.0f;
    [SerializeField] float acceleration = 5.0f;
    [SerializeField] float maxMoveSpeed = 5.0f;

    Vector3 viewDirection;
    Vector3 inputDirection;
    bool wasPressingKeys = false;

    CharacterController controller;

    void Start()
    {
        rb.freezeRotation = true;
    }

    public void Move(float horizontalInput, float verticalInput)
    {
        viewDirection = combatLookAt.position - new Vector3(combatCamera.position.x, combatLookAt.position.y, combatCamera.position.z);
        orientation.forward = viewDirection.normalized;

        playerObj.forward =  viewDirection.normalized;

        inputDirection = (orientation.forward * verticalInput) + (orientation.right * horizontalInput);

        if (inputDirection != Vector3.zero)
        {
            wasPressingKeys = true;
        }
        else if (wasPressingKeys)
        {
            wasPressingKeys = false;
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

        //Speed control
        rb.AddForce(acceleration * 10 * inputDirection.normalized, ForceMode.Force);
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        if(flatVel.magnitude > maxMoveSpeed)
        {
            rb.linearVelocity = flatVel.normalized * maxMoveSpeed;
        }
    }
}
