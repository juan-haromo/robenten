using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{

    public static PlayerCombat Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

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
    [SerializeField] Image imgAttackIcon;
    [SerializeField] Image imgDefensiveIcon;
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

    public Player player;

    void Start()
    {
        specialCharge = 0;
        ultimateCharge = 0;
        StartCoroutine(ChargeSpecial());
        StartCoroutine(ChargeUltimate());
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
        attackAbility.Initialize(player);
        imgAttackIcon.sprite = attackAbility.abilitySprite;

        defenseAbility = ChangeAbility(defenseAbility, defenses);
        defenseAbility.Initialize(player);
        imgDefensiveIcon.sprite = defenseAbility.abilitySprite;

        specialAbility = ChangeAbility(specialAbility, specials);
        specialAbility.Initialize(player);
        imgSpecialIcon.sprite = specialAbility.abilitySprite;
        imgSpecialCooldown.sprite = specialAbility.abilitySprite;

        ultimateAbility = ChangeAbility(ultimateAbility, ultimates);
        ultimateAbility.Initialize(player);
        imgUltimateIcon.sprite = ultimateAbility.abilitySprite;
        imgUltimateCooldown.sprite = ultimateAbility.abilitySprite;

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

    public bool UseSpecial()
    {
        if(!IsSpecialCharged){ return false; }
        IsSpecialCharged = false;
        specialCharge = 0;
        StartCoroutine(ChargeSpecial());
        return true;
    }

    public IEnumerator ChargeSpecial()
    {
        IsSpecialCharged = false;
        while (specialCharge < 100)
        {
            specialCharge += specialChargeRate * Time.deltaTime;
            imgSpecialCooldown.fillAmount = specialCharge / 100;
            yield return null;
        }
        IsSpecialCharged = true;
    }

    public bool UseUltimate()
    {
        if(!IsUltimateCharged){ return false; }
        IsUltimateCharged = false;
        ultimateCharge = 0;
        StartCoroutine(ChargeUltimate());
        return true;
    }
    
    IEnumerator ChargeUltimate()
    {
        IsUltimateCharged = false;
        while (ultimateCharge < 100)
        {
            ultimateCharge += ultimateChargeRate * Time.deltaTime;
            imgUltimateCooldown.fillAmount = ultimateCharge / 100;
            yield return null;
        }
        IsUltimateCharged = true;
    }

    public Hitbox hitbox;

}