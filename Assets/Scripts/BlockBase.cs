using UnityEngine;

public abstract class BlockBase : BaseBehaviour
{
	public abstract float MiningTime { get; }
	public abstract Hardness Hardness { get; }
	public abstract BreakableTool BreakableTool { get; }
}

public interface ITickable
{
	void OnTick(Vector3Int location);
}

public interface IInteractable
{
	void OnInteract(Vector3Int location, BaseBehaviour interacter);
}