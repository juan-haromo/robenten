using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [Header("Curren abilities")]
    public PlayerAbility attackAbility;
    public PlayerAbility defenseAbility;
    public PlayerAbility specialAbility;
    public PlayerAbility ultimateAbility;

    [Header("Ability Pools")]
    [SerializeField] List<PlayerAbility> attacks;
    [SerializeField] List<PlayerAbility> defenses;
    [SerializeField] List<PlayerAbility> specials;
    [SerializeField] List<PlayerAbility> ultimates;

    [Header("UI")]
    [SerializeField] Image imgSpecialIcon;
    [SerializeField] Image imgSpecialCooldown;
    [SerializeField] Image imgUltimateIcon;
    [SerializeField] Image imgUltimateCooldown;

    [Header("Ability change cooldowns")]
    [SerializeField] float minCooldown;
    [SerializeField] float maxCooldown;
    float nextChangeTime;

    [Header("Charges")]
    [SerializeField] float specialChargeRate;
    public bool IsSpecialCharged { get; private set;}
    [SerializeField] float ultimateChargeRate;
    public bool IsUltimateCharged { get; private set;}
    float specialCharge;
    float ultimateCharge;

    void Start()
    {
        UseSpecial();
        UseUltimate();
    }

    void Update()
    {
        if (nextChangeTime < Time.time)
        {
            ChangeAbilities();
        }
    }
    
    void ChangeAbilities()
    {
        attackAbility = ChangeAbility(attackAbility, attacks);
        defenseAbility = ChangeAbility(defenseAbility, defenses);
        specialAbility = ChangeAbility(specialAbility, specials);
        ultimateAbility = ChangeAbility(ultimateAbility, ultimates);

        nextChangeTime = Time.time + Random.Range(minCooldown, maxCooldown);
    }

    PlayerAbility ChangeAbility(PlayerAbility oldAbility, List<PlayerAbility> abilityPool)
    {
        PlayerAbility newAbility;
        int randomIndex = Random.Range(0, abilityPool.Count);
        newAbility = abilityPool[randomIndex];
        abilityPool.RemoveAt(randomIndex);
        if (oldAbility != null)
        {
            abilityPool.Add(oldAbility);
        }

        return newAbility;
    }

    public void ReduceChange(float cooldownSeconds)
    {
        nextChangeTime -= Mathf.Abs(cooldownSeconds);
    }

    public void UseSpecial()
    {
        IsSpecialCharged = false;
        specialCharge = 0;
        StartCoroutine(ChargeSpecial());
    }

    public IEnumerator ChargeSpecial()
    {
        while (specialCharge < 100)
        {
            specialCharge += specialChargeRate * Time.deltaTime;
            imgSpecialCooldown.fillAmount = specialCharge / 100;
            yield return null;
        }
        IsSpecialCharged = true;
    }

    public void UseUltimate()
    {
        IsUltimateCharged = false;
        ultimateCharge = 0;
        StartCoroutine(ChargeUltimate());
    }
    
    IEnumerator ChargeUltimate()
    {
        while (ultimateCharge < 100)
        {
            ultimateCharge += ultimateChargeRate * Time.deltaTime;
            imgUltimateCooldown.fillAmount = ultimateCharge / 100;
            yield return null;
        }
        IsUltimateCharged = true;
    }
    public PlayerAbilityUltimate ultimateAbility;

    public Hitbox hitbox;
}