using Godot;
using System;
using System.Data.SqlTypes;

public partial class Paddle : Area2D
{
	[Export] float _speed = 200.0f;
	[Export] float _margin = 52.0f; //half the size of the paddle
	[Export] public AnimatedSprite2D animatedSprite;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("right") == true)
		{
			Position += new Vector2(_speed * (float)delta, 0);
			animatedSprite.Play("drive_right");
		}
		else if (Input.IsActionPressed("left") == true)
		{
			Position -= new Vector2(_speed * (float)delta, 0);
            animatedSprite.Play("drive_left");
        } else
		{
			animatedSprite.Play("idle");
		}

		Rect2 vpr = GetViewportRect();

		if (Position.X < vpr.Position.X + _margin)
		{
			Position = new Vector2(vpr.Position.X + _margin, Position.Y);
		}
		if (Position.X > vpr.End.X - _margin)
		{
			Position = new Vector2(vpr.End.X - _margin, Position.Y);
		}
	}
}
