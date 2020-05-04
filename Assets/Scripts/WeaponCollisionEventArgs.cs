﻿using System;
using UnityEngine;

public class WeaponCollisionEventArgs : EventArgs
{
    public Transform Target {get; private set; }
    public int Damage {get; private set; } 
    public int Knockback {get; private set; }

    public WeaponCollisionEventArgs(Transform target, int damage, int knockback) {
        this.Target = target;
        this.Damage = damage;
        this.Knockback = knockback;
    }
}
