using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CraftInventory : MonoBehaviour {


    private int _totalSlots = 9;
    private Slot[,] _craftingGrid = new Slot[3, 3];
    public Slot[,] CraftingGrid {
        get { return _craftingGrid; }
        set { _craftingGrid = CraftingGrid; }
    }



    private void Start() {
        GenerateSlots();
    }

    private void GenerateSlots() {
        for (int i = 0; i < _totalSlots; i++) {
            Slot craftingSlot = new Slot();
            craftingSlot.Item = new None();
            craftingSlot.Quantity = 0;

            _craftingGrid[GetRow(i), GetColumn(i)] = craftingSlot;
        }
        Debug.Log("Crafting table instantiated");
    }
    private int GetRow(int slotPos) {
        var row = Mathf.FloorToInt(slotPos / _craftingGrid.GetLength(1));
        return row;
    }
    private int GetColumn(int slotPos) {
        var column = slotPos % _craftingGrid.GetLength(1);
        return column;
    }

}


