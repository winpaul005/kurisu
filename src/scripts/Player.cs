using Godot;
using System;

public partial class Player : Entity
{
	 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	public void HandlePause(bool _paused)
	{
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		base._PhysicsProcess(delta);
	}
}
