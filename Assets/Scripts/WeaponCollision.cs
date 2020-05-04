using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;


 [System.Serializable]
 public class WeaponCollisionEvent : UnityEvent<GameObject, Transform>{}


public class WeaponCollision : MonoBehaviour
{
    //Static OnCollision event, anything that wants to know about a collision would subscribe to this event
    public WeaponCollisionEvent onCollision = new WeaponCollisionEvent();

    private void OnTriggerEnter(Collider other)
    {      
        onCollision.Invoke(this.gameObject, other.transform); 
    }
}
