using UnityEngine;

public abstract class Item {
    public int MaxStackSize { get; set; }
    public int CurrentAmount { get; set; }
    public Sprite Icon { get; set; }

    public Item() {
        MaxStackSize = 20;
    }
}
