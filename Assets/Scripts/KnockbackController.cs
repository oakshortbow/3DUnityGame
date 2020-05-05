using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackController : MonoBehaviour
{
    public float knockback = 1f;

    public float multiplier = 1f;


    public void IncreaseKnockback(float increaseBy) {
        knockback += increaseBy;
    }

    public void DecreaseKnockback(float decreaseBy) {
        knockback -= decreaseBy;
    }

    public float GetKnockback() {
        return knockback * multiplier;
    }
}
