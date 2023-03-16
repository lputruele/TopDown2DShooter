using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable, IAgent, IKnockback
{
    [field: SerializeField]
    public EnemyDataSO EnemyData { get; set; }

    [field: SerializeField]
    public int Health { get; set; } = 2;

    [field: SerializeField]
    public bool IsBeingHit { get; set; }

    [field: SerializeField]
    public bool IsRespawned { get; set; }

    [field: SerializeField]
    public float SafeSpawnRadius { get; set; } = 2;

    [field: SerializeField]
    public EnemyAttack enemyAttack { get; set; }

    private bool dead = false;
    private AgentMovement agentMovement = null;

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    private void Awake()
    {
        if (enemyAttack == null)
        {
            enemyAttack = GetComponent<EnemyAttack>();
        }
        agentMovement = GetComponent<AgentMovement>();
    }

    private void Start()
    {
        Health = EnemyData.MaxHealth;
        if (IsRespawned)
        {
            GetComponentInChildren<ItemDropper>().Disabled = true;
        }
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (!IsBeingHit)
        {
            IsBeingHit = true;
            StartCoroutine(AlertCoroutine());
        }
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

    private IEnumerator AlertCoroutine()
    {
        yield return new WaitForSeconds(2f);
        IsBeingHit = false;
    }

    public void Die()
    {
        Destroy(gameObject);
    }


    public void PerformAttack()
    {
        if (!dead)
        {
            enemyAttack.Attack(EnemyData.Damage);
        }
    }

    public void Knockback(Vector2 direction, float power, float duration)
    {
        agentMovement.Knockback(direction, power, duration);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, SafeSpawnRadius);
            Gizmos.color = Color.white;
        }
    }
#endif
}
