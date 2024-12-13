using Godot;
using System;

public class Enums
{
	[Flags]
	public enum CharacterStatus
	{
		Idle = 1 << 1,
		Walk = 1 << 2,
		Sleep = 1 << 3,
		Die = 1 << 4,
		Resurrect = 1 << 5,
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

	public static Direction GetDirection(int val)
	{
		return val < 0 ? Direction.Left : Direction.Right;
	}
}
