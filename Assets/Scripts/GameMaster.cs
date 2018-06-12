using UnityEngine;
using System.Collections;

public class GameMaster : Singleton<GameMaster>
{
	[SerializeField]
	PlayerEntity playerPrefab;

	public PlayerEntity Player { get; private set; }

	[SerializeField]
	private DeathHudBehaviour guiDeath;

	public string Name { get; set; } = "Xeltica";

	public static readonly string Version = "Indev 0.3.0";
	
	public static readonly string ShortVersion = "indev0.3";


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

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

		if (CursorLocked)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	public void ShowDeathGUI(string reason = null)
	{
		if (reason == null) reason = $"{0} は不明の死を遂げた";
		CursorLocked = false;
		guiDeath.Reason = string.Format(reason, UniCraft.Name);
		guiDeath.gameObject.SetActive(true);
	}

	public bool CursorLocked { get; set; }

	public void SpawnPlayer(Vector3 pos)
	{
		if (Player != null)
			Destroy(Player.gameObject);
		Player = Instantiate(playerPrefab.gameObject, pos, playerPrefab.transform.rotation).GetComponent<PlayerEntity>();
	}
}