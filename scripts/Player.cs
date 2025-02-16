using Godot;

public partial class Player : Area2D
{
    private HeadArea headArea;
    private MouthArea mouthArea;
    [Signal] public delegate void OnFoodCaughtEventHandler();
    [Export] float _speed = 200.0f;
    [Export] float _margin = 52.0f;
    private Timer chewingTimer;
    private AnimatedSprite2D animatedSprite;
    bool isHumming = false;
    bool isChewing = false;


  

    public override void _Ready()
    {
        headArea = GetNode<HeadArea>("HeadArea");
        mouthArea = GetNode<MouthArea>("MouthArea");
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        chewingTimer = new Timer();  // Create a new Timer
        AddChild(chewingTimer);  // Add Timer as a child of Player
        chewingTimer.WaitTime = 0.5f;  // Set the timer duration to match your animation
        chewingTimer.OneShot = true;  // Set the timer to fire only once
        chewingTimer.Timeout += OnChewingTimeout;

        headArea.OnHeadDetection += HandleHeadDetection;
        headArea.OnHeadAreaExited += HandleHeadAreaExited;
        mouthArea.OnMouthDetection += HandleMouthDetection;
    }

    public override void _Process(double delta)
    {
        // Basic horizontal movement logic.
        if (Input.IsActionPressed("right"))
        {
            Position += new Vector2(_speed * (float)delta, 0);
            animatedSprite.Play("drive_right");
        }
        else if (Input.IsActionPressed("left"))
        {
            Position -= new Vector2(_speed * (float)delta, 0);
            animatedSprite.Play("drive_left");
        } else if (isHumming == false && isChewing == false)
        {
            animatedSprite.Play("idle");
        }


            Rect2 viewportRect = GetViewportRect();
        Position = new Vector2(
            Mathf.Clamp(Position.X, viewportRect.Position.X + _margin, viewportRect.End.X - _margin),
            Position.Y
        );
    }


    private void OnChewingTimeout()
    {
        isChewing = false;
        isHumming = false;// Reset the flag after the timer ends
    }

    private void HandleHeadDetection()
    {
        GD.Print("Head area detected!");
        animatedSprite.Play("humming");
        isHumming = true;
        isChewing = false;
    }

    private void HandleHeadAreaExited()
    {
        GD.Print("area exited");
    }

    private void HandleMouthDetection()
    {
        GD.Print("heyooooooooo");
        animatedSprite.Play("chewing");  // Play the "chewing" animation
        isHumming = false;
        isChewing = true;
        chewingTimer.Start();  // Start the timer after starting the chewing animation
        EmitSignal(SignalName.OnFoodCaught);
    }
}
