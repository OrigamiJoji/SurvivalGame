public class Fists : ITool {
    public string ToolName { get; set; }
    public float Durability { get; set; }
    public float Damage { get; set; }
    public float Range { get; set; }
    public double AttackSpeed { get; set; }

    public Fists() {
        ToolName = "Fists";
        Damage = 2;
        Range = 5;
        AttackSpeed = 0.5f;
    }
    
}
