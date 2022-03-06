using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class PlayerInventory : MonoBehaviour {
    // To store item objects in an inventory array and return each item's specific properties.

    static PlayerInventory _instance;
    public static PlayerInventory Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<PlayerInventory>();
            }
            return _instance;
        }
    }



    public Item EquippedItem { get; set; }

    public ITool EquippedItemToolStats {
        get {
            if (EquippedItem is ITool) {
                return (ITool) EquippedItem;
            }
            else {
                return new Fists();
            }
        }
    }

    [System.Serializable]
    public class InventorySlot {
        private static int TotalSlots { get; set; }
        public int Position { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }

        public InventorySlot() {
            Item = null;
            Position = TotalSlots;
            TotalSlots++;
            Debug.Log(this.Position + TotalSlots);
            PlayerInventory.Instance.StoreInProperSlot(this);
        }
    }
    public InventorySlot[,] inventory = new InventorySlot[3, 5];

    public void StoreInProperSlot(InventorySlot inventorySlot) {
        int row = inventorySlot.Position % inventory.GetLength(1);
        int column = Mathf.FloorToInt(inventorySlot.Position / inventory.GetLength(1));
        inventory[row, column] = inventorySlot;
        Debug.Log($"Item #{inventorySlot.Position} is in position #{row}, {column} and contains {inventorySlot.Item}");
    }


    private void Start() {
        for(int i = inventory.GetLength(1) * inventory.GetLength(0); i > 0; i--) {
            StoreInProperSlot(new InventorySlot);
           
        }
    }
}
