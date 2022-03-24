using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class FoundryInventory : Inventory {
    [field: SerializeField] public double CurrentSmeltingTime { get; private set; }
    public double MaxSmeltingTime { get { return 75; } }

    public static event InventoryEvent UpdateFoundryInventory;

    private void Start() {
        GenerateInventory(new Slot[2, 2], 4);
    }
    public override void OnChange() {
        AddMaterial();
        UpdateFoundryInventory?.Invoke();
    }

    private void AddMaterial() {
        Slot smeltSlot = InventoryGrid[0,0];

        if (smeltSlot.Item is IFlammable itemFlammable) {
            CurrentSmeltingTime += itemFlammable.SmeltTime;
            smeltSlot.Quantity--;
            if (smeltSlot.Quantity.Equals(0)) smeltSlot.Item = new None();
        }
    }
}
