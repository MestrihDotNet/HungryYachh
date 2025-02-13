using Godot;
using System;

public partial class Player : Area2D
{
    [Export] float _speed = 200.0f;
    [Export] float _margin = 52.0f;
    private AnimatedSprite2D animatedSprite;
    private bool isHumming = false;
    private bool isChewing = false;

    // Array to hold references to all RayCast2D nodes
    private RayCast2D[] mouthRayCasts;

    public override void _Ready()
    {
        // Initialize the array with all RayCast2D nodes under the Player
        mouthRayCasts = new RayCast2D[]
        {
            GetNode<RayCast2D>("MouthRayCastUp"),    // RayCast going straight up
            GetNode<RayCast2D>("MouthRayCastLeft"),  // RayCast at 45-degree left
            GetNode<RayCast2D>("MouthRayCastRight")  // RayCast at 45-degree right
        };

        // Get the AnimatedSprite2D
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        // Enable RayCasts
        foreach (var rayCast in mouthRayCasts)
        {
            rayCast.Enabled = true;
        }
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionPressed("right"))
        {
            Position += new Vector2(_speed * (float)delta, 0);
            animatedSprite.Play("drive_right");
        }
        else if (Input.IsActionPressed("left"))
        {
            Position -= new Vector2(_speed * (float)delta, 0);
            animatedSprite.Play("drive_left");
        } else if( isHumming == false && isChewing == false)
        {
            animatedSprite.Play("idle");
        }
            // Clamp position within viewport
            Rect2 vpr = GetViewportRect();
        Position = new Vector2(Mathf.Clamp(Position.X, vpr.Position.X + _margin, vpr.End.X - _margin), Position.Y);
    }

    public override void _PhysicsProcess(double delta)
    {
        // Loop through each RayCast2D
        foreach (var rayCast in mouthRayCasts)
        {
            if (rayCast.IsColliding())
            {
                Node2D collider = rayCast.GetCollider() as Node2D;

                if (collider != null && !isHumming) // Only trigger once
                {
                    isHumming = true;
                    GD.Print("Food detected!");  // Debugging output

                    // Play the "hum_left" animation
                    animatedSprite.Play("humming");

                    // Wait for 0.5 seconds before playing "chewing_left"
                    GetTree().CreateTimer(0.5f).Timeout += () =>
                    {
                        animatedSprite.Play("chewing");

                        // Wait for another 0.5 seconds before resetting isHumming
                        GetTree().CreateTimer(0.5f).Timeout += () =>
                        {
                            isHumming = false;
                        };
                    };

                    break; // Exit the loop once food is detected
                }
            }
        }
    }
}
