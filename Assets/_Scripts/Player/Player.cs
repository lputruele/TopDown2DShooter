using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IAgent, IHittable
{
    [field: SerializeField]
    public PlayerDataSO PlayerData { get; set; }

    private int health;
    public int Health { 
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, PlayerData.MaxHealth);
            UIHealth.UpdateUI(health);
        }
    }

    private bool dead = false;

    [field: SerializeField]
    public HealthUI UIHealth { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (!dead)
        {
            Health -= damage;
            OnGetHit?.Invoke();
            if (Health <= 0)
            {
                dead = true;
                OnDie?.Invoke();
            }
        }
    }

    private void Start()
    {
        Health = PlayerData.MaxHealth;
        UIHealth.Initialize(Health);
    }

}
