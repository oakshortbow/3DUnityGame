using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody rb;
    private Vector3 movement;
    private Animator anim;
    private float hor = 0f;
    private float ver = 0f;
    private bool lockMovement = false;

    //Temporary will be dynamically found based on closest distance to player later
    public Transform lookAt;

    private string[] movementRestrictedAnims = {"Right Slash", "Left Slash", "Lower Slash", "Blocking", "Dodging", "High Slash" };

    public float turnTime = 30f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
    }


    private void Update() {        
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0F, Input.GetAxisRaw("Vertical"));
        hor = movement.x;
        ver = movement.z;
        anim.SetBool("Running", (hor != 0 || ver != 0));
        
        if(Input.GetKeyDown(KeyCode.E)) {
            anim.SetTrigger("isDodgingOrBlocking");
        }

        if(Input.GetMouseButtonDown(0)) {
            Vector3 ToFace = lookAt.position;
            ToFace.y = transform.position.y;
            transform.LookAt(ToFace);
            anim.SetTrigger("isAttacking");
        }
        lockMovement = shouldLock();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if((hor != 0 || ver != 0) && !lockMovement) {
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
        rb.MovePosition(transform.position + (movement * speed * Time.deltaTime));
    }
    
    private bool shouldLock() {
        foreach(string s in movementRestrictedAnims) {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName(s)) {
                return true;
            }
        }
        return false;
    }
}
