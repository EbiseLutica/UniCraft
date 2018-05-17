using UnityEngine;

public abstract class BaseBehaviour : MonoBehaviour
{
	protected GameMaster UniCraft => GameMaster.Instance;
	protected ChunkRenderer Chunk => ChunkRenderer.Instance;
	protected DaylightCycleController Daylight => DaylightCycleController.Instance;
}