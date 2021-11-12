using System.Linq;
using Godot;

public class Player : KinematicBody2D
{
    private static PlayerPhysicsConfig DefaultConfig = new PlayerPhysicsConfig
    {
        Size = global::PlayerSize.Normal,
        Scale = new Vector2(1f, 1f),
        JumpForce = 800,
        Speed = 200,
    };

    private static PlayerPhysicsConfig[] configs = new PlayerPhysicsConfig[]{
        new PlayerPhysicsConfig {
            Size = global::PlayerSize.Small,
            Scale = new Vector2(0.5f, 0.5f),
            JumpForce = 200,
            Speed = 200
        },
        DefaultConfig,
        new PlayerPhysicsConfig {
            Size = global::PlayerSize.Large,
            Scale = new Vector2(1.5f, 1.5f),
            JumpForce = 500,
            Speed = 100
        },
        new PlayerPhysicsConfig {
            Size = global::PlayerSize.XLarge,
            Scale = new Vector2(2f, 2f),
            JumpForce = 1000,
            Speed = 50
        }
    };

    public PlayerPhysicsConfig CurrentConfig = DefaultConfig;

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
        camera = GetNode<Camera2D>("Camera2D");
        ApplyDefaultConfig();
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

    void JumpCheck()
    {
        if (Input.IsActionJustPressed(InputConstants.Jump) && IsOnFloor())
        {
            CurrentVelocity.y -= JumpForce;
        }
    }

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
        ApplyDefaultConfig();
        GetTree().ReloadCurrentScene();
    }

    public override void _PhysicsProcess(float delta)
    {
        CurrentVelocity.x = 0;
        GetInputMovement();
        CurrentVelocity = MoveAndSlide(CurrentVelocity, Vector2.Up);
        CurrentVelocity.y += Gravity * delta;
        JumpCheck();
        AnimationCheck(CurrentVelocity);
    }

    public void UseItem(Item item)
    {
        bool IsMatchingConfig(PlayerPhysicsConfig c) => (int)c.Size == (int)item.Type;
        GD.PrintS("Player used item with type " + item.Type.ToString());
        var config = configs.First(IsMatchingConfig);
        ApplyConfig(config);
    }

    void ApplyDefaultConfig() => ApplyConfig(DefaultConfig);

    void ApplyConfig(PlayerPhysicsConfig config)
    {
        GD.PrintS("Applying Config " + config.Size.ToString());
        this.Size = config.Size;
        this.Speed = config.Speed;
        this.JumpForce = config.JumpForce;
        this.Transform.Scaled(config.Scale);
        this.animatedSprite.Scale = config.Scale;
        this.collider.Scale = config.Scale;
        this.CurrentConfig = config;
    }
}
