using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerStrategy : MonoBehaviour, IWorldGenerator
{
	public GameObject PlayerObj; 
	
	public void Init()
	{
		Vector3 playerPos = new Vector3(0, 0, 0);
		GameObject player = Instantiate(PlayerObj, playerPos, Quaternion.identity);
	}
}
