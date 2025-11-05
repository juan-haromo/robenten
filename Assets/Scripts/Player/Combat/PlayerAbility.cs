using UnityEngine;

public abstract class PlayerAbility : ScriptableObject
{
    [SerializeField] float cooldownTime;
    [SerializeField] private string abilityName;
    [SerializeField] private AnimationClip clip;

    public float CooldownTime { get => cooldownTime; }

    public virtual void Initialize(Player player)
    {
        player.animManager.ReplaceClip(abilityName, clip);
    }

    public abstract void Activate(Player player);

    public virtual void Deactivate(Player player)
    {
        
    }
}