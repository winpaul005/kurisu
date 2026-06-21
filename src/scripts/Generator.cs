using Godot;
using System;

public partial class Generator : Node,IInteractable
{
	public Gamestate gameState;
	private float _spinCooldown;
	private MeshInstance3D _turbine;
	private bool _on;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameState = GetTree().Root.GetNode("Breadboard") as Gamestate;
		gameState.switchCharge += launchCharge;
		_turbine = this.GetChild<MeshInstance3D>(1);
		_on = true;
	}

	private void launchCharge(bool _state)
	{
	   _on = _state;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(!_on)
		{
			if(_spinCooldown>0.0f)
			{
				_spinCooldown -= (float)delta;	
			}
			else
				_spinCooldown = 0.0f;			
		}
		else
			_spinCooldown = gameState.generatorCooldown;
		if(_turbine!=null)
			_turbine.RotateX(((float)delta*gameState.generatorTorque)*(_spinCooldown/gameState.generatorCooldown));
	}

	public void OnEnter(Node3D _victim)
	{
		var player = (_victim as Player);
		if(player!=null)
			player._victim = this;
	}

	public void OnExit(Node3D _victim)
	{
		var player = (_victim as Player);
		if(player!=null)
			if(player._victim==this)
				player._victim = null;
	}

	public void Interact(Node3D _victim)
	{
		gameState.switchCharge(true);
	}

}
