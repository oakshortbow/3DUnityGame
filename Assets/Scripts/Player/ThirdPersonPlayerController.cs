using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThirdPersonPlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float turnTime = 100f;

    [SerializeField]
    private float dodgeDistance = 8.0f;
    [SerializeField]
    private float dodgeTime = 0.5f;

    [SerializeField]
    private Material cloneMaterial;

    private Rigidbody rb;
    private Renderer[] renderers;
    private Vector3 movement;
    private bool lockMovement = false;
    private int cloneLoopTicks = 0;

    Sequence colourChangeSequence;


    // Start is called before the first frame update
    private void Start()
    {
        renderers = this.GetComponentsInChildren<Renderer>();
        rb = this.GetComponent<Rigidbody>();
        GetComponent<AnimationController>().OnLockedAnimationEncountered += ToggledLockMovement;
    }

    private void OnDestroy()
    {
         GetComponent<AnimationController>().OnLockedAnimationEncountered -= ToggledLockMovement;
    }


    private void Update() {       
        if(lockMovement) {
            return;
        } 

        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0F, Input.GetAxisRaw("Vertical"));
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(lockMovement) {
            return;
        } 

        if(movement.x != 0 || movement.z != 0) {
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
    

    private void ToggledLockMovement(object sender, LockMovementEventArgs args) {
        if(lockMovement != args.LockMovement) {
            lockMovement = args.LockMovement;
        }
    }

    private void Blink() 
    {
        colourChangeSequence?.Kill(true);

        SetMaterialColour(100f);
        //this.GetComponent<BoxCollider>().enabled = false;
        this.GetComponent<Rigidbody>().useGravity = false;
        //this.transform.localScale = Vector3.zero;
        rb.DOMove(transform.position + transform.forward * dodgeDistance, dodgeTime).SetUpdate(UpdateType.Fixed, true).OnUpdate(ClonePlayer).OnComplete(FinishBlink);
    }


    public void SetMaterialColour(float val) {
        foreach(Renderer r in renderers) 
        {
            foreach(Material m in r.materials) {
                if(m.HasProperty("_Offset")) 
                {              
                    m.SetFloat("_Offset", val);              
                }  
            }
        }
    }

    private void ClonePlayer() {
        if(cloneLoopTicks <= 2) {
            cloneLoopTicks++;
            return;
        }

        cloneLoopTicks = 0;

        GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
        //clone.transform.localScale = new Vector3(1, 1, 1);
        clone.GetComponent<BoxCollider>().enabled = false;
        Destroy(clone.GetComponent<ThirdPersonPlayerController>());
        Destroy(clone.GetComponent<Animator>());
        Destroy(clone.GetComponent<AnimationController>());  
        clone.AddComponent<DissolveClones>();
        SetCloneMaterial(clone);
    }

    
    private void SetCloneMaterial(GameObject obj) 
    {

        Renderer[] skinMeshList = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in skinMeshList)
        {
            if(r.materials.Length > 1) 
            {
                r.materials = new Material[] {cloneMaterial, cloneMaterial, cloneMaterial};     
            }
            else 
            {
                r.material = cloneMaterial;
            }
        }
    }


    private void FinishBlink() 
    {
        int index = 0;
        bool append = true;
        //this.transform.localScale = new Vector3(1, 1, 1);
        this.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<Rigidbody>().useGravity = true;
        colourChangeSequence = DOTween.Sequence();
        foreach(Renderer r in renderers) {
            foreach(Material m in r.materials) {
                 if(m.HasProperty("_Offset")) 
                 {
                        colourChangeSequence.Join(m.DOFloat(0, "_Offset", 1f));          
                 }
            }
        }
        colourChangeSequence.PrependInterval(0.5f);
    }
}
