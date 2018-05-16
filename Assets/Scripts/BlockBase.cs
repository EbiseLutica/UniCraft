using UnityEngine;

public abstract class BlockBase : BaseBehaviour
{
	public abstract float MiningTime { get; }
	public abstract Hardness Hardness { get; }
	public abstract BreakableTool BreakableTool { get; }

	public virtual void OnInteract(Vector3Int location, BaseBehaviour interacter) { }
	public virtual void OnTick(Vector3Int location) { }
}