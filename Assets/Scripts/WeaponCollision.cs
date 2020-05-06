using UnityEngine.Events;
using UnityEngine;
using System;
 
public class WeaponCollision : MonoBehaviour
{
    public event EventHandler<WeaponCollisionEventArgs> OnWeaponCollision;

    private void OnTriggerEnter(Collider other)
    {      
        OnWeaponCollision?.Invoke(this, new WeaponCollisionEventArgs(other.transform));       
    }
}
