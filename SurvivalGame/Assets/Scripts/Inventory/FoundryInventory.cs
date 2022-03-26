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
        UpdateFoundryInventory?.Invoke();
    }


    private void Smelt() {

    }

    private void Update() {
        if (InventoryGrid[0, 0].Item is IFlammable itemFlammable) {
            if (CurrentSmeltingTime + itemFlammable.SmeltTime <= MaxSmeltingTime) {
                CurrentSmeltingTime += itemFlammable.SmeltTime;
                InventoryGrid[0, 0].Quantity--;
                if (InventoryGrid[0, 0].Quantity.Equals(0)) InventoryGrid[0, 0].Item = new None();
                UpdateFoundryInventory?.Invoke();
            }
        }
        if (InventoryGrid[1, 0].Item is ISmeltable itemSmeltable) {
            if (CurrentSmeltingTime > 0) {
                StartCoroutine(Smelt(itemSmeltable));
            }
        }
    }


    private IEnumerator Smelt(ISmeltable itemSmeltable) {
        double timeRemaining = 8;
        while (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            CurrentSmeltingTime -= Time.deltaTime;
            yield return null;
        }
        if (InventoryGrid[0, 1].Item is None) {
            InventoryGrid[0, 1].Item = itemSmeltable.Product;
        }

        InventoryGrid[1, 0].Quantity--;
        if (InventoryGrid[1, 0].Quantity.Equals(0)) {
            InventoryGrid[1, 0].Item = new None();
        }
        InventoryGrid[0, 1].Quantity++;
        UpdateFoundryInventory?.Invoke();
        yield break;
    }
}
