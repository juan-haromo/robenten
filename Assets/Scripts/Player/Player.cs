using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    Input input;
    [SerializeField] PlayerMovement movement;
    Vector2 movementInput;

    public AnimatorController animController;

    void Awake()
    {
        input = new Input();
        input.Player.Enable();
        SetUpCombat();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        movementInput = input.Player.Move.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        movement.Move(movementInput.x, movementInput.y);
    }

    [SerializeField] PlayerCombat combat;
    void SetUpCombat()
    {
        input.Player.Attack.performed += context => combat.attackAbility.Activate(this);
        input.Player.Defend.performed += context => combat.defenseAbility.Activate(this);
        input.Player.Special.performed += context => combat.specialAbility.Activate(this);
        input.Player.Ultimate.performed += context => combat.ultimateAbility.Activate(this);
    }
}