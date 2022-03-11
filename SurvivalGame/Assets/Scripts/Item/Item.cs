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
    public Item(string name) {

    }
}

public abstract class Tool : Item {
    public string ToolName { get; }
    public float Durability { get; set; }
    public float Damage { get; }
    public double AttackSpeed { get; }
    public float Range { get; }
}

public abstract class Axe : Tool {
    public int Tier { get; set; }
    public Axe() {

    }
}

public abstract class Sword : Tool {
    public Sword() {

    }
}

public abstract class Pickaxe : Tool {

}

public abstract class Placeable : Item {

}

