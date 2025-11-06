using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    Input input;
    public PlayerMovement movement;
    Vector2 movementInput;
    public Animator playerAnimator;
    public PlayerHealthSystem health;
    public AnimManager animManager;

    public GameObject TankUltimateBall, tankDefensiveExplotion;
    public Transform proyectileSpawnpoint;
    public bool canUseAbilities;

    void Awake()
    {
        combat.player = this;

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

    public PlayerCombat combat;
    void SetUpCombat()
    {
        input.Player.Attack.performed += context => combat.attackAbility.Activate(this);
        combat.attackAbility.Initialize(this);
        input.Player.Attack.canceled += context => combat.attackAbility.Deactivate(this);
        input.Player.Defend.performed += context => combat.defenseAbility.Activate(this);
        combat.defenseAbility.Initialize(this);
        input.Player.Special.performed += context => combat.specialAbility.Activate(this);
        combat.specialAbility.Initialize(this);
        input.Player.Ultimate.performed += context => combat.ultimateAbility.Activate(this);
        combat.ultimateAbility.Initialize(this);
    }

    public IEnumerator ActiveAbility(float time, PlayerAbility ability)
    {
        yield return new WaitForSeconds(time);
        ability.Deactivate(this);
    }
}