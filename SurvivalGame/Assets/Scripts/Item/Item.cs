using UnityEngine;
using System;

public abstract class Item {
    public int MaxStackSize { get; set; }
    public int CurrentAmount { get; set; }
    public Sprite Icon { get; set; }
    public Type ItemType { get; set; }

    public Item() {
        MaxStackSize = 64;
    }
    public Item(string name) {

    }
}

public abstract class Tool : Item {
    public int Tier { get; set; }
    public string ToolName { get; set; }
    public float Durability { get; set; }
    public float Damage { get; set; }
    public double AttackSpeed { get; set; }
    public float Range { get; set; }
    public Tool() {
        MaxStackSize = 1;
    }
}

public abstract class Axe : Tool {
    public Axe() {
        Range = 3f;
        AttackSpeed = 1.5f;
    }
}
public class Crafted_Axe : Axe {
    public Crafted_Axe() {
        Icon = ImageHandler.Instance.GetSprite(GetType().Name.ToString());
        ItemType = this.GetType();
        Tier = 1;
        ToolName = "Crafted Axe";
        Durability = 100;
        Damage = 5;
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

public sealed class Wood : Item {
    public Wood() {
        Icon = ImageHandler.Instance.GetSprite(GetType().Name.ToString());
        ItemType = GetType();
    }
}

public sealed class Stick : Item {
    public Stick() {
        Icon = ImageHandler.Instance.GetSprite(GetType().Name.ToString());
        ItemType = GetType();
    }

}

public class Fists : Tool {
    public Fists() {
        ToolName = "Fists";
        Durability = Mathf.Infinity;
        Damage = 2;
        Range = 5;
        AttackSpeed = 0.5f;
    }
}