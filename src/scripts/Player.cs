using Godot;
using System;

public partial class Player : Entity
{
	[Export] public float _cameraArmLength;
	[Export] public float _cameraArmUp;
	[Export] public Camera3D _playerCam;
	public IInteractable _victim;
	private bool isPaused;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		Healthpoints = 5;
		gameState.Init(this);
	}
	public int GetHealth() => Healthpoints;
	//I want to die
	public void HandlePause(bool _paused)
	{
		isPaused = _paused;
	}
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if(@event.IsActionPressed("ui_accept"))
		{
			velocity.Y = IsOnFloor()?velocity.Y:JumpVelocity;
			if(_victim!=null)
				_victim.Interact(this);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	public override void _PhysicsProcess(double delta)
	{
		_movementDir = isPaused?Vector2.Zero:Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		base._PhysicsProcess(delta);
		Vector3 neuPosition = _playerCam.Position.Lerp(new Vector3(this.Position.X,this.Position.Y + _cameraArmUp,this.Position.Z + _cameraArmLength),0.5f);
		if(!(_playerCam.Position-neuPosition).IsZeroApprox())
			_playerCam.Position = neuPosition;
	}
}
