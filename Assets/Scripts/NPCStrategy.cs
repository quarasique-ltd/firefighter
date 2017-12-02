using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCStrategy : MonoBehaviour, IWorldGenerator
{
    public int NPCCount = 3;
    public int MinDistance = 3;

    private List<Vector3> NPCPlaces = new List<Vector3>();
    public GameObject NPCObj;

    private bool NPCExists(Vector3 newNpc)
    {
        bool exists = false;
        foreach (Vector3 position in NPCPlaces)
        {
            exists = exists || (Math.Abs(Math.Round(position.x) - Math.Round(newNpc.x)) <= MinDistance &&
                                Math.Abs(Math.Round(position.y) - Math.Round(newNpc.y)) <= MinDistance);
        }
        return exists;
    }

    public void Init()
    {
        for (int i = 0; i < NPCCount; i++)
        {
            Vector3 npcPos = new Vector3(Random.Range(1, 8), Random.Range(1, 8), 0);
            while (NPCExists(npcPos))
            {
                npcPos = new Vector3(Random.Range(1, 8), Random.Range(1, 8), 0);
            }
            GameObject newNPC = Instantiate(NPCObj, npcPos, Quaternion.identity);
            NPCPlaces.Add(npcPos);
        }
    }
}