using Godot;

public partial class Player : Area2D
{
    private HeadArea headArea;
    private MouthArea mouthArea;
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

        // Setup Timer for Chewing Animation
        chewingTimer = new Timer();
        AddChild(chewingTimer);
        chewingTimer.WaitTime = 0.5f;
        chewingTimer.OneShot = true;
        chewingTimer.Timeout += OnChewingTimeout;

        // Subscribe to the events correctly
        headArea.OnHeadDetection += HandleHeadDetection;
        mouthArea.OnMouthDetectVeggies += OnMouthDetectVeggies;
        mouthArea.OnMouthDetectFood += OnMouthDetectFood;
    }

    public override void _Process(double delta)
    {
        // Basic movement logic
        if (Input.IsActionPressed("right"))
        {
            Position += new Vector2(_speed * (float)delta, 0);
            animatedSprite.Play("drive_right");
        }
        else if (Input.IsActionPressed("left"))
        {
            Position -= new Vector2(_speed * (float)delta, 0);
            animatedSprite.Play("drive_left");
        }
        else if (!isHumming && !isChewing)
        {
            animatedSprite.Play("idle");
        }

        // Keep player within viewport bounds
        Rect2 viewportRect = GetViewportRect();
        Position = new Vector2(
            Mathf.Clamp(Position.X, viewportRect.Position.X + _margin, viewportRect.End.X - _margin),
            Position.Y
        );
    }

    private void OnChewingTimeout()
    {
        isChewing = false;
        isHumming = false; // Reset flags after chewing animation ends
    }

    private void HandleHeadDetection()
    {
        animatedSprite.Play("humming");
        isHumming = true;
        isChewing = false;
    }

    private void HandleMouthDetection()
    {
        animatedSprite.Play("chewing");
        isChewing = true;
        chewingTimer.Start();
    }

    private void OnVeggieEating()
    {
        animatedSprite.Play("damaged");
        isChewing = true;
        chewingTimer.Start();
        GD.Print("veggggggie");
    }

    private void OnMouthDetectVeggies()
    {
        GD.Print("Veggies detected! Playing damaged animation.");
        OnVeggieEating();  // Call the function when veggies are eaten
    }

    private void OnMouthDetectFood()
    {
        GD.Print("Food detected! Playing chewing animation.");
        HandleMouthDetection();  // Call chewing animation when food is eaten
    }
}
