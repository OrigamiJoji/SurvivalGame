public interface ITool {
    public string ToolName { get; }
    public float Durability { get; set; }
    public float Damage { get; }
    public double AttackSpeed { get; }
    public float Range { get; }
}