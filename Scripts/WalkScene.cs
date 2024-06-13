using Godot;
using System;

public partial class WalkScene : Node2D
{
	[Export]
	public PackedScene CharacterScene;

	private Window window;
	private Character character;

	private int taskbarPosition;
	private int screenWidth;

	public override void _Ready()
	{
		window = GetWindow();
		character = CharacterScene.Instantiate<Character>();
		Rect2I screenRect = DisplayServer.ScreenGetUsableRect();

		if (window != null && character != null) 
		{
			window.MinSize = character.Size;
			window.Size = character.Size;

			taskbarPosition = screenRect.Size.Y - character.Size.Y;
			screenWidth = screenRect.Size.X;

			window.Position = new Vector2I(DisplayServer.ScreenGetSize().X / 2 - character.Size.X / 2, taskbarPosition);
		}
	}


	public override void _Process(double delta)
	{
		if (character != null)
		{
			switch (character.CharacterStatus)
			{
				case Enums.CharacterStatus.Walk:
					Walk(delta);
					break;
			}
		}
	}

	public void Walk(double delta)
	{
		if (window != null)
		{
			Vector2I position = window.Position;

			// Moves the character
			position.X += Mathf.RoundToInt(character.WalkSpeed * delta * (int)character.Direction);

			// Keep character inside the screen
			if (!DisplayServer.ScreenGetUsableRect().HasPoint(position))
			{
				character.FlipDirection();
				position.X = Mathf.Clamp(position.X, 0, ClampOnScreenWidth(position.X, character.Size.X));
			}
			window.Position = position;
		}
	}

	public int ClampOnScreenWidth(int position, int charWidth)
	{
		return Mathf.Clamp(position, 0, screenWidth - charWidth);
	}
}
