using UnityEngine;
using System;
using System.Runtime;

public class PlayerInventory : Inventory {

    #region Data Members
    public Item EquippedItem { get; set; }
    public Tool EquippedItemToolStats {
        get {
            if (EquippedItem is Tool tool) {
                return tool;
            }
            else {
                return new Fists();
            }
        }
    }
    public Placeable EquippedItemPlaceable {
        get {
            if (EquippedItem is Placeable placeable) {
                return placeable;
            }
            else {
                throw new NullReferenceException();
            }
        }
    }
    #endregion Data Members
    public static event InventoryEvent UpdatePlayerInventory;
    public override void OnChange() {
        UpdatePlayerInventory?.Invoke();
    }
    private void Awake() {
        GenerateInventory(new Slot[3, 5], 15);
    }

    private void ChangeItem() {
        for (int i = 0; i < 5; i++) {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) {
                SelectItem(i);
            }
        }
    }
    private void SelectItem(int column) {
        var currentSlot = FindSlot(0, column);
        EquippedItem = currentSlot.Item;
    }


    private void Start() {
        OnChange();
    }

    private void Update() {
        ChangeItem();
    }
    public string Debug() {
        return HeldItem.Instance.Held.Item.ToString() + HeldItem.Instance.Held.Quantity.ToString();
    }
}

