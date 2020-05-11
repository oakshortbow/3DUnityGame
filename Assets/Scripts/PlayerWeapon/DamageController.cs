using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public int baseHit = 50;
    public float multiplier = 1f;
    public float randMinMultiplier = 0.9f;
    public float randMaxMultiplier = 1.1f;


    private void Start()
    {
        GetComponent<WeaponCollision>().OnWeaponCollision += DamageEnemy;
    }

    private void OnDestroy()
    {
        GetComponent<WeaponCollision>().OnWeaponCollision -= DamageEnemy;
    }


    public void DecreaseBaseHit(int amt) {
        baseHit -= amt;
    }

    public void IncreaseBaseHit(int amt) {
        baseHit += amt;
    }

    public int GetDamage() {
        return (int)Mathf.Floor(baseHit * multiplier * Random.Range(randMinMultiplier, randMaxMultiplier));
    }

    private void DamageEnemy(object sender, WeaponCollisionEventArgs args) 
    {
        args.Target.GetComponent<HealthController>().DecreaseCurrentHealth(GetDamage());        
    }
}
