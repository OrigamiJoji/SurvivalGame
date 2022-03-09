using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attackable : MonoBehaviour
{
    public float HitPoints { get; set; }
    public float MaxHitPoints { get; set; }

    public void TakeDamage(float dmg) {
        HitPoints -= dmg;
        if(HitPoints <= 0) {
            Death();
        }
    }

    private void Death() {
        gameObject.SetActive(false);
    }

    protected void SpawnEntity(float hitPoints) {
        MaxHitPoints = hitPoints;
        HitPoints = hitPoints;
    }
}
