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
    private Vector3 movement;
    private bool lockMovement = false;

    private List<GameObject> clones = new List<GameObject>();
    private int cloneCount = 0;
    private int renderClone = 0;
    private int cloneLoopTicks = 0;

    private const int MAX_CLONES = 3;

    // Start is called before the first frame update
    private void Start()
    {
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

    private void Blink() {
        this.GetComponent<BoxCollider>().enabled = false;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.transform.localScale = Vector3.zero;
        rb.DOMove(transform.position + transform.forward * dodgeDistance, dodgeTime).SetUpdate(UpdateType.Fixed, true).OnUpdate(ClonePlayer).OnComplete(FinishBlink);
    }


    private void ClonePlayer() {
        if(cloneLoopTicks <= 4) {
            cloneLoopTicks++;
            return;
        }

        cloneLoopTicks = 0;   
        if(clones.Count < MAX_CLONES) {
            GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
            clones.Add(clone);
            clone.transform.localScale = new Vector3(1, 1, 1);
            Destroy(clone.GetComponent<BoxCollider>());
            Destroy(clone.GetComponent<Rigidbody>());
            Destroy(clone.GetComponent<ThirdPersonPlayerController>());
            Destroy(clone.GetComponent<Animator>());
            Destroy(clone.GetComponent<AnimationController>());  
            Renderer[] skinMeshList = clone.GetComponentsInChildren<Renderer>();
            Material[] swordMaterials = new Material[] {cloneMaterial, cloneMaterial, cloneMaterial};
            foreach (Renderer r in skinMeshList)
            {
                if(r.materials.Length == 3) 
                {
                   r.materials = swordMaterials;     
                }
                else 
                {
                    r.material = cloneMaterial;
                }
            }
            return;       
        }
        int cloneIndex = cloneCount;
        if(cloneIndex < MAX_CLONES) {
            clones[cloneIndex].transform.position = this.transform.position;
            clones[cloneIndex].transform.rotation = this.transform.rotation;
            clones[cloneIndex].SetActive(true);
            cloneCount++;
        }

    }

    private void FinishBlink() {
        this.transform.localScale = new Vector3(1, 1, 1);
        this.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<Rigidbody>().useGravity = true;
        cloneCount = 0;
    }
}
