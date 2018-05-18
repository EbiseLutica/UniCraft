using UnityEngine;

/// <summary>
/// すべてのブロックのベースクラスです．
/// </summary>
public abstract class BlockBase : BaseBehaviour
{
	public abstract float MiningTime { get; }
	public abstract Hardness Hardness { get; }
	public abstract BreakableTool BreakableTool { get; }
}