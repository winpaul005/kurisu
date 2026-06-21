using Godot;
using System;
public enum GameMode
{
	MainMenu,
	MainGame,
	EditMode
} 

public partial class Gamestate : Node
{
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
	public bool isOn()=> _isOn;
	//Required for UI
	public float currentCharge() => _currentCharge;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this._currentCharge = maxCharge;
		SwitchGamemode += SelectGamemode;
		SwitchGamemode(this.currentGamemode);
	}
	public void Init(Player _player)
	{
		this._playerInstance = _player;
		switchCharge += SwitchChargeVoid;
	}

    private void SwitchChargeVoid(bool _state)
    {
        _isOn = _state;
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

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		
	}
}
