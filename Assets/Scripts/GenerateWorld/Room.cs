using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    public int shapeX, shapeY;

    public GameObject[] WallsObjects;

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
    
    public Vector3[] GenerateWalls(Vector3 initPos)
    {
        Vector3[] walls = new Vector3[(3* shapeX - 9) + 2* shapeY];
        int yCounter = 1;
        int arrCounter = 0;
        while (yCounter < shapeY - 1)
        {
            walls[arrCounter] = new Vector3(initPos.x, initPos.y + yCounter);
            Instantiate(VertLeftWallls[Random.Range(0, VertLeftWallls.Length)], walls[arrCounter], Quaternion.identity);
            arrCounter++;
            walls[arrCounter] = new Vector3(initPos.x+shapeX - 1, initPos.y + yCounter);
            Instantiate(VertRightWallls[Random.Range(0, VertRightWallls.Length)], walls[arrCounter], Quaternion.identity);
            arrCounter++;
            yCounter++;
        }
        int xCounter = 1;
        while (xCounter < shapeX - 1)
        {
            if (Math.Abs(shapeX / 2 - xCounter) < 2)
            {
                xCounter++;
                continue;
            }
            walls[arrCounter] = new Vector3(initPos.x + xCounter, initPos.y);
            Instantiate(HorNearWallls[Random.Range(0, HorNearWallls.Length)], walls[arrCounter], Quaternion.identity);
            arrCounter++;
            walls[arrCounter] = new Vector3(initPos.x + xCounter, initPos.y+shapeY - 1);
            Instantiate(HorOutWallls[Random.Range(0, HorOutWallls.Length)], walls[arrCounter], Quaternion.identity);
            arrCounter++;
            // wallpaper
            walls[arrCounter] = new Vector3(initPos.x + xCounter, initPos.y+shapeY - 2);
            Instantiate(HorWallpaperWallls[Random.Range(0, HorWallpaperWallls.Length)], walls[arrCounter], Quaternion.identity);
            arrCounter++;
            xCounter++;
        }
        
        
        // wallpaper
        walls[arrCounter] = new Vector3(initPos.x + shapeX / 2 - 1, initPos.y+shapeY - 2);
        Instantiate(HorWallpaperWallls[Random.Range(0, HorWallpaperWallls.Length)], walls[arrCounter], Quaternion.identity);
        arrCounter++;
        walls[arrCounter] = new Vector3(initPos.x + shapeX / 2 + 1, initPos.y+shapeY - 2);
        Instantiate(HorWallpaperWallls[Random.Range(0, HorWallpaperWallls.Length)], walls[arrCounter], Quaternion.identity);
        arrCounter++;
        
        
        // angles of the room
        walls[arrCounter] = new Vector3(initPos.x, initPos.y);
        Instantiate(LeftDownAngleWalls[Random.Range(0, LeftDownAngleWalls.Length)], walls[arrCounter], Quaternion.identity);
        arrCounter++;
        walls[arrCounter] = new Vector3(initPos.x+shapeX - 1, initPos.y);
        Instantiate(RightDownAngleWalls[Random.Range(0, RightDownAngleWalls.Length)], walls[arrCounter], Quaternion.identity);
        arrCounter++;
        walls[arrCounter] = new Vector3(initPos.x, initPos.y + shapeY -1);
        Instantiate(LeftUpAngleWalls[Random.Range(0, LeftUpAngleWalls.Length)], walls[arrCounter], Quaternion.identity);
        arrCounter++;
        walls[arrCounter] = new Vector3(initPos.x + shapeX -1, initPos.y + shapeY -1);
        Instantiate(RitghUpAngleWalls[Random.Range(0, RitghUpAngleWalls.Length)], walls[arrCounter], Quaternion.identity);
        arrCounter++;
        
        // angles of the pass
        walls[arrCounter] = new Vector3(initPos.x + (shapeX / 2) -1, initPos.y + shapeY -1);
        Instantiate(LeftUpPass[Random.Range(0, LeftUpPass.Length)], walls[arrCounter], Quaternion.identity);
        arrCounter++;
        walls[arrCounter] = new Vector3(initPos.x + (shapeX / 2) + 1, initPos.y + shapeY -1);
        Instantiate(RightUpPass[Random.Range(0, RightUpPass.Length)], walls[arrCounter], Quaternion.identity);
        arrCounter++;
        walls[arrCounter] = new Vector3(initPos.x + (shapeX / 2) -1, initPos.y);
        Instantiate(LeftDownPass[Random.Range(0, LeftDownPass.Length)], walls[arrCounter], Quaternion.identity);
        arrCounter++;
        walls[arrCounter] = new Vector3(initPos.x + (shapeX / 2) + 1, initPos.y);
        Instantiate(RightDownPass[Random.Range(0, RightDownPass.Length)], walls[arrCounter], Quaternion.identity);
        
        return walls;
    }

    public Vector3[] GenerateFloor(Vector3 initPos)
    {
        Vector3[] floors = new Vector3[(shapeX - 2) * (shapeY - 3) + 3];
        for (int i = 1; i < shapeX - 1; i++)
        {
            for (int g = 1; g < shapeY - 2; g++)
            {
                floors[(i - 1) * (shapeY - 3) + (g - 1)] = new Vector3(initPos.x + i, initPos.y + g);
            }
        }
        floors[floors.Length-3] = new Vector3(shapeX  /  2 + initPos.x, initPos.y+shapeY - 2);
        floors[floors.Length-2] = new Vector3(shapeX  /  2 + initPos.x, initPos.y);
        floors[floors.Length-1] = new Vector3(shapeX  /  2 + initPos.x, initPos.y + shapeY - 1);
        foreach (Vector3 floor in floors)
        {
            Instantiate(FloorObjects[Random.Range(0, FloorObjects.Length)], floor, Quaternion.identity);
        }
        
        return floors;
    }

    public int getShapeX()
    {
        return shapeX;
    }

    public int getShapeY()
    {
        return shapeY;
    }
    
}
