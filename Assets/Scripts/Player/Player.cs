using UnityEngine;

public class Player : MonoBehaviour
{
    Input input;
    [SerializeField] PlayerMovement movement;
    Vector2 movementInput;
    public Transform proyectileSpawnpoint;
    public bool canUseAbilities;

    void Awake()
    {
        input = new Input();
        SetUpCombat();
        input.Player.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        canUseAbilities = true;
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
        input.Player.Attack.started += context => combat.attackAbility.Activate(this);
        input.Player.Attack.canceled += context => combat.attackAbility.Deactivate(this);
        input.Player.Defend.performed += context => combat.defenseAbility.Activate(this);
        input.Player.Special.performed += context => combat.specialAbility.Activate(this);
        input.Player.Ultimate.performed += context => combat.ultimateAbility.Activate(this);
    }
}