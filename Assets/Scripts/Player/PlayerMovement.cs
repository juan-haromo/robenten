using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform explorationCamera;
    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerObj;
    [SerializeField] Rigidbody rb;

    [Header("Speed")]
    
    [SerializeField] float rotationSpeed = 5.0f;
    public float acceleration = 5.0f;
    public float maxMoveSpeed = 5.0f;

    Vector3 viewDirection;
    Vector3 inputDirection;
    bool wasPressingKeys = false;

    void Start()
    {
        rb.freezeRotation = true;
    }

    public void Move(float horizontalInput, float verticalInput)
    {
        //rotate direction
        viewDirection = player.position - new Vector3(explorationCamera.position.x, player.position.y, explorationCamera.position.z);

        orientation.forward = viewDirection.normalized;

        inputDirection = (orientation.forward * verticalInput) + (orientation.right * horizontalInput);
        if (inputDirection != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
            wasPressingKeys = true;
        } 
        else if (wasPressingKeys)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            wasPressingKeys = false;
        }   
        rb.AddForce(acceleration * 10 * inputDirection.normalized, ForceMode.Force);
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        if(flatVel.magnitude > maxMoveSpeed)
        {
            rb.linearVelocity = flatVel.normalized * maxMoveSpeed;
        }
    }
}
