using UnityEngine.Events;
using UnityEngine;
using System;
 
public class WeaponCollision : MonoBehaviour
{
    public event EventHandler<WeaponCollisionEventArgs> OnWeaponCollision;

    private void OnTriggerEnter(Collider other)
    {      
        if(OnWeaponCollision != null)
        {
            //Last Arg is Knockback
            OnWeaponCollision.Invoke(this, new WeaponCollisionEventArgs(other.transform, GetComponent<DamageController>().GetDamage(), GetComponent<KnockbackController>().GetKnockback()));
        }
    }
}
