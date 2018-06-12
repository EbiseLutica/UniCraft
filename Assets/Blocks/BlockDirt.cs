using UnityEngine;

/// <summary>
/// 土ブロック．
/// </summary>
public class BlockDirt : BlockBase, ITickable
{
	public override BreakableTool BreakableTool => BreakableTool.Shovel;
	public override Hardness Hardness => Hardness.Softer;
	public override float MiningTime => 2;

	/// <summary>
	/// 草の伝搬処理を行います．
	/// </summary>
	/// <param name="location"></param>
	public void OnTick(Vector3Int location)
	{
		// 周囲8ブロックで，上3mから下1mの間に草ブロックが存在し，直上にブロックがなければ，草が伝搬する
		if (Chunk[location + Vector3Int.up].BlockId == "unicraft:air" &&
			(CheckGrass(location + new Vector3Int(-1, 0, -1)) ||
			CheckGrass(location + new Vector3Int(-1, 0,  0)) ||
			CheckGrass(location + new Vector3Int(-1, 0,  1)) ||
			CheckGrass(location + new Vector3Int( 0, 0,  1)) ||
			CheckGrass(location + new Vector3Int( 1, 0,  1)) ||
			CheckGrass(location + new Vector3Int( 1, 0,  0)) ||
			CheckGrass(location + new Vector3Int( 1, 0, -1)) ||
			CheckGrass(location + new Vector3Int( 0, 0, -1))))
			Chunk.SetBlock("unicraft:grass", location);
	}

	/// <summary>
	/// その座標周辺の草ブロックの有無を取得します．
	/// </summary>
	/// <param name="v"></param>
	/// <returns></returns>
	private bool CheckGrass(Vector3Int v)
	{
		for (int i = v.y - 1; i <= v.y + 3; i++)
			if (Chunk[new Vector3Int(v.x, i, v.z)].BlockId == "unicraft:grass")
				return true;
		return false;
	}
}