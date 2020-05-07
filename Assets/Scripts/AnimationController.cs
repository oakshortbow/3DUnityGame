using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationController : MonoBehaviour
{

    private Animator anim;
    private string[] movementRestrictedAnims = {"Right Slash", "Left Slash", "Lower Slash", "Blocking", "Dodging", "High Slash" };

    public event EventHandler<LockMovementEventArgs> OnLockedAnimationEncountered;
    private bool lastAnimationStatus;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        lastAnimationStatus = isLockedAnimationPlaying();
    }

    // Update is called once per frame
    void Update()
    {
        bool currAnimationStatus = isLockedAnimationPlaying();
        if(lastAnimationStatus != currAnimationStatus) {
            OnLockedAnimationEncountered?.Invoke(this, new LockMovementEventArgs(currAnimationStatus));
            lastAnimationStatus = !lastAnimationStatus;
        }

        anim.SetBool("Running", (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0));
        anim.SetBool("isBlocking", Input.GetKeyDown(KeyCode.E));

        if(Input.GetMouseButtonDown(0)) {
            anim.SetTrigger("isAttacking");
        }    
    }


    private bool isLockedAnimationPlaying() {
        foreach(string s in movementRestrictedAnims) {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName(s)) {
                return true;
            }
        }
        return false;
    }
}
