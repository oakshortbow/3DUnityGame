using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float turnTime = 100f;

    private Rigidbody rb;
    private Vector3 movement;
    private Animator anim;

    private float hor = 0f;
    private float ver = 0f;
    private bool lockMovement = false;

    // Start is called before the first frame update
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        GetComponent<AnimationController>().OnLockedAnimationEncountered += SetLockMovement;
    }


    private void Update() {       
        if(lockMovement) {
            return;
        } 

        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0F, Input.GetAxisRaw("Vertical"));
        hor = movement.x;
        ver = movement.z;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(lockMovement) {
            return;
        } 
        
        if(hor != 0 || ver != 0) {
            moveCharacter(movement);
        }
    }

    private void moveCharacter(Vector3 movement) 
    {
        movement = Camera.main.transform.TransformDirection(movement);
        Quaternion facingAngle = Quaternion.LookRotation(movement);
        facingAngle.x = 0;
        facingAngle.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, facingAngle, turnTime * Time.deltaTime);
        rb.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime));
    }
    

    private void SetLockMovement(object sender, LockMovementEventArgs args) {
        if(lockMovement != args.LockMovement) {
            lockMovement = args.LockMovement;
        }
    }
}
