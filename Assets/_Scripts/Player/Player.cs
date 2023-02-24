using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IAgent, IHittable
{
    [field: SerializeField]
    public PlayerDataSO PlayerData { get; set; }

    [field: SerializeField]
    public int Health { get; set; } = 5;

    private bool dead = false;

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
                StartCoroutine(WaitToDie());
            }
        }
    }

    private void Start()
    {
        Health = PlayerData.MaxHealth;
    }

    public IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(.6f);
        Destroy(gameObject);
    }
}
