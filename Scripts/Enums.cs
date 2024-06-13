using Godot;

public class Enums
{
	public enum CharacterStatus
	{
		Idle,
		Walk,
		Sleep
	}

	public enum Direction
	{
		Left = -1,
		Right = 1
	}

	public static Direction FlipDirection(Direction dir)
	{
		return dir == Direction.Left ? Direction.Right : Direction.Left;
	}
}
