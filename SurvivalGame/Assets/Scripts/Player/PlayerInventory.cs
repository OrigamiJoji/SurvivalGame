using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class PlayerInventory : MonoBehaviour {

    private InventorySlot[,] _inventory = new InventorySlot[3, 5];

    private Slot _heldItem = new Slot();
    public Slot HeldItem { 
        get { 
            return _heldItem; 
        } 
        set {
            _heldItem = HeldItem;
        } 
    }

    public Item EquippedItem { get; set; }
    public ITool EquippedItemToolStats {
        get {
            if (EquippedItem is ITool) {
                return (ITool)EquippedItem;
            }
            else {
                return new Fists();
            }
        }
    }

    public class Slot {
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }

    [System.Serializable]
    public class InventorySlot : Slot {
        public int Position { get; set; }
        public static int TotalSlots { get; private set; }

        public InventorySlot() {
            Item = null;
            Position = TotalSlots;
            TotalSlots++;
        }
    }

    private void ClearSlot(Slot slot) {
        slot.Item = null;
        slot.Quantity = 0;
    }
    private void EmptySlot(Slot inventorySlot) {
        if (inventorySlot.Quantity.Equals(0)) {
            inventorySlot.Item = null;
        }
    }
    private void StoreInProperSlot(InventorySlot inventorySlot) {
        var column = inventorySlot.Position % _inventory.GetLength(1);
        var row = Mathf.FloorToInt(inventorySlot.Position / _inventory.GetLength(1));
        _inventory[row, column] = inventorySlot;
        Debug.Log($"Item #{inventorySlot.Position} is in position #[{row}, {column}] and contains {inventorySlot.Item}. A total of {InventorySlot.TotalSlots}");
    }

    public void GrabItems(InventorySlot inventorySlot) {
        if (HeldItem.Item.Equals(null)) {
            HeldItem.Item = inventorySlot.Item;
            HeldItem.Quantity = inventorySlot.Quantity;
            ClearSlot(inventorySlot);
        }
    }

    public void StoreHalf(InventorySlot inventorySlot) {
        var half = HeldItem.Quantity / 2;
        inventorySlot.Quantity += half;
        HeldItem.Quantity -= half;
    }

    public void TakeHalf(InventorySlot inventorySlot) {
        var half = inventorySlot.Quantity / 2;
        HeldItem.Quantity += half;
        inventorySlot.Quantity -= half;
    }

    public void SwapItems(InventorySlot inventorySlot) {
        var tempSlot = inventorySlot;
        inventorySlot.Item = HeldItem.Item;
        inventorySlot.Quantity = HeldItem.Quantity;
        HeldItem.Item = tempSlot.Item;
        HeldItem.Quantity = tempSlot.Quantity;
    }

    public void AddSingleItem(InventorySlot inventorySlot) {
        if (HeldItem.Quantity > 0) {
            inventorySlot.Quantity += 1;
            HeldItem.Quantity -= 1;
        }
        else {
            ClearSlot(HeldItem);
        }
    }

    public void RemoveSingleItem(InventorySlot inventorySlot) {
        if (inventorySlot.Quantity > 0) {
            inventorySlot.Quantity -= 1;
            HeldItem.Quantity += 1;
        }
    }

    private void Start() {
        // Generate all inventory slots
        for (int i = _inventory.GetLength(1) * _inventory.GetLength(0); i > 0; i--) {
            StoreInProperSlot(new InventorySlot());
        }
    }
}


// To store item objects in an inventory array and return each item's specific properties.

/*
 * Incase I need it later
 * 
static PlayerInventory _instance;
public static PlayerInventory Instance {
    get {
        if (_instance == null) {
            _instance = FindObjectOfType<PlayerInventory>();
        }
        return _instance;
    }
}
*/

