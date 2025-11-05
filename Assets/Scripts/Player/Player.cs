using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    Input input;
    public PlayerMovement movement;
    Vector2 movementInput;
    public Animator playerAnimator;
    int health = 100;
    public PlayerHealthSystem health;
    public AnimManager animManager;

    Vector2 movementInput;

    public GameObject TankUltimateBall, tankDefensiveExplotion;

    void Awake()
    {
        input = new Input();
        SetUpCombat();
        input.Player.Enable();

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

    public PlayerCombat combat;
    void SetUpCombat()
    {
        input.Player.Attack.performed += context => combat.attackAbility.Activate(this);
        combat.attackAbility.Initialize(this);
        input.Player.Defend.performed += context => combat.defenseAbility.Activate(this);
        combat.defenseAbility.Initialize(this);
        input.Player.Special.performed += context => combat.specialAbility.Activate(this);
        combat.specialAbility.Initialize(this);
        input.Player.Ultimate.performed += context => combat.ultimateAbility.Activate(this);
        combat.ultimateAbility.Initialize(this);
    }

    public IEnumerator UltimateTimeActive(float time)
    {
        yield return new WaitForSeconds(time);
        combat.ultimateAbility.ExitUltimate(this);
    }

    public IEnumerator ActiveAbility(float time, PlayerAbility ability)
    {
        yield return new WaitForSeconds(time);
        ability.Deactivate(this);
    }

    public void Damage(int damage)
    {
        health -= damage;
    }
}