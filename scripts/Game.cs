using Godot;
using System;
using System.Threading.Tasks.Sources;

public partial class Game : Node2D
{
    [Export] private PackedScene _foodScene;
    [Export] private Timer _spawnTimer;
    private const int FOOD_MARGIN = 52;
    [Export] public ProgressBar progressbar;
    [Export] private Label scoreLabel;

    private int score = 0;
    public override void _Ready()
    {
        _spawnTimer.Timeout += SpawnFood;
        SpawnFood();
    }
    public override void _Process(double delta)
    {
    }

    private void SpawnFood()
    {
        Rect2 vpr = GetViewportRect();
        Food food = (Food)_foodScene.Instantiate();
        AddChild(food);

        float rx = (float)GD.RandRange(vpr.Position.X +FOOD_MARGIN, vpr.End.X -FOOD_MARGIN);
        food.Position = new Vector2(rx, -100);
    }
    private void _on_player_on_food_caught()
    {
        progressbar.Value += 10;
    }
}