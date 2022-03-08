using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;
using UnityEngine.UI;

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
        public static int TotalSlots { get; set; }

        public InventorySlot() {
            Item = null;
            Position = TotalSlots;
            TotalSlots++;
        }
    }

    #endregion Slot Class

    private void GenerateSlots() {
        for (int i = _totalSlots; i > 0; i--) {
            InventorySlot inventorySlot = new InventorySlot();
            inventorySlot.Item = new None();
            inventorySlot.Quantity = 0;
            var column = inventorySlot.Position % _inventory.GetLength(1);
            var row = Mathf.FloorToInt(inventorySlot.Position / _inventory.GetLength(1));
            _inventory[row, column] = inventorySlot;
            Debug.Log($"Item #{inventorySlot.Position} is in position #[{row}, {column}] and contains {inventorySlot.Item}. A total of {InventorySlot.TotalSlots} slots exist");
        }
    }

    private void Start() {
        _totalSlots = _inventory.GetLength(1) * _inventory.GetLength(0);
        GenerateSlots();
        FindSlot(0, 1).Item = new Axe();
        FindSlot(0, 1).Quantity = 20;
        HeldItem.Item = new Stick();
        HeldItem.Quantity = 10;
    }


    private void ClearSlot(Slot slot) {
        slot.Item = null;
        slot.Quantity = 0;
    }

    private void CheckSlotIfEmpty(Slot inventorySlot) {
        if (inventorySlot.Quantity.Equals(0)) {
            inventorySlot.Item = new None();
        }
        if (_heldItem.Quantity.Equals(0)) {
            HeldItem.Item = new None();
        }
    }

    private int ItemPickupRemainder(InventorySlot inventorySlot) {
        var itemsInHand = HeldItem.Quantity;
        var limit = HeldItem.Item.MaxStackSize;
        var itemsInSlot = inventorySlot.Quantity;
        if (itemsInHand + itemsInSlot <= limit) {
            // return 0 if items in hand and slot within hand max
            return 0;
        }
        else {
            // return items needing to be subtracted from slot to equal the max
            return limit - itemsInHand;
        }
    }

    private int ItemPlaceRemainder(InventorySlot inventorySlot) {
        var itemsInHand = HeldItem.Quantity; // 45
        var limit = inventorySlot.Item.MaxStackSize; //64
        var itemsInSlot = inventorySlot.Quantity; // 25
        if (itemsInHand + itemsInSlot <= limit) {
            // return 0 if items in slot and hand are within slot max
            return 0;
        }
        else {
            // return items needing to be subtracted from hand to equal the max
            return limit - itemsInSlot;
        }
    }
    public string GetSlotData() {
        return $"{HeldItem.Quantity} of Item: {HeldItem.Item} exists in hand";
    }
    public string GetSlotData(int x, int y) {
        InventorySlot slot = FindSlot(x, y);
        return $"{slot.Quantity} of Item: {slot.Item} exists in Position #{slot.Position}";
    }

    public void OnLeftClick(int x, int y) {
        var currentSlot = FindSlot(x, y);
        if(currentSlot.Item.Equals(HeldItem.Item)) {

        }
        else {
            SwapItems(currentSlot);
        }
    }

    public void OnRightClick(int x, int y) {
        var currentSlot = FindSlot(x, y);

        SlotOneItem(currentSlot);
        
    }

    private void SwapItems(InventorySlot inventorySlot) {
        var tempItem = inventorySlot.Item;
        var tempQuantity = inventorySlot.Quantity;
        inventorySlot.Item = HeldItem.Item;
        inventorySlot.Quantity = HeldItem.Quantity;
        HeldItem.Item = tempItem;
        HeldItem.Quantity = tempQuantity;
    }

    private void SlotOneItem(InventorySlot inventorySlot) {
        Debug.Log("slot 1 item");
        if (inventorySlot.Item is None) {
            Debug.Log("new pool");
            inventorySlot.Item = HeldItem.Item;
            HeldItem.Quantity -= 1;
            inventorySlot.Quantity += 1;
        }
        else {
            Debug.Log("slotted");
            if (HeldItem.Quantity > 0 && inventorySlot.Quantity < inventorySlot.Item.MaxStackSize) {
                HeldItem.Quantity -= 1;
                inventorySlot.Quantity += 1;
            }
        }
    }



    public int GetQuantity(int x, int y) {
        return _inventory[x, y].Quantity;
    }
    public int GetQuantity() {
        return HeldItem.Quantity;
    }

    public InventorySlot FindSlot(int x, int y) {
        return _inventory[x, y];
    }

    public Image GetImage(int x, int y) {
        if (_inventory[x, y].Item is null) {
            return ImageHandler.Instance.GetIcon("None");
        }
        else {
            return _inventory[x, y].Item.Icon;
        }
    }
    public Image GetImage() {
        if (HeldItem.Item is null) {
            return ImageHandler.Instance.GetIcon("None");
        }
        else {
            return HeldItem.Item.Icon;
        }
    }
    


}


// To store item objects in an inventory array and return each item's specific properties.

/* 
 * 
 * 
 *         if (HeldItem.Item is None) { //if no held item
            PlaceItems(currentSlot);
        }
        else {
            if (currentSlot.Item is None) { //if no items in slot
                SwapItems(currentSlot);
            }
            else {
                if (currentSlot.Item.Equals(HeldItem.Item)) { //if held item is same as slot item
                    SwapItems(currentSlot);
                    Debug.Log("same item");
                    //if max slot
                    //else max
                }
                else { //if held item is not same as slot item
                    SwapItems(currentSlot);
                }
            }
        }
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * check if items in hand can carry items picked up, else pickup differece
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
 * 
 *     private int ItemPickupRemainder(InventorySlot inventorySlot) {
    var itemsInHand = HeldItem.Quantity;
    var limit = HeldItem.Item.MaxStackSize;
    var itemsInSlot = inventorySlot.Quantity;
    if (itemsInHand + itemsInSlot <= limit) {
        // return 0 if items in hand and slot within hand max
        return 0;
    }
    else {
        // return items needing to be subtracted from slot to equal the max
        return limit - itemsInHand;
    }
}

private int ItemPlaceRemainder(InventorySlot inventorySlot) {
    var itemsInHand = HeldItem.Quantity; // 45
    var limit = inventorySlot.Item.MaxStackSize; //64
    var itemsInSlot = inventorySlot.Quantity; // 25
    if (itemsInHand + itemsInSlot <= limit) {
        // return 0 if items in slot and hand are within slot max
        return 0;
    }
    else {
        // return items needing to be subtracted from hand to equal the max
        return limit - itemsInSlot;
    }
}
 */

