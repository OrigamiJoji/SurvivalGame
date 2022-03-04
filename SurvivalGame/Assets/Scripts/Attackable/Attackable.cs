using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attackable : MonoBehaviour
{
    protected float hitPoints { get; set; }

    public void TakeDamage(float dmg) {
        hitPoints -= dmg;
        if(hitPoints <= 0) {
            Death();
        }
    }

    private void Death() {
        gameObject.SetActive(false);
    }
}
