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

    private Rigidbody rb;
    private Vector3 movement;
    private bool lockMovement = false;

    private List<GameObject> clones = new List<GameObject>();
    private int cloneCount = 0;
    private int renderClone = 0;

    private const int MAX_CLONES = 25;

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
        if(clones.Count < 3) {
            GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
            clones.Add(clone);
            clone.transform.localScale = new Vector3(1, 1, 1);
            Destroy(clone.GetComponent<ThirdPersonPlayerController>());
            Destroy(clone.GetComponent<Animator>());
            Destroy(clone.GetComponent<BoxCollider>());
            Destroy(clone.GetComponent<Rigidbody>());
            Destroy(clone.GetComponent<AnimationController>());  
            SkinnedMeshRenderer[] skinMeshList = clone.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer smr in skinMeshList)
            {
                smr.material.DOFloat(2, "_AlphaThreshold", 5f);
            }
            return;       
        }
        int cloneIndex = cloneCount++;
        clones[cloneIndex].transform.position = this.transform.position;
        clones[cloneIndex].transform.rotation = this.transform.rotation;
        clones[cloneIndex].SetActive(true);

    }

    private void FinishBlink() {
        this.transform.localScale = new Vector3(1, 1, 1);
        this.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<Rigidbody>().useGravity = true;
        cloneCount = 0;
    }
}
