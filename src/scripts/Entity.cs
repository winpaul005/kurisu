using Godot;
using System;

public enum BehaviourScheme
{
	Wander,
	Pursue
}
public interface IInteractable
{
	//Need to make Node3D to support signals
	public void OnEnter(Node3D _victim);
	public void OnExit(Node3D _victim);
	public void Interact(Node3D _victim);
}
public interface IEntity
{
	public void Punch(int hitPoints);
}
public partial class Entity : CharacterBody3D, IEntity
{
	public Gamestate gameState;
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	protected Vector2 _movementDir;
	protected int Healthpoints = 1;
	private float _decomposeTimeline;
	protected Vector3 velocity;
	protected bool isDead;
	protected BehaviourScheme _scheme;
	protected NavigationAgent3D _navigationAgent;
	protected Vector3? targetPos;
	protected float movementCooldown;
	public override void _Ready()
	{
		gameState = GetTree().Root.GetNode("Breadboard") as Gamestate;
		if(this as Player == null)
		{
			_decomposeTimeline = gameState.decomposeTime;
			_navigationAgent = GetChild<NavigationAgent3D>(2);
			_navigationAgent.VelocityComputed += OnVelocityComputed;
		}
		isDead = false;
	}
	private void SetMovementTarget(Vector3 movementTarget)
	{
		_navigationAgent.TargetPosition = movementTarget;
	}
	private void OnVelocityComputed(Vector3 safeVelocity)
	{
		velocity = safeVelocity; 
	}
	// public override void _Process(double delta)
	// {
	//     if(this as Player==null)
	// 	{
	// 		switch(_scheme)
	// 		{
	// 			case BehaviourScheme.Wander:

	// 			break;
	// 		}
	// 	}
	// }

	public override void _PhysicsProcess(double delta)
	{
		velocity = Velocity;
		if(this as Player ==null)
		{
			if(targetPos!=null)
			{
				
			}
		}

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}
		Vector3 direction = (Transform.Basis * new Vector3(_movementDir.X, 0, _movementDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}
		Velocity = velocity;
		MoveAndSlide();
	}
	public void CallDead()
	{
		//play death anim
		isDead = true;
	}
	public void Punch(int hitPoints)
	{
		Healthpoints-=hitPoints;
		if(Healthpoints<=0)
			CallDead();
	}
}
