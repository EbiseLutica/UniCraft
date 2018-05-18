using UnityEngine;

/// <summary>
/// プレイヤーが寝ることができるベッドブロックです．
/// </summary>
public class BlockBed : BlockBase, IInteractable
{
	public override BreakableTool BreakableTool => BreakableTool.Axe;
	public override Hardness Hardness => Hardness.Softer;
	public override float MiningTime => 0.1f;

	/// <summary>
	/// 右クリックすると，昼夜を反転できます．
	/// </summary>
	/// <param name="location"></param>
	/// <param name="interacter"></param>
	public void OnInteract(Vector3Int location, BaseBehaviour interacter)
	{
		if (Daylight.IsDay)
		{
			Daylight.GameTime = Daylight.RealTimeOfADay / 4 * 3;
		}
		else
		{
			Daylight.Day++;
			Daylight.GameTime = Daylight.RealTimeOfADay / 4;
		}
	}
}