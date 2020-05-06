using System;
using UnityEngine;

public class WeaponCollisionEventArgs : EventArgs
{
    public Transform Target {get; private set; }

    public WeaponCollisionEventArgs(Transform target) {
        this.Target = target;
    }
}
