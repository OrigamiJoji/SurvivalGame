using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item {
    public int MaxStackSize { get; set; }
    public int CurrentAmount { get; set; }
    public Sprite Icon { get; set; }

    public Item() {
        MaxStackSize = 20;
    }
}
