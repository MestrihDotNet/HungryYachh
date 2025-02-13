using Godot;
using System;

public partial class Food : Area2D
{
    [Signal] public delegate void OnCatchEventHandler();
    [Export] public Sprite2D FoodSprite; // Reference to the FoodSprite
    private Random _random = new Random();
    
	
	[Export] float _fallSpeed = 100.0f;
	[Export] public int FoodWidth = 16;   // Width of each food item (16px)
    [Export] public int FoodHeight = 16;  // Height of each food item (16px)
    [Export] public int Columns = 3;      // Number of columns in the sprite sheet (3)
    [Export] public int Rows = 2;

    public override void _Ready()
	{
        AreaEntered += OnAreaEntered;

        int randomIndex = _random.Next(0, Columns * Rows); // Random number between 0 and 5 (6 items)

        // Calculate the X and Y position in the sprite sheet based on random index
        int x = (randomIndex % Columns) * FoodWidth; // Column in sprite sheet
        int y = (randomIndex / Columns) * FoodHeight; // Row in sprite sheet

        // Set the selected region for the sprite
        FoodSprite.RegionRect = new Rect2(x, y, FoodWidth, FoodHeight);
    }

	public override void _Process(double delta)
	{
        Position += new Vector2(0, _fallSpeed * (float)delta);
	}

    private void OnAreaEntered(Area2D area)
    {
        GD.Print("scored");
        EmitSignal(SignalName.OnCatch);

        QueueFree();

    }
}
