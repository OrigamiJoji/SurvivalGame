using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    protected Slot[,] InventoryGrid { get; set; }
    protected int TotalSlots { get; set; }
    protected HeldItem Held {
        get { return HeldItem.Instance; } }


    protected int GetRow(int slotPos) {
        var row = Mathf.FloorToInt(slotPos / InventoryGrid.GetLength(1));
        return row;
    }
    protected int GetColumn(int slotPos) {
        var column = slotPos % InventoryGrid.GetLength(1);
        return column;
    }


    protected void GenerateInventory(Slot[,] inventory, int totalSlots) {
        InventoryGrid = inventory;
        TotalSlots = totalSlots;

        for (int i = 0; i < TotalSlots; i++) {

            Slot inventorySlot = new Slot();
            inventorySlot.Quantity = 0;
            inventorySlot.Item = new None();

            InventoryGrid[GetRow(i), GetColumn(i)] = inventorySlot;
            Debug.Log($"Slot #{i} is in {GetRow(i)},{GetColumn(i)} and contains {inventorySlot.Quantity} of {inventorySlot.Item}");
        }
    }
    private Slot GetNextSlot() {
        foreach (Slot inventorySlot in InventoryGrid) {
            if (inventorySlot.Item is None) {
                return inventorySlot;
            }
        }
        return null;
    }



    private int ItemPlaceRemainder(Slot inventorySlot) {
        var itemsInHand = HeldItem.Instance.Held.Quantity;
        var limit = inventorySlot.Item.MaxStackSize;
        var itemsInSlot = inventorySlot.Quantity;
        if (itemsInHand + itemsInSlot < limit) {
            // return all items if items in slot and hand are within slot max
            return itemsInHand;
        }
        else {
            // return items needing to be subtracted from hand to equal the max
            return limit - itemsInSlot;
        }
    }

    private void SlotAllItems(Slot inventorySlot) {
        var req = ItemPlaceRemainder(inventorySlot);
        inventorySlot.Quantity += req;
        HeldItem.Instance.Held.Quantity -= req;
        VerifySlot(inventorySlot);
    }

    private void SlotOneItem(Slot inventorySlot) {
        Item thisItem = inventorySlot.Item;
        if (thisItem is None) {
            if (HeldItem.Instance.Held.Quantity > 0) {
                inventorySlot.Item = HeldItem.Instance.Held.Item;
                HeldItem.Instance.Held.Quantity -= 1;
                inventorySlot.Quantity += 1;
                VerifySlot(inventorySlot);
            }
        }
        else {
            if (HeldItem.Instance.Held.Item.GetType() == thisItem.ItemType()) {
                if (HeldItem.Instance.Held.Quantity > 0 && inventorySlot.Quantity < inventorySlot.Item.MaxStackSize) {
                    HeldItem.Instance.Held.Quantity -= 1;
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

    private void TakeHalf(Slot inventorySlot) {
        if (inventorySlot.Quantity >= 2) {
            var half = inventorySlot.Quantity / 2;
            HeldItem.Instance.Held.Quantity += half;
            inventorySlot.Quantity -= half;
            HeldItem.Instance.Held.Item = inventorySlot.Item;
            VerifySlot(inventorySlot);
        }
    }


    public void PickupItem(Item item, int quantity) {
        Debug.Log($"Picking up {quantity} of {item}");
        int s = 0;
        foreach (Slot inventorySlot in InventoryGrid) {
            s++;
            if (inventorySlot.Item.GetType() == item.ItemType() && inventorySlot.Quantity < inventorySlot.Item.MaxStackSize) {
                if (quantity + inventorySlot.Quantity <= inventorySlot.Item.MaxStackSize) {
                    inventorySlot.Quantity += quantity;
                    break;
                }
                else {
                    var diff = inventorySlot.Item.MaxStackSize - inventorySlot.Quantity;
                    inventorySlot.Quantity += diff;
                    PickupItem(item, quantity -= diff);
                    break;
                }
            }
            if (s == TotalSlots - 1) {
                var nextSlot = GetNextSlot();
                nextSlot.Item = item;
                nextSlot.Quantity = quantity;
                break;
            }
        }
    }

    public bool IsOwned(Item item, int quantity) {
        foreach (Slot inventorySlot in InventoryGrid) {
            if (inventorySlot.Item.GetType() == item.ItemType() && inventorySlot.Quantity >= quantity) {
                return true;
            }
        }
        return false;
    }

    private void SwapItems(Slot inventorySlot) {
        var tempItem = inventorySlot.Item;
        var tempQuantity = inventorySlot.Quantity;
        inventorySlot.Item = HeldItem.Instance.Held.Item;
        inventorySlot.Quantity = HeldItem.Instance.Held.Quantity;
        HeldItem.Instance.Held.Item = tempItem;
        HeldItem.Instance.Held.Quantity = tempQuantity;
    }

    #region Action Methods
    public void OnLeftClick(int x, int y) {
        var currentSlot = FindSlot(x, y);
        if (HeldItem.Instance.Held.Item.ItemType() == currentSlot.Item.ItemType()) {
            SlotAllItems(currentSlot);
            Debug.Log("Slotted");
        }
        else {
            Debug.Log("Swapped");
            SwapItems(currentSlot);
        }
    }

    public void OnRightClick(int x, int y) {
        var currentSlot = FindSlot(x, y);
        if (HeldItem.Instance.Held.Item is None) {
            TakeHalf(currentSlot);
            Debug.Log("Took half");
        }
        else {
            SlotOneItem(currentSlot);
            Debug.Log("Slot one");
        }
    }
    #endregion Action Methods

    #region Public Methods
    public string GetName(int row, int column) {
        return InventoryGrid[row, column].Item.ToString().Replace("_", " ");
    }
    public string GetSlotData(int row, int column) {
        Slot slot = FindSlot(row, column);
        return $"{slot.Quantity} of Item: {slot.Item}. Type: {slot.Item.ItemType()}";
    }

    public int GetQuantity(int row, int column) {
        return InventoryGrid[row, column].Quantity;
    }
    public Slot FindSlot(int row, int column) {
        return InventoryGrid[row, column];
    }
    public Item GetItem(int row, int column) {
        return InventoryGrid[row, column].Item;
    }
    public Sprite GetSprite(int row, int column) {
        return InventoryGrid[row, column].Item.Icon();
    }

    #endregion Public Methods



    private void VerifySlot(Slot inventorySlot) {
        if (inventorySlot.Quantity.Equals(0)) {
            inventorySlot.Item = new None();
        }
        if (HeldItem.Instance.Held.Quantity.Equals(0)) {
            HeldItem.Instance.Held.Item = new None();
        }
    }

}
