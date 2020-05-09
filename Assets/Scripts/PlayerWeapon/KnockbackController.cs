using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackController : MonoBehaviour
{

    private Transform weaponHolder;

    public float x = 0;
    public float y = 0;
    public float z = 0;


    void Start()
    {
        GetComponent<WeaponCollision>().OnWeaponCollision += TakeKnockback;
        weaponHolder = this.FindParentWithTag("Player").transform;
    }

    // Update is called once per frame
    private void TakeKnockback(object sender, WeaponCollisionEventArgs args) 
    {
        Vector3 dir = weaponHolder.position - args.Target.position;
        dir = -dir.normalized;
        args.Target.GetComponent<Rigidbody>().AddForce(Vector3.Scale(dir,new Vector3(x, y ,z)), ForceMode.VelocityChange);
    }
}
