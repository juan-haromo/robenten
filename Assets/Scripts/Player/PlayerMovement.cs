using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform explorationCamera;
    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerObj;
    public Rigidbody rb;

    [Header("Speed")]

    public float rotationSpeed = 5.0f;


    public float currentAcceleration = 5.0f;
    public float currentMaxMoveSpeed = 5.0f;
    public float normalAcceleration = 5.0f;
    public float normalMaxMoveSpeed = 5.0f;

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
        rb.AddForce(currentAcceleration * 10 * inputDirection.normalized, ForceMode.Force);
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        if(flatVel.magnitude > currentMaxMoveSpeed)
        {
            rb.linearVelocity = flatVel.normalized * currentMaxMoveSpeed;
        }
    }
}
