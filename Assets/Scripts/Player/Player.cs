using UnityEngine;

public class Player : MonoBehaviour
{
    Input input;
    [SerializeField] PlayerMovement movement;
    Vector2 movementInput;
    void Awake()
    {
        input = new Input();
        input.Player.Enable();
    }

    void Update()
    {
        movementInput = input.Player.Move.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        movement.Move(movementInput.x, movementInput.y);
    }
}