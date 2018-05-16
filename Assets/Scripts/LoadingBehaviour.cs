using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBehaviour : BaseBehaviour
{
	
	Text text;

	void Update ()
	{
		if (text == null)
		{
			text = GetComponentInChildren<Text>();
			return;
		}

		text.text = $@"Chunk Loading...\n{ChunkRenderer.Instance.Progress}%";
		
		// 読み終わったら削除
		if (ChunkRenderer.Instance.Loaded)
			this.gameObject.SetActive(false);

	}
}
