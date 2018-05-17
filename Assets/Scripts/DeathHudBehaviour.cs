using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathHudBehaviour : BaseBehaviour
{
	[SerializeField]
	private Text reason;

	public string Reason { get; set; }

	void Update()
	{
		reason.text = $@"<b>You Died!</b>
<size=18>{Reason}</size>";
	}


	public void Respawn()
	{
		Destroy(UniCraft.Player.gameObject);
		UniCraft.SpawnPlayer(new Vector3(0, Chunk.GetSurfaceY(0, 0) + 2, 0));
		UniCraft.CursorLocked = true;
		gameObject.SetActive(false);
	}
}
