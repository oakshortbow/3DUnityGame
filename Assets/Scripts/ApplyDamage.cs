using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<WeaponCollision>().OnWeaponCollision += TakeDamage;
    }

    // Update is called once per frame
    private void TakeDamage(object sender, WeaponCollisionEventArgs args) 
    {
        args.Target.GetComponent<HealthController>().DecreaseCurrentHealth(args.Damage);        
    }
}
