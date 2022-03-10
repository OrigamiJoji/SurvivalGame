using UnityEngine;
using System;
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
        public static int TotalSlots { get; set; }

        public InventorySlot() {
            Item = null;
            Position = TotalSlots;
            TotalSlots++;
        }
    }

    #endregion Slot Class
    private int GetRow(int slotPos) {
        var row = Mathf.FloorToInt(slotPos / _inventory.GetLength(1));
        return row;
    }
    private int GetColumn(int slotPos) {
        var column = slotPos % _inventory.GetLength(1);
        return column;
    }


    private void GenerateSlots() {
        for (int i = _totalSlots; i > 0; i--) {
            InventorySlot inventorySlot = new InventorySlot();
            inventorySlot.Item = new None();
            inventorySlot.Quantity = 0;

            _inventory[GetRow(inventorySlot.Position), GetColumn(inventorySlot.Position)] = inventorySlot;
            Debug.Log($"Item #{inventorySlot.Position} is in position #[{GetRow(inventorySlot.Position)}, {GetColumn(inventorySlot.Position)}] and contains {inventorySlot.Item}. A total of {InventorySlot.TotalSlots} slots exist");
        }
    }

    private void Start() {
        _totalSlots = _inventory.GetLength(1) * _inventory.GetLength(0);
        GenerateSlots();
        FindSlot(0, 1).Item = new Axe();
        FindSlot(0, 1).Quantity = 20;

        FindSlot(1, 3).Item = new Stick();
        FindSlot(1, 3).Quantity = 20;

        HeldItem.Item = new None();
        HeldItem.Quantity = 0;
    }



    private int ItemPlaceRemainder(InventorySlot inventorySlot) {
        var itemsInHand = HeldItem.Quantity; // 45 // 2
        var limit = inventorySlot.Item.MaxStackSize; //64 // 20
        var itemsInSlot = inventorySlot.Quantity; // 25 // 20
        if (itemsInHand + itemsInSlot < limit) {
            // return all items if items in slot and hand are within slot max
            return itemsInHand;
        }
        else {
            // return items needing to be subtracted from hand to equal the max
            return limit - itemsInSlot;
        }
    }

    private void SlotAllItems(InventorySlot inventorySlot) {
        var req = ItemPlaceRemainder(inventorySlot);
        Debug.Log(ItemPlaceRemainder(inventorySlot));
        inventorySlot.Quantity += req;
        HeldItem.Quantity -= req;
        VerifySlot(inventorySlot);
    }

    private void SlotOneItem(InventorySlot inventorySlot) {
        Item thisItem = inventorySlot.Item;
        if (thisItem is None) {
            if (HeldItem.Quantity > 0) {
                inventorySlot.Item = HeldItem.Item;
                HeldItem.Quantity -= 1;
                inventorySlot.Quantity += 1;
                VerifySlot(inventorySlot);
            }
        }
        else {
            if (HeldItem.Item.GetType() == thisItem.ItemType) {
                if (HeldItem.Quantity > 0 && inventorySlot.Quantity < inventorySlot.Item.MaxStackSize) {
                    HeldItem.Quantity -= 1;
                    inventorySlot.Quantity += 1;
                    VerifySlot(inventorySlot);
                }
            }
            else {
                SwapItems(inventorySlot);
                VerifySlot(inventorySlot);
            }
        }
    }

    private void TakeHalf(InventorySlot inventorySlot) {
        if (inventorySlot.Quantity >= 2) {
            var half = inventorySlot.Quantity / 2;
            HeldItem.Quantity += half;
            inventorySlot.Quantity -= half;
            HeldItem.Item = inventorySlot.Item;
            VerifySlot(inventorySlot);
        }
    }
    private InventorySlot GetNextSlot() {
        foreach(InventorySlot inventorySlot in _inventory) {
            if(inventorySlot.Item is None) {
                return inventorySlot;
            }
        }
        return null;
    }

    public void PickupItem(Item item, int quantity) {
        foreach(InventorySlot inventorySlot in _inventory) {
            if(inventorySlot.Item.Equals(item)) {
                if(quantity + inventorySlot.Quantity <= inventorySlot.Item.MaxStackSize) {
                    //if can pick up all items
                    //pick up
                }
                else {
                    //if remainder
                    var diff = inventorySlot.Item.MaxStackSize - inventorySlot.Quantity;
                    quantity -= diff;
                    inventorySlot.Quantity += diff;
                    PickupItem(item, quantity);
                }
            }
            else {
                var nextSlot = GetNextSlot();
                nextSlot.Item = item;
                nextSlot.Quantity = quantity;
            }
        }
    }


    #region Action Methods
    public void OnLeftClick(int x, int y) {
        var currentSlot = FindSlot(x, y);
        if (HeldItem.Item.GetType() == currentSlot.Item.ItemType) {
            SlotAllItems(currentSlot);
        }
        else {
            SwapItems(currentSlot);
        }
    }

    public void OnRightClick(int x, int y) {
        var currentSlot = FindSlot(x, y);
        if (HeldItem.Item is None) {
            TakeHalf(currentSlot);
        }
        else {
            SlotOneItem(currentSlot);
        }
    }
    #endregion Action Methods

    #region Public Methods
    public string GetSlotData() {
        return $"{HeldItem.Quantity} of Item: {HeldItem.Item} exists in hand. Type: {HeldItem.Item.GetType()}";
    }
    public string GetSlotData(int x, int y) {
        InventorySlot slot = FindSlot(x, y);
        return $"{slot.Quantity} of Item: {slot.Item} exists in Position #{slot.Position}. Type: {slot.Item.ItemType}";
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
    public Item GetItem(int x, int y) {
        return _inventory[x, y].Item;
    }
    public Item GetItem() {
        return HeldItem.Item;
    }

    public Sprite GetSprite() {
        return HeldItem.Item.Icon;
    }
    public Sprite GetSprite(int x, int y) {
        return _inventory[x, y].Item.Icon;
    }

    #endregion Public Methods

    private void SwapItems(InventorySlot inventorySlot) {
        var tempItem = inventorySlot.Item;
        var tempQuantity = inventorySlot.Quantity;
        inventorySlot.Item = HeldItem.Item;
        inventorySlot.Quantity = HeldItem.Quantity;
        HeldItem.Item = tempItem;
        HeldItem.Quantity = tempQuantity;
    }

    private void ClearSlot(Slot slot) {
        slot.Item = new None();
        slot.Quantity = 0;
    }

    private void VerifySlot(Slot inventorySlot) {
        if (inventorySlot.Quantity.Equals(0)) {
            inventorySlot.Item = new None();
        }
        if (HeldItem.Quantity.Equals(0)) {
            HeldItem.Item = new None();
        }
    }

}

