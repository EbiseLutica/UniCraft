using UnityEngine;

public class Block : BaseBehaviour
{
	[SerializeField]
	private float miningTime;
	public float MiningTime
	{
		get { return miningTime; }
		set { miningTime = value; }
	}

	[SerializeField]
	private Hardness hardness;
	public Hardness Hardness
	{
		get { return hardness; }
		set { hardness = value; }
	}

	[SerializeField]
	private BreakableTool breakableTool;
	public BreakableTool BreakableTool
	{
		get { return breakableTool; }
		set { breakableTool = value; }
	}

	public virtual void OnInteract(Vector3Int location, BaseBehaviour interacter) { }
}
