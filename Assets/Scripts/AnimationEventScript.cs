using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventScript : MonoBehaviour
{
    public GameObject weapon;
    private Rigidbody rb;
    private BoxCollider bc;
    private Animator anim;

    private void Start() {
        rb = this.GetComponent<Rigidbody>();
        bc = weapon.GetComponent<BoxCollider>();
        anim = this.GetComponent<Animator>();
    }

    public void MoveIntoSwordSwing(float speed) 
    {
        //Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    public void ToggleCollider() 
    {
        //BoxCollider bc = weapon.GetComponent<BoxCollider>();
        bc.enabled = !bc.enabled;
    }

    public void SetKnockbackX(float val) 
    {
        this.GetComponentInChildren<KnockbackController>().x= val;
    }

    public void SetKnockbackY(float val) 
    {

        this.GetComponentInChildren<KnockbackController>().y= val;
    }

    public void SetKnockbackZ(float val) 
    {
        this.GetComponentInChildren<KnockbackController>().z = val;
    }

    
    public void SetDamageMultiplier(float value) 
    {
        this.GetComponentInChildren<DamageController>().multiplier = value;
    }

    public void SetAnimSpeed(float speed) {
        anim.speed = speed;
    }


}
