using UnityEngine;

public abstract class PlayerAbility : ScriptableObject
{
    [SerializeField] float cooldownTime;
    public float CooldownTime { get => cooldownTime; }

    public abstract void Activate(Player player);
}