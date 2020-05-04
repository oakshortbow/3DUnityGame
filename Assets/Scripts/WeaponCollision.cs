using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public delegate void WeaponCollisionEventHandler(object source, WeaponCollisionEventArgs e);
 
public class WeaponCollision : MonoBehaviour
{
    public event WeaponCollisionEventHandler OnWeaponCollision;

    private void OnTriggerEnter(Collider other)
    {      
        if(OnWeaponCollision != null)
        {
            //Last Arg is Knockback
            OnWeaponCollision.Invoke(this, new WeaponCollisionEventArgs(other.transform, GetComponent<DamageController>().GetRandomDamage(), 0));
        }
    }
}
