using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Workstump : Interactable {
    private CraftInventory ThisInv {
        get { return gameObject.GetComponent<CraftInventory>(); }
    }
    public static event UpdateInventoryCaller<CraftInventory> UpdateCraft;

    protected override void UpdateCaller() {
        UpdateCraft?.Invoke(ThisInv);
        Debug.Log("Caller Updated");
    }
}