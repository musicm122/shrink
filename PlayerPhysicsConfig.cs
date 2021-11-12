using Godot;

public class PlayerPhysicsConfig
{
    [ExportEnumAsFlags(typeof(PlayerSize))]
    public PlayerSize Size;

    [Export]
    public Vector2 Scale;

    [Export]
    public int Speed;

    [Export]
    public int JumpForce;

    public int RunMultiplier = 2;

    public int Gravity = 800;
}
