using UnityEngine;

public class BlockGrass : BlockBase
{
	public override BreakableTool BreakableTool => BreakableTool.Shovel;
	public override Hardness Hardness => Hardness.Softer;
	public override float MiningTime => 2;

	public override void OnTick(Vector3Int location)
	{
		// 圧死で土に戻る
		if (Chunk[location + Vector3Int.up].BlockId != "unicraft:air")
			Chunk.SetBlock("unicraft:dirt", location);
	}
}