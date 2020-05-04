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
        GetComponent<WeaponCollision>().onCollision.AddListener((sender, other) => HandleHit(sender, other));
        mainCamera = Camera.main;
    }




    private void HandleHit(GameObject sender, Transform other) {

            //Used to Face Damage Numbers Towards Camera
            Vector3 relativePos = mainCamera.transform.position - other.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            //Offset to instantiate damage numbers
            Vector3 offset = new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f));
            //Setting Damage. Not Done For now, just displaying
            damageNumbers.text = "Hit!";
            //Instantiating Damage Numbers
            Instantiate(damageNumbers, other.transform.position + offset, rotation);
        
    }
}
