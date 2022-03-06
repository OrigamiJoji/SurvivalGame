using UnityEngine;

public class Axe : Item, ITool {
    public string ToolName { get; private set; }
    public float Durability { get; set; }
    public float Damage { get; private set; }
    public float Range { get; private set; }
    public double AttackSpeed { get; private set; }

    public Axe() {
        ToolName = "Fists";
        Durability = 100;
        Damage = 5;
        Range = 6;
        AttackSpeed = 0.8f;
    }
}