using UnityEngine;

/// <summary>
/// 草ブロック．
/// </summary>
public class BlockGrass : BlockBase, ITickable
{
	public override BreakableTool BreakableTool => BreakableTool.Shovel;
	public override Hardness Hardness => Hardness.Softer;
	public override float MiningTime => 2;

	public void OnTick(Vector3Int location)
	{
		// 圧死で土に戻る
		if (Chunk[location + Vector3Int.up].BlockId != "unicraft:air")
			Chunk.SetBlock("unicraft:dirt", location);
	}
}