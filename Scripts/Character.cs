using Godot;
using System;

public partial class Character : Node2D
{
	[Export]
	public int WalkSpeed { get; private set; } = 150;
	[Export]
	private AnimatedSprite2D sprite2D;

	[Export]
	private Enums.CharacterStatus AllowedStatuses;

	public Vector2I SpriteSize { get; set; } = new Vector2I(64, 64);
	public Enums.CharacterStatus CharacterStatus { get { return characterStatus; } set { SetStatus(value); } }
	public Enums.Direction Direction { get { return direction; } set { SetDirection(value); } }

	private Enums.CharacterStatus characterStatus;
	private Enums.Direction direction;


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

	private void SetDirection(Enums.Direction newDirection)
	{
		if (direction != newDirection)
		{
			FlipDirection();
		}
	}

	public void FlipDirection()
	{
		direction = Enums.FlipDirection(direction);
		sprite2D.FlipH = (direction == Enums.Direction.Left); // if direction is left, flip H
	}
}
