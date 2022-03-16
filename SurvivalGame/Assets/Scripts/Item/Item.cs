using UnityEngine;
using System;

#region Archetypes

public abstract class Item {
    public int MaxStackSize { get; protected set; }
    public Sprite Icon() { return ImageHandler.Instance.GetSprite(GetType().Name.ToString()); }
    public Type ItemType() { return GetType(); }

    public Item() {
        MaxStackSize = 64;
    }
}

public abstract class Tool : Item {
    public int Tier { get; protected set; }
    public string ToolName() { return GetType().Name.Replace("_", " "); }
    public float Durability { get; set; }
    public float Damage { get; set; }
    public double AttackSpeed { get; protected set; }
    public float Range { get; protected set; }
    public Tool() {
        MaxStackSize = 1;
    }
}

public abstract class Axe : Tool {
    public Axe() {
        Range = 1f;
        AttackSpeed = 1.5f;
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

#endregion Archetypes

#region Tools

public sealed class Crafted_Axe : Axe {
    public Crafted_Axe() {
        Tier = 1;
        Durability = 100;
        Damage = 5;
    }
}

public class Crafted_Pickaxe : Pickaxe {
    public Crafted_Pickaxe() {
        MaxStackSize = 1;
        Durability = 100;
        Damage = 5;
        Range = 6;
        AttackSpeed = 0.8f;
    }
}

#endregion Tools

#region Items

public sealed class Wood : Item {
    public Wood() {
    }
}

public sealed class Stick : Item {
    public Stick() {
    }

}

#endregion Items

public sealed class Fists : Tool {
    public Fists() {
        Durability = Mathf.Infinity;
        Damage = 2;
        Range = 5;
        AttackSpeed = 0.5f;
    }
}