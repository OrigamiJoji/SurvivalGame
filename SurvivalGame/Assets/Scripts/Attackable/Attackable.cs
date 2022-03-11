using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Attackable : MonoBehaviour
{
    public Type[] typeReq;
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
