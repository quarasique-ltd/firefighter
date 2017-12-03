using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GenerateWorld
{
	public class GenerateHorRoom : MonoBehaviour, Room {

		public GameObject[] HorOutWallls;
		public GameObject[] HorWallpaperWallls;
		public GameObject[] HorNearWallls;
		public GameObject[] VertLeftWallls;
		public GameObject[] VertRightWallls;
		public GameObject[] LeftUpAngleWalls;
		public GameObject[] RitghUpAngleWalls;
		public GameObject[] LeftDownAngleWalls;
		public GameObject[] RightDownAngleWalls;

		public GameObject[] LeftDownPass;
		public GameObject[] RightDownPass;
    
		public GameObject[] LeftUpPass;
		public GameObject[] RightUpPass;
    
    
		public GameObject[] FloorObjects;

		public int shapeX = 10;
		public int shapeY =  8;

		public int getShapeY()
		{
			return shapeY;
		}

		public int getShapeX()
		{
			return shapeX;
		}

		public Vector3[] GenerateWalls(Vector3 initPos)
		{
			initPos.y -= shapeY / 2;
			List<Vector3> walls = new List<Vector3>();
			int yCounter = 1;
			int arrCounter = 0;
			while (yCounter < shapeY - 1)
			{
				if (Math.Abs(shapeY / 2 - yCounter) < 2 || yCounter - shapeY/2 == 1)
				{
					yCounter++;
					continue;
				}
				walls.Add(new Vector3(initPos.x, initPos.y + yCounter));
				Instantiate(VertLeftWallls[Random.Range(0, VertLeftWallls.Length)], walls[arrCounter], Quaternion.identity);
				arrCounter++;
				walls.Add(new Vector3(initPos.x+shapeX - 1, initPos.y + yCounter));
				Instantiate(VertRightWallls[Random.Range(0, VertRightWallls.Length)], walls[arrCounter], Quaternion.identity);
				arrCounter++;
				yCounter++;
			}
			int xCounter = 1;
			while (xCounter < shapeX - 1)
			{
				walls.Add(new Vector3(initPos.x + xCounter, initPos.y));
				Instantiate(HorNearWallls[Random.Range(0, HorNearWallls.Length)], walls[arrCounter], Quaternion.identity);
				arrCounter++;
				walls.Add(new Vector3(initPos.x + xCounter, initPos.y+shapeY - 1));
				Instantiate(HorOutWallls[Random.Range(0, HorOutWallls.Length)], walls[arrCounter], Quaternion.identity);
				arrCounter++;
				// wallpaper
				walls.Add(new Vector3(initPos.x + xCounter, initPos.y+shapeY - 2));
				Instantiate(HorWallpaperWallls[Random.Range(0, HorWallpaperWallls.Length)], walls[arrCounter], Quaternion.identity);
				arrCounter++;
				xCounter++;
			}
        
//        
        // wallpaper
        walls.Add(new Vector3(initPos.x, initPos.y + shapeY/2+1));
        Instantiate(HorWallpaperWallls[Random.Range(0, HorWallpaperWallls.Length)], walls[arrCounter], Quaternion.identity);
        arrCounter++;
        walls.Add(new Vector3(initPos.x +shapeX -1, initPos.y + shapeY/2+1));
        Instantiate(HorWallpaperWallls[Random.Range(0, HorWallpaperWallls.Length)], walls[arrCounter], Quaternion.identity);
        arrCounter++;
        
        
			// angles of the room
			walls.Add(new Vector3(initPos.x, initPos.y));
			Instantiate(LeftDownAngleWalls[Random.Range(0, LeftDownAngleWalls.Length)], walls[arrCounter], Quaternion.identity);
			arrCounter++;
			walls.Add(new Vector3(initPos.x+shapeX - 1, initPos.y));
			Instantiate(RightDownAngleWalls[Random.Range(0, RightDownAngleWalls.Length)], walls[arrCounter], Quaternion.identity);
			arrCounter++;
			walls.Add(new Vector3(initPos.x, initPos.y + shapeY -1));
			Instantiate(LeftUpAngleWalls[Random.Range(0, LeftUpAngleWalls.Length)], walls[arrCounter], Quaternion.identity);
			arrCounter++;
			walls.Add(new Vector3(initPos.x + shapeX -1, initPos.y + shapeY -1));
			Instantiate(RitghUpAngleWalls[Random.Range(0, RitghUpAngleWalls.Length)], walls[arrCounter], Quaternion.identity);
			arrCounter++;

			initPos.y += shapeY / 2;        
			// angles of the pass
			walls.Add(new Vector3(initPos.x, initPos.y  + 2));
			Instantiate(LeftUpPass[Random.Range(0, LeftUpPass.Length)], walls.Last(), Quaternion.identity);
			arrCounter++;
			walls.Add(new Vector3(initPos.x, initPos.y  - 1));
			Instantiate(LeftDownPass[Random.Range(0, LeftDownPass.Length)], walls.Last(), Quaternion.identity);
			arrCounter++;
			walls.Add(new Vector3(initPos.x + shapeX - 1, initPos.y +2));
			Instantiate(RightUpPass[Random.Range(0, RightUpPass.Length)], walls.Last(), Quaternion.identity);
			arrCounter++;
			walls.Add(new Vector3(initPos.x + shapeX - 1, initPos.y -1));
			Instantiate(RightDownPass[Random.Range(0, RightDownPass.Length)], walls.Last(), Quaternion.identity);

			return walls.ToArray();
		}

		public Vector3[] GenerateFloor(Vector3 initPos)
		{
			List<Vector3> floors = new List<Vector3>((shapeX - 2) * (shapeY - 3) + 3);
			for (int i = 1; i < shapeX - 1; i++)
			{
				for (int g = 1; g < shapeY - 2; g++)
				{
					floors.Add(new Vector3(initPos.x + i, initPos.y - shapeY / 2 + g));
				}
			}
			floors.Add(new Vector3(initPos.x + shapeX - 1,  initPos.y));
			floors.Add(new Vector3(initPos.x ,  initPos.y));
			
			foreach (Vector3 floor in floors)
			{
				Instantiate(FloorObjects[Random.Range(0, FloorObjects.Length)], floor, Quaternion.identity);
			}
        
			initPos.x -= shapeX / 2;
			initPos.y += shapeY / 2;
			return floors.ToArray();
		}

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
		
		}
	}
}
