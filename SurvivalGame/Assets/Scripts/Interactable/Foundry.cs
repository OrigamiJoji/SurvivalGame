using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Foundry : Interactable {
    private FoundryInventory ThisInv {
        get { return gameObject.GetComponent<FoundryInventory>(); }
    }
    public static event UpdateInventoryCaller<FoundryInventory> UpdateFoundry;

    protected override void UpdateCaller() {
        UpdateFoundry?.Invoke(ThisInv);
        Debug.Log("Caller Updated");
    }
}
