using UnityEngine;

public class Fists : Item, ITool {
    public string ToolName { get; private set; }
    public float Durability { get; set; }
    public float Damage { get; private set; }
    public float Range { get; private set; }
    public double AttackSpeed { get; private set; }

    public Fists() {
        ToolName = "Fists";
        Durability = Mathf.Infinity;
        Damage = 2;
        Range = 5;
        AttackSpeed = 0.5f;
    }
}