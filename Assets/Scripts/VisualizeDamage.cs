using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualizeDamage : MonoBehaviour
{
    public TextMeshPro damageNumbers;
    private DamageController dmgController;
    private Camera mainCamera;

    void Start()
    {
        GetComponent<WeaponCollision>().OnWeaponCollision += VisualizeDamageNumbers;
        dmgController = GetComponent<DamageController>();
        mainCamera = Camera.main;
    }

    private void VisualizeDamageNumbers(object sender, WeaponCollisionEventArgs args) 
    {
        damageNumbers.text = dmgController.GetDamage().ToString();
        //Instantiating Damage Numbers
        Instantiate(damageNumbers, args.Target.position + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f)), Quaternion.LookRotation(mainCamera.transform.position - args.Target.position));        
    }
}
