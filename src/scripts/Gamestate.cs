using Godot;
using System;
using System.Collections.Generic;
public enum GameMode
{
	MainMenu,
	MainGame,
	EditMode
} 

public partial class Gamestate : Node
{
	[Export] public Label debugText;
	[Export] public float decomposeTime;
	[Export] public float generatorTorque;
	[Export] public float generatorCooldown;
	[Export] public float maxCharge;
	[Export] public GameMode currentGamemode;
	public float _baseSizeX;
	public bool isPaused;
	public float _baseSizeY;
	public delegate void SwitchChargeHandler(bool _state);
	private Player _playerInstance;
	public Player playerInstance() => _playerInstance;
	public SwitchChargeHandler switchCharge;
	public delegate void SwitchGamemodeHandler(GameMode _gamemode);
	public SwitchGamemodeHandler SwitchGamemode;
	private float _currentCharge;
	private bool _isOn;
	private List<ISwitchSensible> switchSensible = new List<ISwitchSensible>();
	public bool isOn()=> _isOn;
	//Required for UI
	public float currentCharge() => _currentCharge;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this._currentCharge = maxCharge;
		SwitchGamemode += SelectGamemode;
		switchCharge += SwitchChargeVoid;
		
	}
	public void Init(Player _player)
	{
		this._playerInstance = _player;
		foreach(var child in this.GetChildren())
		{
			if(child as ISwitchSensible !=null)
			{
				child.AddToGroup("switchers");
				GD.Print($"Added {child} to switchers");
			}
		}
		_isOn = true;
	}

	private void SwitchChargeVoid(bool _state)
	{
		_isOn = _state;
		_currentCharge = _isOn?maxCharge:0.0f;
		GetTree().CallGroup("switchers","Switch",_state);
	}


	private void SelectGamemode(GameMode _currentGamemode)
	{
		this.currentGamemode = _currentGamemode;
		switch(this.currentGamemode)
		{
			case(GameMode.EditMode):
				isPaused = true;
				break;
			default:
				isPaused = false;
			break;
		}
		SwitchGamemode(this.currentGamemode);
	}
	public void UpdateDebug()
	{
		string _chargeVal = _currentCharge>0?_currentCharge.ToString():"OUT";
		debugText.Text = $"Charge:{_chargeVal}\nHealth: {_playerInstance.GetHealth()}";
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(debugText!=null)
			UpdateDebug();
		if(_currentCharge>0.0f)
		{
			_currentCharge-= (float)delta;
		}
		else
			if (_isOn)
				switchCharge(false);
	}
}
