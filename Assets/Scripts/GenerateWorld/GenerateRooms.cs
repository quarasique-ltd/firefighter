using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenerateWorld
{
	public class GenerateRooms : MonoBehaviour, IWorldGenerator
	{
		public GameObject winFloor;
		public GameObject winWall;
		
		public GenerateHorRoom[] rooms;
		private List<Vector3> innerFloor = new List<Vector3>();
		private List<Vector3> innerWall = new List<Vector3>();
		public void Init()
		{
			Debug.Log("started to generate rooms");
			Vector3 initPos = new Vector3(0, 0, 0);
			int initX = (int) initPos.x;
			foreach (Room room in rooms)
			{
				int shapeX = room.getShapeX();
				int shapeY = room.getShapeY();
				innerWall.AddRange(room.GenerateWalls(initPos));
				innerFloor .AddRange(room.GenerateFloor(initPos));
				initPos.x += shapeX;
			}
			
			Instantiate(rooms[rooms.Length - 1].VertLeftWallls[0], new Vector3(initX - 1, initPos.y), Quaternion.identity);
			Instantiate(rooms[rooms.Length - 1].VertLeftWallls[0], new Vector3(initX - 1, initPos.y+1), Quaternion.identity);
			Instantiate(rooms[rooms.Length - 1].VertRightWallls[0], new Vector3(initPos.x, initPos.y), Quaternion.identity);
			Instantiate(rooms[rooms.Length - 1].VertRightWallls[0], new Vector3(initPos.x, initPos.y+1), Quaternion.identity);
			int y = (int) initPos.y + rooms[rooms.Length - 1].getShapeY()/2 - 3;
			
			Instantiate(winFloor, new Vector3(initPos.x - 2, y), Quaternion.identity);
			Instantiate(winWall, new Vector3(initPos.x - 2, y + 1), Quaternion.identity);
			
			GameManager.instance.FloorTiles.AddRange(innerFloor);
		}
	}
}
