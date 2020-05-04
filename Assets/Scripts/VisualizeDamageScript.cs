using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualizeDamageScript : MonoBehaviour
{
    public TextMeshPro damageNumbers;
    private Camera mainCamera;

    void Start()
    {
        GetComponent<WeaponCollision>().OnWeaponCollision += HandleHit;
        mainCamera = Camera.main;
    }

    private void HandleHit(object sender, WeaponCollisionEventArgs args) 
    {
            damageNumbers.text = args.Damage.ToString();
            //Instantiating Damage Numbers
            Instantiate(damageNumbers, args.Target.position + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f)), Quaternion.LookRotation(mainCamera.transform.position - args.Target.position));        
    }
}
