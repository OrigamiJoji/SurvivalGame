using UnityEngine;
using System;

public abstract class Item {
    public int MaxStackSize { get; set; }
    public int CurrentAmount { get; set; }
    public Sprite Icon { get; set; }
    public Type ItemType { get; set; }

    public Item() {
        MaxStackSize = 20;
    }
}
