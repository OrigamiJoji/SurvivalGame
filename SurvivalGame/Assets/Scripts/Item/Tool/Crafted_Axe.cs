using UnityEngine;

public class Crafted_Axe : Item, ITool {
    public string ToolName { get; private set; }
    public float Durability { get; set; }
    public float Damage { get; private set; }
    public float Range { get; private set; }
    public double AttackSpeed { get; private set; }

    public Crafted_Axe() {
        Icon = ImageHandler.Instance.GetSprite(GetType().Name.ToString());
        ItemType = this.GetType();
        ToolName = "Crafted Axe";
        MaxStackSize = 1;
        Durability = 100;
        Damage = 5;
        Range = 6;
        AttackSpeed = 0.8f;
    }
}
