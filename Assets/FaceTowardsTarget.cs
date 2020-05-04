using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTowardsTarget : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject target;

    // Update is called once per frame
    void Update()
    {    
        transform.LookAt(target.transform.position);
        Debug.Log(target.transform.position);
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        // Set the altered rotation back
        transform.rotation = Quaternion.Euler(eulerAngles);
    }
}
