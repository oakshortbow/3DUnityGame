using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventScript : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;

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

    public void SetDamageMultiplier(float value) 
    {
        this.GetComponentInChildren<DamageController>().multiplier = value;
    }
}
