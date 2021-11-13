using Godot;

public class PlayerPhysicsConfig
{
    [ExportEnumAsFlags(typeof(PlayerSize))]
    public PlayerSize Size = PlayerSize.Normal;

    [Export]
    public Vector2 Scale = new Vector2(1f, 1f);

    [Export]
    public int Speed = 200;

    [Export]
    public int JumpForce = 800;

    public int RunMultiplier = 2;
    public int Gravity = 800;

    public override string ToString() =>
        "Size: " + Size + "\r\n" +
        "Scale: " + Scale + "\r\n" +
        "Speed: " + Speed + "\r\n" +
        "JumpForce: " + JumpForce + "\r\n" +
        "RunMultiplier: " + RunMultiplier + "\r\n" +
        "Gravity: " + Gravity + "\r\n";

}
