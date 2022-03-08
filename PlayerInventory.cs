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

    private void CheckIfSlotEmpty(Slot inventorySlot) {
        if (inventorySlot.Quantity.Equals(0)) {
            inventorySlot.Item = null;
        }
        if (_heldItem.Quantity.Equals(0)) {
            HeldItem.Item = null;
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


    #region Functions

    //if no items held, grab items and quantity, check if slot empty
    private void GrabItems(InventorySlot inventorySlot) {
        if (HeldItem.Item.Equals(null)) {
            HeldItem.Item = inventorySlot.Item;
            HeldItem.Quantity = inventorySlot.Quantity;
            CheckIfSlotEmpty(inventorySlot);

        }
    }
    // COMPLETE



    public void StoreHalf(InventorySlot inventorySlot) {
        var half = HeldItem.Quantity / 2;

    //if items in hand, store half if slot contains same item
    private void StoreHalf(InventorySlot inventorySlot) {
        // may exceed max

        var half = HeldItem.Quantity / 2;
        if (inventorySlot.Item.Equals(null)) {
            //if slot contains same item
            //add half if slot can contain max
            //else add remainder
            if (ItemPlaceRemainder(inventorySlot).Equals(0)) {
                inventorySlot.Quantity = 

            }
        }
        else {
            //if slot does not contain same item
            //set slot item to hand item
            //add half of hand items to slot

        }
        if (ItemPlaceRemainder(inventorySlot).Equals(0)) {
            // if 0 remainder
            //add all items to hand
            //clear slot
        }
        else {
            //else remainder
            //add remainder to hand
            //subtract remainder from slot
        }
        if (inventorySlot.Item.Equals(null)) {
            inventorySlot.Item = HeldItem.Item;
        }


        inventorySlot.Quantity += half;
        HeldItem.Quantity -= half;
    }


    public void TakeHalf(InventorySlot inventorySlot) {

    private void TakeHalf(InventorySlot inventorySlot) {
        // take half if can hold
        // else take all
        if (ItemPickupRemainder(inventorySlot) > 0) {
            return;
        }
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
    private void SwapItems(InventorySlot inventorySlot) {
        if (!HeldItem.Item.Equals(null)) {
            var tempSlot = inventorySlot;
            inventorySlot.Item = HeldItem.Item;
            inventorySlot.Quantity = HeldItem.Quantity;
            HeldItem.Item = tempSlot.Item;
            HeldItem.Quantity = tempSlot.Quantity;
        }
    }
    // COMPLETE

    public void AddSingleItem(InventorySlot inventorySlot) {
        if (HeldItem.Quantity > 0) {
    private void PlaceSingleItem(InventorySlot inventorySlot) {
        // may exceed slot max
        if (ItemPlaceRemainder(inventorySlot).Equals(0)) {
            inventorySlot.Quantity += 1;
            HeldItem.Quantity -= 1;
        }
        else {
            ClearSlot(HeldItem);
        }
        CheckIfSlotEmpty(inventorySlot);
    }

    public void RemoveSingleItem(InventorySlot inventorySlot) {
    private void GrabSingleItem(InventorySlot inventorySlot) {
        // may exceed hand max
        if (inventorySlot.Quantity > 0) {
            inventorySlot.Quantity -= 1;
            HeldItem.Quantity += 1;
        }
    }

    private void Start() {
        // Generate all inventory slots
        for (int i = _inventory.GetLength(1) * _inventory.GetLength(0); i > 0; i--) {
            StoreInProperSlot(new InventorySlot());
    #endregion Functions

    /* check if items in hand can carry items picked up, else pickup differece
     * check if items in hand can be placed
     * items in hand max should be equal to the max amount that can be stored of that item
     * 
     * on click left
     * if items held and items slot, swap items
     * if items held and no items slot, place items
     * if no items held and items in click, swap items
     * if no items held and no items slot, swap items
     * if too many items to pick up, swap items
     * 
     * on click right
     * if items held and items slot: if same item, drop half
     * else swap
     * if no items held and items slot, pickup half
     * if items held and items slot, swap
     * if no items held and no items slot, swap
     * if too manyy items to pick up, swap items
     * 
     * on middle click
     * if items held, place one item
     * if items 
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

