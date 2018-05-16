using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpHudBehaviour : MonoBehaviour
{
	PlayerEntity player;

	Text text;

	void Start()
	{
		text = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		player = GameMaster.Instance.Player;
		text.text = player != null ? $"HP {player.Health} / {player.MaxHealth}" : "Dead";
	}
}
