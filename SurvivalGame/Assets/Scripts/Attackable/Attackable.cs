using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class Attackable : MonoBehaviour {
    public float HitPoints { get; set; }
    public float MaxHitPoints { get; set; }

    public Type TypeReq { get; set; }
    public int TierReq { get; set; }

    private None _none = new None();

    [SerializeField] public List<Drop> ItemDrops;

    public void TakeDamage(PlayerInventory attacker) {
        Debug.Log($"A tier {TierReq} {TypeReq} is required");
        Debug.Log($"A tier {attacker.EquippedItemToolStats.Tier} {attacker.EquippedItemToolStats.ItemType} is equipped");
        switch (TierReq) {
            case 0:
                Debug.Log("Hit");
                HitPoints -= attacker.EquippedItemToolStats.Damage;
                break;
            default:
                if (attacker.EquippedItemToolStats.Tier >= TierReq && attacker.EquippedItemToolStats.ItemType.IsSubclassOf(TypeReq)) {
                    Debug.Log("Hit");
                    HitPoints -= attacker.EquippedItemToolStats.Damage;
                }
                break;
        }
        if (HitPoints <= 0) {
            Death(attacker);
        }
    }


    private void Death(PlayerInventory attacker) {
        gameObject.SetActive(false);
        foreach (Drop drop in ItemDrops) {
            var quantityDrops = UnityEngine.Random.Range(drop.MinQuantity, drop.MaxQuantity + 1);
            attacker.PickupItem(drop.DroppedItem, quantityDrops);
        }
    }

    protected void SpawnEntity(float hitPoints) {
        MaxHitPoints = hitPoints;
        HitPoints = hitPoints;
    }

    public Attackable() {
        TierReq = 0;
        TypeReq = Type.GetType("None");
    }
}

[System.Serializable]
public class Drop {
    public Item DroppedItem { get; set; }
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
    public Drop(Item item, int minQuantity, int maxQuantity) {
        DroppedItem = item;
        MinQuantity = minQuantity;
        MaxQuantity = maxQuantity;
    }
}
