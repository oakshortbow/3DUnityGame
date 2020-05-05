using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventScript : MonoBehaviour
{
    public GameObject weapon;

    public void MoveIntoSwordSwing(float speed) {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    public void ToggleCollider() {
        BoxCollider c = weapon.GetComponent<BoxCollider>();
        c.enabled = !c.enabled;
    }

    public void SetKnockbackMultiplier(float value) {
        this.GetComponentInChildren<KnockbackController>().multiplier = value;
    }

    
    public void SetDamageMultiplier(float value) {
        this.GetComponentInChildren<DamageController>().multiplier = value;
    }


}
