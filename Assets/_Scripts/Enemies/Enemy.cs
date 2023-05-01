using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

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
    public List<EnemyAttack> enemyRotatingAttacks { get; set; }

    [field: SerializeField]
    public EnemyAttack basicMeleeAttack { get; set; }

    private AgentMovement agentMovement = null;

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    private bool dead = false;

    private bool changingAttack = false;

    private int attackIndex = 0;

    [SerializeField]
    private float changeAttackDelay = 0.1f;

    private void Awake()
    {
        enemyRotatingAttacks = GetComponents<EnemyAttack>().ToList();
        basicMeleeAttack = GetComponent<EnemyMeleeAttack>();
        enemyRotatingAttacks.Remove(basicMeleeAttack); // rotating attacks are never melee
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

    private void Update()
    {
        if (!changingAttack)
        {
            StartCoroutine(ChangeAttackCoroutine());
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
            enemyRotatingAttacks[attackIndex].Attack(EnemyData.Damage);
        }
    }

    public void Knockback(Vector2 direction, float power, float duration)
    {
        agentMovement.Knockback(direction, power, duration);
    }

    IEnumerator ChangeAttackCoroutine()
    {
        changingAttack = true;
        yield return new WaitForSeconds(changeAttackDelay);
        //attackIndex = (attackIndex + 1) % enemyRotatingAttacks.Count;
        attackIndex = Random.Range(0, enemyRotatingAttacks.Count);
        changingAttack = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Player" && !dead)
        {
            basicMeleeAttack.Attack(EnemyData.Damage);
        }
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
