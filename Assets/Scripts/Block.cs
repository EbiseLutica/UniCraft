using UnityEngine;

public class Block : BlockBase
{
	[SerializeField]
	private float miningTime;
	public override float MiningTime => miningTime;

	[SerializeField]
	private Hardness hardness;
	public　override Hardness Hardness => hardness;

	[SerializeField]
	private BreakableTool breakableTool;
	public　override BreakableTool BreakableTool => breakableTool;
}
