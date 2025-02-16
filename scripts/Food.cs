using Godot;
using System;

public partial class Food : Area2D
{
    [Signal] public delegate void OnFoodCaughtEventHandler();
    [Export] public Sprite2D FoodSprite; // Reference to the FoodSprite
    private Random _random = new Random();
    
	
	[Export] float _fallSpeed = 100.0f;
	[Export] public int FoodWidth = 16;   // Width of each food item (16px)
    [Export] public int FoodHeight = 16;  // Height of each food item (16px)
    [Export] public int Columns = 3;      // Number of columns in the sprite sheet (3)
    [Export] public int Rows = 2;

    public override void _Ready()
	{

        int randomIndex = _random.Next(0, Columns * Rows); // Random number between 0 and 5 (6 items)

        // Calculate the X and Y position in the sprite sheet based on random index
        int x = (randomIndex % Columns) * FoodWidth; // Column in sprite sheet
        int y = (randomIndex / Columns) * FoodHeight; // Row in sprite sheet

        // Set the selected region for the sprite
        FoodSprite.RegionRect = new Rect2(x, y, FoodWidth, FoodHeight);

        AreaEntered += OnFoodAreaEntered;
    }

	public override void _Process(double delta)
	{
        Position += new Vector2(0, _fallSpeed * (float)delta);
        Rotation += 1.0f * (float)delta;
    }

    private void OnFoodAreaEntered(Area2D area)
    {
        // Check if the food has been caught by the mouth area
        if (area is MouthArea)  // Assuming MouthArea is the Area2D that detects food
        {
            QueueFree(); // Immediately remove the food after catching it
            EmitSignal(SignalName.OnFoodCaught);
        }
    }

}
