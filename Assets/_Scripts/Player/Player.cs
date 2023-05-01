using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IAgent, IHittable, IKnockback
{
    [field: SerializeField]
    public PlayerDataSO PlayerData { get; set; }

    private int health;
    public int Health
    {
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, PlayerData.MaxHealth);
            UIHealth.UpdateUI(health);
        }
    }

    private int keys;
    public int Keys
    {
        get => keys;
        set
        {
            keys = Mathf.Clamp(value, 0, PlayerData.MaxKeys);
            UIKeys.UpdateKeysText(keys);
        }
    }

    private bool dead = false;
    public bool IsDead {
        get => dead;
    }
    private bool gracePeriod = false;

    private PlayerWeapon playerWeapon;
    private PlayerRunes playerRunes;
    private AgentMovement agentMovement = null;

    [field: SerializeField]
    public HealthUI UIHealth { get; set; }

    [field: SerializeField]
    public KeysUI UIKeys { get; set; }

    [field: SerializeField]
    public RunesUI UIRunes { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (!dead && !gracePeriod)
        {
            Health -= damage;
            Knockback(-(damageDealer.transform.position - transform.position).normalized, 5f, .3f);
            OnGetHit?.Invoke();
            if (Health <= 0)
            {
                dead = true;
                OnDie?.Invoke();
            }
            else
            {
                StartCoroutine(GracePeriodCoroutine());
            }           
        }
    }

    private void Awake()
    {
        agentMovement = GetComponent<AgentMovement>();
        playerWeapon = GetComponentInChildren<PlayerWeapon>();
        playerRunes = GetComponentInChildren<PlayerRunes>();
        if (UIHealth == null)
            UIHealth = FindObjectOfType<HealthUI>();
    }

    private void Start()
    {
        Health = PlayerData.MaxHealth;
        UIHealth.Initialize(Health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            var item = collision.gameObject.GetComponent<Item>();
            TryGrabbingItem(item);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("TreasureChest"))
        {
            var chest = collision.gameObject.GetComponent<TreasureChest>();
            TryOpeningChest(chest);
        }
    }

    private void TryGrabbingItem(Item item)
    {
        if (item != null)
        {
            switch (item.ResourceData.Resource)
            {
                case ResourceType.Health:
                    if (Health >= PlayerData.MaxHealth)
                    {
                        return;
                    }
                    Health += item.ResourceData.GetAmount();
                    item.PickupItem();
                    break;
                case ResourceType.Ammo:
                    if (playerWeapon.AmmoFull)
                    {
                        return;
                    }
                    playerWeapon.AddAmmo(item.ResourceData.GetAmount());
                    item.PickupItem();
                    break;
                case ResourceType.Weapon:
                    playerWeapon.ChangeWeapon(item.ResourceData.WeaponData);
                    item.PickupItem();
                    break;
                case ResourceType.Rune:
                    playerRunes.UpdateStats(item.ResourceData.RuneData);
                    UIRunes.UpdateRuneUI(item.ResourceData.RuneData);
                    item.PickupItem();                    
                    break;
                case ResourceType.Key:
                    Keys++;
                    item.PickupItem();
                    break;
                default:
                    break;
            }
        }
    }

    private void TryOpeningChest(TreasureChest chest)
    {
        if (chest.OpenChest(Keys > 0))
            Keys--;
    }

    public void Knockback(Vector2 direction, float power, float duration)
    {
        agentMovement.Knockback(direction, power, duration);
    }

    IEnumerator GracePeriodCoroutine()
    {
        gracePeriod = true;
        yield return new WaitForSeconds(PlayerData.GracePeriodDelay);
        gracePeriod = false;
    }
}
