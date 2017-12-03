using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenerateWorld
{
	public class GenerateRooms : MonoBehaviour, IWorldGenerator
	{
		public GenerateHorRoom[] rooms;
		private List<Vector3> innerFloor = new List<Vector3>();
		private List<Vector3> innerWall = new List<Vector3>();
		public void Init()
		{
			Debug.Log("started to generate rooms");
			Vector3 initPos = new Vector3(0, 0, 0);
			foreach (Room room in rooms)
			{
				int shapeX = room.getShapeX();
				int shapeY = room.getShapeY();
				innerWall.AddRange(room.GenerateWalls(initPos));
				innerFloor .AddRange(room.GenerateFloor(initPos));
				initPos.x += shapeX;
			}
			GameManager.instance.FloorTiles.AddRange(innerFloor);
		}
	}
}
