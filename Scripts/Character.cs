using Godot;
using System;

public partial class Character : Node2D
{
	[Export]
	public int WalkSpeed { get; private set; } = 150;
	[Export]
	private AnimatedSprite2D sprite2D;

	public Vector2I Size { get; set; } = new Vector2I(64, 64);
	public Enums.CharacterStatus CharacterStatus { get { return characterStatus; } set { SetStatus(value); } }
	public Enums.Direction Direction { get; set; }

	private Enums.CharacterStatus characterStatus;

	public override void _Ready()
	{
		CharacterStatus = Enums.CharacterStatus.Idle;
	}

	public override void _Process(double delta)
	{
	}

	private void SetStatus(Enums.CharacterStatus newStatus)
	{
		characterStatus = newStatus;
		switch (characterStatus) 
		{
			case Enums.CharacterStatus.Idle:
				sprite2D.Play("idle");
				break;
			case Enums.CharacterStatus.Walk:
				sprite2D.Play("walk");
				break;
			case Enums.CharacterStatus.Sleep:
				break;
		}
	}

	public void FlipDirection()
	{
		Direction = Enums.FlipDirection(Direction);
		sprite2D.FlipH = (Direction == Enums.Direction.Left); // if direction is left, flip H
	}
}
