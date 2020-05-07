using System;
using UnityEngine;

public class LockMovementEventArgs : EventArgs
{
    public bool LockMovement{get; private set; }

    public LockMovementEventArgs(bool lockMovement) {
        this.LockMovement = lockMovement;
    }
}
