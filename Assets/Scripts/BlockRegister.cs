using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockRegister : Singleton<BlockRegister> 
{	
	[SerializeField]
	private BlockKeyValue[] blocks;

	public Block this[string key] => blocks.FirstOrDefault(kv => kv.Key == key).Block;
}

[System.Serializable]
public struct BlockKeyValue
{
	public string Key;
	public Block Block;
}