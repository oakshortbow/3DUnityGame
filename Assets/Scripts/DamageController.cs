using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public int minHit = 25;
    public int maxHit = 50;

    public void IncreaseMinimumHit(int amt) {
        minHit += amt;
    }

    public void IncreaseMaximumHit(int amt) {
        maxHit += amt;
    }

    public void DecreaseMaximumHit(int amt) {
        maxHit -= amt;
    }

    public void DecreaseMinimumHit(int amt) {
        minHit -= amt;
    }

    public int GetRandomDamage() {
        return Random.Range(minHit, maxHit);
    }
}
