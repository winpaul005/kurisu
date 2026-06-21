using Godot;
using System;

public partial class Generator : Node
{
	public Gamestate gameState;
	private float _spinCooldown;
	private MeshInstance3D _turbine;
	private bool _on;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameState = GetTree().Root.GetNode<Gamestate>("RootNameNode");
		gameState.switchCharge += launchCharge;
		_turbine = this.GetChild<MeshInstance3D>(1);
		
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
		}
		else
			_spinCooldown = gameState.generatorCooldown;
		if(_turbine!=null)
			_turbine.RotateZ(((float)delta*gameState.generatorTorque)*(_spinCooldown/gameState.generatorCooldown));
	}
}
