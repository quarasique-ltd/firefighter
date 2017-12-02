using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    public int shapeX, shapeY;

    public GameObject[] WallsObjects;
    public GameObject[] FloorObjects;
    
    public Vector3[] GenerateWalls(Vector3 initPos)
    {
        Vector3[] walls = new Vector3[(2 * shapeX - 6) + 2* shapeY];
        int yCounter = 0;
        int arrCounter = 0;
        while (yCounter < shapeY)
        {
            walls[arrCounter] = new Vector3(initPos.x, initPos.y + yCounter);
            arrCounter++;
            walls[arrCounter] = new Vector3(initPos.x+shapeX - 1, initPos.y + yCounter);
            arrCounter++;
            yCounter++;
        }
        int xCounter = 1;
        while (xCounter < shapeX - 1)
        {
            if (shapeX / 2 == xCounter)
            {
                xCounter++;
                continue;
            }
            walls[arrCounter] = new Vector3(initPos.x + xCounter, initPos.y);
            arrCounter++;
            walls[arrCounter] = new Vector3(initPos.x + xCounter, initPos.y+shapeY - 1);
            arrCounter++;
            xCounter++;
        }
        Debug.Log("wall generated");
        foreach (Vector3 wall in walls)
        {
            Instantiate(WallsObjects[Random.Range(0, WallsObjects.Length)], wall, Quaternion.identity);
        }
        return walls;
    }

    public Vector3[] GenerateFloor(Vector3 initPos)
    {
        Vector3[] floors = new Vector3[(shapeX - 2) * (shapeY - 2) + 1];
        for (int i = 1; i < shapeX - 1; i++)
        {
            for (int g = 1; g < shapeY - 1; g++)
            {
                floors[(i - 1) * (shapeY - 2) + (g - 1)] = new Vector3(initPos.x + i, initPos.y + g);
            }
        }
        Debug.Log("floor generated");
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
