using UnityEngine;
using System.Collections;

public class GameMaster : Singleton<GameMaster>
{
	[SerializeField]
	PlayerEntity playerPrefab;

	public PlayerEntity Player { get; private set; }



	public static readonly string Version = "Indev 0.2.0";

	IEnumerator Start()
	{
		Application.targetFrameRate = 60;

		yield return ChunkRenderer.Instance.Populate(System.DateTime.Now.GetHashCode());
		SpawnPlayer(new Vector3(0, ChunkRenderer.Instance.GetSurfaceY(0, 0) + 2, 0));
	}

	public void SpawnPlayer(Vector3 pos)
	{
		if (Player != null)
			Destroy(Player.gameObject);
		Player = Instantiate(playerPrefab.gameObject, pos, playerPrefab.transform.rotation).GetComponent<PlayerEntity>();
	}
}