using System.Linq;
using Godot;

public class Player : KinematicBody2D
{

    private static PlayerPhysicsConfig DefaultConfig = new PlayerPhysicsConfig
    {
        Size = global::PlayerSize.Normal
    };

    private static PlayerPhysicsConfig[] configs = new PlayerPhysicsConfig[]{
        new PlayerPhysicsConfig {
            Size = global::PlayerSize.Small,
            Scale = new Vector2(0.5f, 0.5f),
            JumpForce = 200,
            Speed = 200,
            Gravity = 800
        },
        DefaultConfig,
        new PlayerPhysicsConfig {
            Size = global::PlayerSize.Large,
            Scale = new Vector2(1.5f, 1.5f),
            JumpForce = 500,
            Speed = 100,
            Gravity = 1200
        },
        new PlayerPhysicsConfig {
            Size = global::PlayerSize.XLarge,
            Scale = new Vector2(2f, 2f),
            JumpForce = 1000,
            Speed = 50,
            Gravity = 1600
        }
    };

    public PlayerPhysicsConfig CurrentConfig = DefaultConfig;

    [Export]
    public float SnapThreshold = 16;

    [Export]
    public PlayerSize Size
    {
        get => CurrentConfig.Size;
        set => CurrentConfig.Size = value;
    }

    [Export]
    public int Speed
    {
        get => CurrentConfig.Speed;
        set => CurrentConfig.Speed = value;
    }

    [Export]
    public int JumpForce
    {
        get => CurrentConfig.JumpForce;
        set => CurrentConfig.JumpForce = value;
    }

    [Export]
    public int Gravity
    {
        get => CurrentConfig.Gravity;
        set => CurrentConfig.Gravity = value;
    }

    [Export]
    public int RunMultiplier
    {
        get => CurrentConfig.RunMultiplier;
        set => CurrentConfig.RunMultiplier = value;
    }

    // public Vector2 Scale
    // {
    //     get => this.Transform.Scale;
    //     set => this.Transform.Scaled(value);
    // }

    [Export]
    private Vector2 CurrentVelocity = Vector2.Zero;

    private AnimatedSprite animatedSprite;
    private CollisionShape2D collider;

    private Camera2D camera;

    private const string AnimatedSpriteName = "AnimatedSprite";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite>(AnimatedSpriteName);
        collider = GetNode<CollisionShape2D>("CollisionShape2D");
        camera = GetNode<Camera2D>("PlayerCamera");

        ApplyDefaultConfig();
    }

    public void SetCameraZoom(Vector2 scale)
    {
        GD.Print("SetCameraZoom called with args: " + scale);
        GD.PrintT(
            "Old Camera Zoom X = " + this.camera.Zoom.x,
            "Old Camera Zoom Y = " + this.camera.Zoom.y
        );
        this.camera.Zoom = scale;
        GD.PrintT(
            "New Camera Zoom X = " + this.camera.Zoom.x,
            "New Camera Zoom Y = " + this.camera.Zoom.y
        );
    }

    private void GetInputMovement()
    {
        bool isRunning = Input.IsActionPressed(InputConstants.Run);

        if (Input.IsActionPressed(InputConstants.Right) && isRunning)
        {
            CurrentVelocity.x += Speed * RunMultiplier;
        }
        else if (Input.IsActionPressed(InputConstants.Right))
        {
            CurrentVelocity.x += Speed;
        }

        if (Input.IsActionPressed(InputConstants.Left) && isRunning)
        {
            CurrentVelocity.x -= Speed * RunMultiplier;
        }
        else if (Input.IsActionPressed(InputConstants.Left))
        {
            CurrentVelocity.x -= Speed;
        }
    }

    bool CanJump() => Input.IsActionJustPressed(InputConstants.Jump) && IsOnFloor();

    void JumpCheck()
    {
        if (CanJump())
        {
            CurrentVelocity.y -= JumpForce;
        }
    }

    Vector2 GetSnap() =>
         !Input.IsActionJustPressed(InputConstants.Jump) && IsOnFloor() ?
             Vector2.Down * SnapThreshold :
             Vector2.Zero;

    private void AnimationCheck(Vector2 velocity)
    {
        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed;
            animatedSprite.Play();
        }
        else
        {
            animatedSprite.Stop();
            return;
        }
        if (velocity.x != 0)
        {
            animatedSprite.Animation = AnimationConstants.Walk;
            animatedSprite.FlipV = false;
            animatedSprite.FlipH = velocity.x < 0;
        }
        else
        {
            animatedSprite.Animation = AnimationConstants.Idle;
        }

        if (velocity.y < 0 && !IsOnFloor())
        {
            animatedSprite.Animation = AnimationConstants.Jump;
        }
    }

    public void Die()
    {
        GD.Print("You Died");
        GetTree().ReloadCurrentScene();
    }

    public override void _PhysicsProcess(float delta)
    {
        CurrentVelocity.x = 0;
        GetInputMovement();
        JumpCheck();
        var snap = GetSnap();
        CurrentVelocity = MoveAndSlideWithSnap(CurrentVelocity, snap, Vector2.Up);
        // CurrentVelocity = (snap != Vector2.Zero) ?
        //     MoveAndSlideWithSnap(CurrentVelocity, snap, Vector2.Up) :
        //     MoveAndSlide(CurrentVelocity, Vector2.Up);

        CurrentVelocity.y += Gravity * delta;

        AnimationCheck(CurrentVelocity);
    }

    public void UseItem(Item item)
    {
        bool IsMatchingConfig(PlayerPhysicsConfig c) => (int)c.Size == (int)item.Type;
        GD.PrintS("Player used item with type " + item.Type.ToString());
        var config = configs.First(IsMatchingConfig);
        ApplyConfig(config);
    }

    void ApplyDefaultConfig()
    {
        GD.PrintS("ApplyDefaultConfig");
        ApplyConfig(DefaultConfig);
    }
    void ApplyConfig(PlayerPhysicsConfig config)
    {
        GD.PrintS("Applying Config " + config.ToString());
        this.CurrentConfig = config;
        this.Scale = config.Scale;
    }
}
