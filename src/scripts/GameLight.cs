using Godot;
using System;
public interface ISwitchSensible
{
	public void Switch(bool _isOn);
}
public partial class GameLight : Node, ISwitchSensible
{
	[Export] public bool _isEmergency;
	private Light3D _targetLight;

	public void Switch(bool _isOn)
	{
		GD.Print($"Called switch {_isOn}");
		if(!_isEmergency)
			_targetLight.Visible = _isOn;
	}


	public override void _Ready()
	{
		_targetLight = this.GetChild<Light3D>(0);
	}


}
