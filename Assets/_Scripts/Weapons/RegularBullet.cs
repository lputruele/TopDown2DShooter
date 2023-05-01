using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class RegularBullet : Bullet
{
    protected Rigidbody2D rigidbody2d;
    private bool isDead = false;
    private int bouncesLeft;
    private int piercesLeft;



    private void Start()
    {
        bouncesLeft = BulletData.BounceCount;
        piercesLeft = BulletData.PierceCount + bulletBonusStats.PiercingBonus;
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(BulletData.ImpactObstaclePrefab, transform.position, Quaternion.identity);
        isDead = true;
        Destroy(gameObject);
    }

    public override BulletDataSO BulletData 
    { 
        get => base.BulletData;
        set
        {
            base.BulletData = value;
            rigidbody2d = GetComponent<Rigidbody2D>();
            rigidbody2d.drag = BulletData.Friction;
            if (bulletBonusStats != null)
            {
                rigidbody2d.drag += bulletBonusStats.SpeedBonus;
            }
        }
    }

    private void FixedUpdate()
    {
        if (rigidbody2d != null && BulletData != null)
        {
            float bulletSpeed = BulletData.BulletSpeed;
            if (bulletBonusStats != null)
            {
                bulletSpeed += bulletBonusStats.SpeedBonus;
            }
            rigidbody2d.MovePosition(transform.position + bulletSpeed * transform.right * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead)
        {
            var hittable = collision.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittable.GetHit(BulletData.Damage + bulletBonusStats.DamageBonus, gameObject);
            }
            if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                HitObstacle(collision);
                isDead = true;
                Destroy(gameObject);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                HitEnemy(collision);
                if (piercesLeft == 0)
                {
                    isDead = true;
                    Destroy(gameObject);
                }
                else
                {
                    piercesLeft--;
                }
            } 
        }        
    }


    private void HitEnemy(Collider2D collision)
    {
        var knockback = collision.GetComponent<IKnockback>();
        knockback?.Knockback(transform.right, BulletData.KnockbackPower + bulletBonusStats.KnockbackBonus, BulletData.KnockbackTime);
        Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
        if (BulletData.HasSplashDamage || bulletBonusStats.SplashDamageBonus > 0)
        {
            SpawnSplashDamageImpact(collision, true);
        }
        else
        {
            GameObject impact;
            impact = Instantiate(BulletData.ImpactEnemyPrefab, collision.transform.position + (Vector3)randomOffset, Quaternion.identity);
            impact.transform.localScale = new Vector3(BulletData.ImpactRadius + bulletBonusStats.ImpactRadiusBonus, BulletData.ImpactRadius + bulletBonusStats.ImpactRadiusBonus, 0);
        }
    }

    private void HitObstacle(Collider2D collision)
    {
        if (BulletData.HasSplashDamage || bulletBonusStats.SplashDamageBonus > 0)
        {
            SpawnSplashDamageImpact(collision, false);
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 3, BulletData.BulletLayerMask);
            GameObject impact;
            if (hit.collider != null)
            {
                impact = Instantiate(BulletData.ImpactObstaclePrefab, hit.point, Quaternion.identity);
            }
            else
            {
                impact = Instantiate(BulletData.ImpactObstaclePrefab, transform.position, Quaternion.identity);
            }
            impact.transform.localScale = new Vector3(BulletData.ImpactRadius + bulletBonusStats.ImpactRadiusBonus, BulletData.ImpactRadius + bulletBonusStats.ImpactRadiusBonus, 0);
        }       
    }

    private void SpawnSplashDamageImpact(Collider2D collision, bool hitEnemy)
    {
        GameObject splashImpact;
        if (hitEnemy)
        {
            splashImpact = Instantiate(BulletData.SplashDamageImpactPrefab, collision.transform.position, Quaternion.identity);
            
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 3, BulletData.BulletLayerMask);
            if (hit.collider != null)
                splashImpact = Instantiate(BulletData.SplashDamageImpactPrefab, hit.point, Quaternion.identity);
            else
                splashImpact = Instantiate(BulletData.SplashDamageImpactPrefab, transform.position, Quaternion.identity);
        }
        splashImpact.GetComponent<SplashDamage>().Damage = BulletData.SplashDamage + bulletBonusStats.SplashDamageBonus;
        splashImpact.transform.localScale = new Vector3(BulletData.ImpactRadius + bulletBonusStats.ImpactRadiusBonus, BulletData.ImpactRadius + bulletBonusStats.ImpactRadiusBonus, 0);
    }

}
