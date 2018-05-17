using UnityEngine;
using System.Collections;

public class GameMaster : Singleton<GameMaster>
{
	[SerializeField]
	PlayerEntity playerPrefab;

	public PlayerEntity Player { get; private set; }


	public string Name { get; set; } = "Xeltica";

	public static readonly string Version = "Indev 0.3.0";

	IEnumerator Start()
	{
		Application.targetFrameRate = 60;
		blockIds = BlockRegister.Instance.GetBlockIds();
		
		CursorLocked = true;

		yield return ChunkRenderer.Instance.Populate(System.DateTime.Now.GetHashCode());
		SpawnPlayer(new Vector3(0, ChunkRenderer.Instance.GetSurfaceY(0, 0) + 2, 0));
	}

	private string[] blockIds;

	int blockInHand;

	public string BlockIdInHand => blockIds[blockInHand];

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			blockInHand--;
		if (Input.GetKeyDown(KeyCode.RightArrow))
			blockInHand++;
		
		if (blockInHand < 0)
			blockInHand = blockIds.Length - 1;

		if (blockInHand >= blockIds.Length)
			blockInHand = 0;
			
	}

	public bool CursorLocked
	{
		set
		{
			Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
			Cursor.visible = !value;
		}
		get
		{
			return Cursor.lockState == CursorLockMode.Locked && !Cursor.visible;
		}
	}

	public void SpawnPlayer(Vector3 pos)
	{
		if (Player != null)
			Destroy(Player.gameObject);
		Player = Instantiate(playerPrefab.gameObject, pos, playerPrefab.transform.rotation).GetComponent<PlayerEntity>();
	}
}