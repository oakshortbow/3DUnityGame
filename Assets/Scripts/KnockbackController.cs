using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform weaponHolder;

    public float x;
    public float y;
    public float z;


    void Start()
    {
        GetComponent<WeaponCollision>().OnWeaponCollision += TakeKnockback;
    }

    // Update is called once per frame
    private void TakeKnockback(object sender, WeaponCollisionEventArgs args) 
    {
        Vector3 dir = weaponHolder.position - args.Target.position;
        dir = -dir.normalized;
        //dir.y = 100f;
        Debug.Log(dir.y);
        args.Target.GetComponent<Rigidbody>().AddForce(Vector3.Scale(dir,new Vector3(x, y ,z)), ForceMode.VelocityChange);
    }
}
