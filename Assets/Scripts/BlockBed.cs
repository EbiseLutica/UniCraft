using UnityEngine;

public class BlockBed : BlockBase, IInteractable
{
	public override BreakableTool BreakableTool => BreakableTool.Axe;
	public override Hardness Hardness => Hardness.Softer;
	public override float MiningTime => 0.1f;

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