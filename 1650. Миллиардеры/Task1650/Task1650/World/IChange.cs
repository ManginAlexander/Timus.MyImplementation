using System;

namespace Task1650.World
{
	internal interface IChange
	{
		Int64 GetValue { get; }
		event Action<int, long> Update;
	}
}