using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ブロックの登録クラスです．インスペクター上でブロック登録を行います．
/// </summary>
/// <typeparam name="BlockRegister"></typeparam>
public class BlockRegister : Singleton<BlockRegister> 
{	
	[SerializeField]
	private BlockKeyValue[] blocks;

	public BlockBase this[string key] => blocks.FirstOrDefault(kv => kv.Key == key).Block;

	public string[] GetBlockIds() => blocks.Select(k => k.Key).ToArray();
	
}

[System.Serializable]
public struct BlockKeyValue
{
	public string Key;
	public BlockBase Block;
}