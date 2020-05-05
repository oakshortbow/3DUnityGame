using System;
using UnityEngine;

public class WeaponCollisionEventArgs : EventArgs
{
    public Transform Target {get; private set; }
    public int Damage {get; private set; } 
    public float Knockback {get; private set; }

    public WeaponCollisionEventArgs(Transform target, int damage, float knockback) {
        this.Target = target;
        this.Damage = damage;
        this.Knockback = knockback;
    }
}
