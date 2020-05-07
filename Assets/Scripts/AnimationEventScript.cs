using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationEventScript : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private float blinkDistance = 10f;
    [SerializeField]
    private float blinkTime = 1f;

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
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    public void ToggleCollider() 
    {
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
