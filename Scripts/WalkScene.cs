using Godot;
using System;

public partial class WalkScene : Node2D
{
	[Export]
	public PackedScene CharacterScene;

	private Window window;
	//[Export]
	private Character character;

	private int taskbarPosition;
	private int screenWidth;
	private int targetPosition;

	public override void _Ready()
	{
		window = GetWindow();
		character = CharacterScene.Instantiate<Character>();
		AddChild(character);

		Rect2I screenRect = DisplayServer.ScreenGetUsableRect();

		if (window != null && character != null) 
		{
			window.TransparentBg = true;
			window.MinSize = character.SpriteSize;
			window.Size = character.SpriteSize;

			taskbarPosition = screenRect.Size.Y - character.SpriteSize.Y;
			screenWidth = screenRect.Size.X;

			window.Position = new Vector2I(DisplayServer.ScreenGetSize().X / 2 - character.SpriteSize.X / 2, taskbarPosition);
			character.CharacterStatus = Enums.CharacterStatus.Idle;
			character.FlipDirection();
		}
	}

	public override void _Input(InputEvent input)
	{
		if (input is InputEventMouseButton MouseEvent)
		{
			if (MouseEvent.IsPressed())
			{
				targetPosition = (int)MouseEvent.GlobalPosition.X;
				character.CharacterStatus = Enums.CharacterStatus.Walk;
			}
		}
	}

	public override void _Process(double delta)
	{
		if (character != null)
		{
			switch (character.CharacterStatus)
			{
				case Enums.CharacterStatus.Idle:
					HandleIdle(delta);
					break;
				case Enums.CharacterStatus.Walk:
					HandleWalk(delta);
					break;
			}
		}
	}

	private void HandleIdle(double delta)
	{
		// add go to sleep / start to walk logic 
	}

	private void HandleWalk(double delta)
	{
		if (window != null)
		{
			Vector2I position = window.Position;

			if (targetPosition > 0)
			{
				int diff = targetPosition - position.X;
				character.Direction = Enums.GetDirection(diff);
				if (Math.Abs(diff) < 20)
				{
					targetPosition = -1;
				}
			}

			// Moves the character
			position.X += Mathf.RoundToInt(character.WalkSpeed * delta * (int)character.Direction);
			Rect2I characterRect = new Rect2I(position, character.SpriteSize);
			// Keep character inside the screen
			if (!DisplayServer.ScreenGetUsableRect().Encloses(characterRect))
			{
				character.FlipDirection();
				position.X = Mathf.Clamp(position.X, 0, ClampOnScreenWidth(position.X, character.SpriteSize.X));
			}
			window.Position = position;
		}
	}

	public int ClampOnScreenWidth(int position, int charWidth)
	{
		return Mathf.Clamp(position, 0, screenWidth - charWidth);
	}
}
