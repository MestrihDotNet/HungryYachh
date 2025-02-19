using Godot;
using System;

public partial class MouthArea : Area2D
{
    public event Action OnMouthDetection;
    public event Action OnMouthDetectVeggies;
    public event Action OnMouthDetectFood;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area is Veggies)
        {
            GD.Print("Veggies entered MouthArea!");
            OnMouthDetectVeggies?.Invoke();
        }
        else if (area is Food)  // Assuming Food is another Area2D type
        {
            GD.Print("Food entered MouthArea!");
            OnMouthDetectFood?.Invoke();
        }
        else
        {
            GD.Print("Unknown object entered MouthArea.");
        }
    }
}
