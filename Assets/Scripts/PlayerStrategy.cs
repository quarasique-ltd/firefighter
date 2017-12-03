using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerStrategy : MonoBehaviour, IWorldGenerator
{
	
	public void Init()
	{
		Vector3 playerPos = new Vector3(0, 0, 0);
		GameManager.instance.FloorTiles.Remove(playerPos);
		Vector3 playerPos2 = new Vector3(1, 0, 0);
		GameManager.instance.FloorTiles.Remove(playerPos2);
		Vector3 playerPos3 = new Vector3(0, -1, 0);
		GameManager.instance.FloorTiles.Remove(playerPos3);
		Vector3 playerPos4 = new Vector3(-1, 0, 0);
		GameManager.instance.FloorTiles.Remove(playerPos4);
		Vector3 playerPos5 = new Vector3(0, 1, 0);
		GameManager.instance.FloorTiles.Remove(playerPos5);
	}
}
