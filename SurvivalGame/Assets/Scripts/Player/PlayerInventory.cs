using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class PlayerInventory : MonoBehaviour {

    #region Data Members
    private InventorySlot[,] _inventory = new InventorySlot[3, 5];
    private int _totalSlots;

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
    #endregion Data Members

    #region Slot Class
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
    private void GenerateSlots(InventorySlot inventorySlot) {
        _totalSlots = _inventory.GetLength(1) * _inventory.GetLength(0);
        for (int i = _totalSlots; i > 0; i--) {
            var column = inventorySlot.Position % _inventory.GetLength(1);
            var row = Mathf.FloorToInt(inventorySlot.Position / _inventory.GetLength(1));
            _inventory[row, column] = inventorySlot;
            Debug.Log($"Item #{inventorySlot.Position} is in position #[{row}, {column}] and contains {inventorySlot.Item}. A total of {InventorySlot.TotalSlots}");
        }
    }
    #endregion Slot Class

    private void Start() {
        GenerateSlots(new InventorySlot());
    }





    private void ClearSlot(Slot slot) {
        slot.Item = null;
        slot.Quantity = 0;
    }

    private void EmptySlot(Slot inventorySlot) {
        if (inventorySlot.Quantity.Equals(0)) {
            inventorySlot.Item = null;
        }
        if(_heldItem.Quantity.Equals(0)) {
            HeldItem.Item = null;
        }
    }

    private void CheckQuantity(Slot inventorySlot) {
        if(HeldItem.Item.MaxStackSize > inventorySlot.Item.MaxStackSize) {

        }
    }

    private bool CanItemsBePickedUp() {
        return true;
    }

    private bool CanItemsBePlaced() {
        return true;
    }


    private void GrabItems(InventorySlot inventorySlot) {
        HeldItem.Item = inventorySlot.Item;
        HeldItem.Quantity = inventorySlot.Quantity;
        ClearSlot(inventorySlot);
    }

    private void StoreHalf(InventorySlot inventorySlot) {
        if (inventorySlot.Item.Equals(null)) {
            inventorySlot.Item = _heldItem.Item;
        }
        var half = HeldItem.Quantity / 2;
        inventorySlot.Quantity += half;
        HeldItem.Quantity -= half;
    }

    private void TakeHalf(InventorySlot inventorySlot) {
        var half = inventorySlot.Quantity / 2;
        HeldItem.Quantity += half;
        inventorySlot.Quantity -= half;
    }

    private void SwapItems(InventorySlot inventorySlot) {
        var tempSlot = inventorySlot;
        inventorySlot.Item = HeldItem.Item;
        inventorySlot.Quantity = HeldItem.Quantity;
        HeldItem.Item = tempSlot.Item;
        HeldItem.Quantity = tempSlot.Quantity;
    }

    private void AddSingleItem(InventorySlot inventorySlot) {
        if (HeldItem.Quantity > 0) {
            inventorySlot.Quantity += 1;
            HeldItem.Quantity -= 1;
        }
        else {
            ClearSlot(HeldItem);
        }
    }

    private void RemoveSingleItem(InventorySlot inventorySlot) {
        if (inventorySlot.Quantity > 0) {
            inventorySlot.Quantity -= 1;
            HeldItem.Quantity += 1;
        }
    }

    /* check if items in hand can carry items picked up, else pickup differece
     * check if items in hand can be placed
     * items in hand max should be equal to the max amount that can be stored of that item
     * 
     * on click left
     * if items held and items slot, swap items
     * if items held and no items slot, place items
     * if no items held and items in click, swap items
     * if no items held and no items slot, swap items
     * 
     * on click right
     * if items held and items slot: if same item, drop half
     * else swap
     * if no items held and items slot, pickup half
     * if items held and items slot, swap
     * if no items held and no items slot, swap
     * 
     * 
     */


    public void OnLeftClick(int x, int y) {
        var currentSlot = FindSlot(x, y);

        if (HeldItem.Item.Equals(null)) {
            //if no item held
            GrabItems(currentSlot);
        }
        else {
            //if item is held
            SwapItems(currentSlot);
        }

    }

    public void OnRightClick(int x, int y) {
        var currentSlot = FindSlot(x, y);
        if (HeldItem.Item.Equals(null)) {
            //If no item is held
            if (currentSlot.Item.Equals(null)) {
                //if no item in slot
                StoreHalf(currentSlot);
            }
            else {
                // if item is in slot
                TakeHalf(currentSlot);
            }
        }
        else if (_heldItem.Item.Equals(currentSlot.Item)) {
            //if holding the same item and right click
        }
    }

    public int GetQuantity(int x, int y) {
        return _inventory[x, y].Quantity;
    }

    public InventorySlot FindSlot(int x, int y) {
        return _inventory[x, y];
    }

    public Sprite GetImage(int x, int y) {
        return null;
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

