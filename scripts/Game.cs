using Godot;
using System;
using System.Threading.Tasks.Sources;

public partial class Game : Node2D
{
    [Export] private PackedScene _foodScene;
    [Export] private PackedScene _veggiesScene;
    [Export] private Timer _spawnTimer;
    [Export] private Timer _veggiesTimer;
    private const int FOOD_MARGIN = 52;
    [Export] public ProgressBar sugarBar;
    [Export] public ProgressBar healthBar;

    private int score = 0;
    public override void _Ready()
    {
        _spawnTimer.Timeout += SpawnFood;
        SpawnFood();
        _veggiesTimer.Timeout += SpawnVeggies;
        SpawnVeggies();
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
        food.OnFoodEating += OnFoodEating;
    }
    private void SpawnVeggies()
    {
        Rect2 vpr = GetViewportRect();
        Veggies veggies = (Veggies)_veggiesScene.Instantiate();
        AddChild(veggies);

        float rx = (float)GD.RandRange(vpr.Position.X + FOOD_MARGIN, vpr.End.X - FOOD_MARGIN);
        veggies.Position = new Vector2(rx, -100);
        veggies.OnVeggieEating += OnVeggieEating;
    }
    private void OnVeggieEating()
    {
        healthBar.Value += 7;
        sugarBar.Value -= 3;
    }
    private void OnFoodEating()
    {
        healthBar.Value -= 6;
        sugarBar.Value += 4;
    }
}