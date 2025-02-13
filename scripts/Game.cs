using Godot;
using System;

public partial class Game : Node2D
{
	int score = 0;
	const double GEM_MARGIN = 60.0f;
	[Export] private PackedScene _gemScene;
	[Export] private Timer _spawntimer;
	[Export] private Label label;

	public override void _Ready()
	{
		_spawntimer.Timeout += SpawnGem;

	}
	public override void _Process(double delta)
	{
	}

	private void SpawnGem()
	{
		Rect2 vpr = GetViewportRect();
		Gem gem = (Gem)_gemScene.Instantiate();
		AddChild(gem);
		float rx = (float)GD.RandRange(vpr.Position.X + GEM_MARGIN, vpr.End.X - GEM_MARGIN); //add a margin to not spawn on the edge of both sides

		gem.Position = new Vector2(rx, -50);
	}

}