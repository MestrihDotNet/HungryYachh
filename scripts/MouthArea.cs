using Godot;
using System;

public partial class MouthArea : Area2D
{
    //[Signal] public delegate void OnMouthDetectionEventHandler();
    public event Action OnMouthDetection;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    private void OnAreaEntered(Area2D area)
    {
        OnMouthDetection?.Invoke(); // Trigger the event if there are subscribers
    }
    //public void OnAreayyyEntered(Area2D area)
    //{
    //    EmitSignal(SignalName.OnMouthDetection);
    //}
}
