using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenerateWorld
{
	public class GenerateRooms : MonoBehaviour, IWorldGenerator
	{
		public Room[] rooms;
		public List<Vector3> innerFloor;
		public List<Vector3> innerWall;
		public void Init()
		{
			Debug.Log("started to generate rooms");
			Vector3 initPos = new Vector3(0, 0, 0);
			foreach (Room room in rooms)
			{
				int shapeX = room.getShapeX();
				int shapeY = room.getShapeY();
				initPos.x = 0 - shapeX / 2;
				innerWall.AddRange(room.GenerateWalls(initPos));
				innerFloor .AddRange(room.GenerateFloor(initPos));
				initPos.y += shapeY;
			}
			Debug.Log("finished to generate rooms");
		}
	}
}
