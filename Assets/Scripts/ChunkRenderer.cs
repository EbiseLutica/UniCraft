//  以下のサイトからいただきました
//  http://kan-kikuchi.hatenablog.com/entry/PerlinNoise
//
//  Created by kan kikuchi on 2016.3.14.
//  Modified by Xeltica

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

/// <summary>
/// ランダムにマップを生成する
/// </summary>
public class ChunkRenderer : Singleton<ChunkRenderer>
{

	//シード
	private int seed;

	//マップのサイズ
	[SerializeField]
	private int _width = 50;
	[SerializeField]
	private int _depth = 50;

	//高さの最大値
	[SerializeField]
	private float _maxHeight = 10;

	//起伏の激しさ
	[SerializeField]
	private float amp = 15f;

	//マップの大きさ
	[SerializeField]
	private float mapSize = 1f;

	private Dictionary<Vector3Int, LocationInfo> blocksDic = new Dictionary<Vector3Int, LocationInfo>();

	public LocationInfo this[Vector3Int loc] => blocksDic != null && blocksDic.ContainsKey(loc) ? blocksDic[loc] : new LocationInfo("unicraft:air", loc, null);


	public LocationInfo GetBlockInfoOf(GameObject game)
	{
		return blocksDic.Select(kv => kv.Value).FirstOrDefault(v => v.ActualObject == game);
	}

	public int Progress {get; private set; }
	public bool Loaded => Progress >= 100;

	//=================================================================================
	//初期化
	//=================================================================================

	protected override void Awake()
	{
		
	}

	void Update()
	{
		if (blocksDic.Count > 0)
		{
			for (int i = 0; i < 10; i++)
			{
				// ランダムティック更新
				var info = blocksDic.Values.ToArray()[Random.Range(0, blocksDic.Values.Count)];
				info.ActualObject.GetComponent<BlockBase>().OnTick(info.Location);
			}
		}
	}

	/// <summary>
	/// 指定したX,Z座標の最も高いY座標を返します．
	/// </summary>
	/// <param name="x"></param>
	/// <param name="z"></param>
	/// <returns></returns>
	public int GetSurfaceY(int x, int z) =>
		blocksDic
			// X, Z座標の一致するものを選び
			.Where(l => l.Key.x == x && l.Key.z == z)
			// X, Z座標の一致するものを選び
			.Select(l => l.Key.y)
			// 大きい順に並べ
			.OrderByDescending(l => l)
			// 上を取る
			.FirstOrDefault();

	public IEnumerator Populate(string seed)
		=> Populate(seed.GetHashCode());

	public IEnumerator Populate(int? seed = null)
	{
		//マップサイズ設定
		transform.localScale = new Vector3(mapSize, mapSize, mapSize);

		this.seed = seed.HasValue ? seed.Value % 100 : (int)Random.value * 100;
		var max = _width * _depth * 4;
		var time = Time.time;
		var pc = 0;
		//キューブ生成
		for (int x = -_width; x < _width; x++)
		{
			for (int z = -_depth; z < _depth; z++)
			{
				var pos = new Vector3Int(x, GetY(x, z), z);

				SetBlock("unicraft:grass", pos);

				var count = 0;
				for (int y = pos.y - 1; y >= 0; y--)
				{
					SetBlock(y == 0 ? "unicraft:bedrock"
					: count < 3 ? "unicraft:dirt"
					: "unicraft:stone", new Vector3Int(pos.x, y, pos.z));
					
					count++;
				}
				if (pc % 100 == 0)
				{
					yield return new WaitForEndOfFrame();
					time = Time.time;
				}
				pc++;
				Progress = (int)((double)pc / max * 100);
			}
		}
	}

	public LocationInfo SetBlock(string id, Vector3Int loc)
	{
		GameObject cube = Instantiate(BlockRegister.Instance[id].gameObject);
		cube.transform.parent = transform;
		cube.transform.localPosition = loc;

		return blocksDic[loc] = new LocationInfo(id, loc, cube);
	}

	//Y座標を求める．
	private int GetY(float x, float z)
	{
		float y = 0;

		float xSample = (x + seed) / amp;
		float zSample = (z + seed) / amp;

		float noise = Mathf.PerlinNoise(xSample, zSample);

		y = _maxHeight * noise;

		return Mathf.RoundToInt(y);
	}
}

public enum Hardness
{
	/// <summary>
	/// 木，金適正
	/// </summary>
	Softer,
	/// <summary>
	/// 石適正
	/// </summary>
	Soft,
	/// <summary>
	/// 鉄適正
	/// </summary>
	Normal,
	/// <summary>
	/// ダイヤ適正
	/// </summary>
	Hard,
	/// <summary>
	/// 適合ツール以外ではヒビすら入らない
	/// </summary>
	Harder
}

public enum BreakableTool
{
	None,
	Pickaxe,
	Axe,
	Shovel
}

/// <summary>
/// 座標のブロック情報を取得します．
/// </summary>
public struct LocationInfo
{
	public string BlockId { get; }
	public int Meta { get; set; }
	public GameObject ActualObject { get; }
	public Vector3Int Location { get; }
	public ITileEntity TileEntity { get; set; }
	public Renderer ActualRenderer => renderer != null ? renderer : renderer = (ActualObject != null ? ActualObject.GetComponent<Renderer>() : null);

	private Renderer renderer;

	public LocationInfo(string id, Vector3Int location, GameObject actualObject)
	{
		BlockId = id;
		Location = location;
		ActualObject = actualObject;
		renderer = null;
		TileEntity = null;
		Meta = 0;
	}
}

public interface ITileEntity
{

}