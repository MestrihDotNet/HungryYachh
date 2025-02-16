using Godot;
using System;

public partial class HeadArea : Area2D
{
    // Define events instead of signals
    public event Action OnHeadDetection;
    public event Action OnHeadAreaExited;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
        AreaExited += OnAreaExited;
    }

    private void OnAreaEntered(Area2D area)
    {
        OnHeadDetection?.Invoke(); // Trigger the event if there are subscribers
    }

    private void OnAreaExited(Area2D area)
    {
        OnHeadAreaExited?.Invoke(); // Trigger the event if there are subscribers
    }
}
