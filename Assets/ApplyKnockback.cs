using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyKnockback : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform weaponHolder;
    void Start()
    {
        GetComponent<WeaponCollision>().OnWeaponCollision += TakeKnockback;
    }

    // Update is called once per frame
    private void TakeKnockback(object sender, WeaponCollisionEventArgs args) 
    {
        Vector3 dir = weaponHolder.position - args.Target.position;
        //dir = -dir.normalized;

        args.Target.GetComponent<Rigidbody>().AddForce(-dir.normalized * args.Knockback, ForceMode.Impulse);

    }
}
