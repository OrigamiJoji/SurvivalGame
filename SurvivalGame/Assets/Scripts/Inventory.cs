using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    protected Slot[,] InventoryGrid { get; set; }
    protected int TotalSlots { get; set; }


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private int GetRow(int slotPos) {
        var row = Mathf.FloorToInt(slotPos / InventoryGrid.GetLength(1));
        return row;
    }
    private int GetColumn(int slotPos) {
        var column = slotPos % InventoryGrid.GetLength(1);
        return column;
    }
    public Inventory(Slot[,] inventory, int totalSlots) {
        InventoryGrid = inventory;
        TotalSlots = totalSlots;
    }

    private int GetPosition(int row, int column) {
        var pos = row * InventoryGrid.GetLength(1);
        pos += column;
        return pos;
    }

    private void GenerateSlots() {
        for (int i = 0; i < TotalSlots; i--) {

            Slot inventorySlot = new Slot();
            inventorySlot.Quantity = 0;
            inventorySlot.Item = new None();

            InventoryGrid[GetRow(i), GetColumn(i)] = inventorySlot;
        }
    }
}
