using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot {
    public Item Item { get; set; }
    public int Quantity { get; set; }
}


[System.Serializable]
public class InventorySlot : Slot {
    public int Position { get; set; }
    public static int TotalSlots { get; set; }

    public InventorySlot() {
        Position = TotalSlots;
        TotalSlots++;
    }
}

public class CraftingSlot : Slot {
    public int Position { get; set; }
    public static int TotalSlots { get; set; }

    public CraftingSlot() {
        Position = TotalSlots;
        TotalSlots++;
    }
}

