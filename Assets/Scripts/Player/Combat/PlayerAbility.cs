using UnityEngine;

public abstract class PlayerAbility : ScriptableObject
{
    [SerializeField] float cooldownTime;
    public string abilityName;
    [SerializeField] private AnimationClip clip;
    public Sprite abilitySprite;

    public float CooldownTime { get => cooldownTime; }

    public virtual void Initialize(Player player)
    {
        Debug.Log("initialize");
        player.animManager.ReplaceClip(abilityName, clip);
    }

    public abstract void Activate(Player player);

    public virtual void Deactivate(Player player) { }
}