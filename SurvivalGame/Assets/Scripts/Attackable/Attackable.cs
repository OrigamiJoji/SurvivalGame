using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Attackable : MonoBehaviour {
    public float HitPoints { get; set; }
    public float MaxHitPoints { get; set; }

    [SerializeField] public List<Drops> _drops;

    public void TakeDamage(float dmg, PlayerInventory attacker) {
        HitPoints -= dmg;
        if (HitPoints <= 0) {
            Death(attacker);
        }
    }

    private void Death(PlayerInventory attacker) {
        gameObject.SetActive(false);
        foreach (Drops drop in _drops) {
            attacker.PickupItem(Type.GetType(drop.ItemName), UnityEngine.Random.Range(drop.MinQuantity, drop.MaxQuantity));
            
        }
    }

    protected void SpawnEntity(float hitPoints) {
        MaxHitPoints = hitPoints;
        HitPoints = hitPoints;
    }
}

[System.Serializable]
public class Drops {
    [field: SerializeField] public string ItemName { get; set; }
    [field: SerializeField] public int MinQuantity { get; set; }
    [field: SerializeField] public int MaxQuantity { get; set; }
}
