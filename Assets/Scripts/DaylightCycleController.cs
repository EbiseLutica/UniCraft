using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightCycleController : Singleton<DaylightCycleController> {

	[SerializeField]
	private float realTimeOfADay = 20 * 60;

	public float RealTimeOfADay
	{
		get { return realTimeOfADay; }
		set { realTimeOfADay = value; }
	}

	public float RotateRatio => 360 / realTimeOfADay;

	public float GameTime { get; set; }

	public int Day { get; set; }

	// Use this for initialization
	void Start () {
		GameTime = RealTimeOfADay / 4;
	}
	
	// Update is called once per frame
	void Update () {
		GameTime += Time.deltaTime;
		if (GameTime > realTimeOfADay)
		{
			Day++;
			GameTime = 0;
		}

		transform.eulerAngles = new Vector3(GameTime * RotateRatio, 90, 0);
	}
}
