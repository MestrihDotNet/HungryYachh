using Godot;
using System;

public partial class HeadArea : Area2D
{
    // Define events instead of signals
    public event Action OnHeadDetection;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D area)
    {
        OnHeadDetection?.Invoke(); // Trigger the event if there are subscribers
    }
}
