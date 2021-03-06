﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireStrategy : MonoBehaviour, IWorldUpdater, IWorldGenerator
{
    public int FireLevelForSpread;
    public float FireLevelSpreadProb;
    public float FireLevelUpProb;

    private List<Fire> FirePlaces = new List<Fire>();
    public GameObject FireObj;
    private int level = 0;
    private double eps = 0.1;

    public int everySteps = 10;
    private int currentStep = 0;

    private bool IsFireExists(Vector3 fire, List<Fire> FirePlaces)
    {
        foreach (Fire firePlace in FirePlaces)
        {
            Vector3 currentPos = firePlace.transform.position;
            if (Math.Abs(Math.Round(currentPos.x) - Math.Round(fire.x)) < eps &&
                Math.Abs(Math.Round(currentPos.y) - Math.Round(fire.y)) < eps)
            {
                return true;
            }
        }
        return false;
    }

    public Vector3[] getNeighbours(Vector3 position)
    {
        Vector3[] firePositions = new Vector3[8];
        firePositions[0] = new Vector3(position.x, position.y + 1);
        firePositions[1] = new Vector3(position.x, position.y - 1);
        firePositions[2] = new Vector3(position.x + 1, position.y + 1);
        firePositions[3] = new Vector3(position.x + 1, position.y - 1);
        firePositions[4] = new Vector3(position.x + 1, position.y);
        firePositions[5] = new Vector3(position.x - 1, position.y + 1);
        firePositions[6] = new Vector3(position.x - 1, position.y - 1);
        firePositions[7] = new Vector3(position.x - 1, position.y);
        return firePositions;
    }

    private Fire getFireByCoordinates(Vector3 position, List<Fire> FirePlaces)
    {
        foreach (Fire firePlace in FirePlaces)
        {
            Vector3 currentPos = firePlace.transform.position;
            if (Math.Abs(Math.Round(currentPos.x) - Math.Round(position.x)) < eps &&
                Math.Abs(Math.Round(currentPos.y) - Math.Round(position.y)) < eps)
            {
                return firePlace;
            }
        }
        return null;
    }

    private bool maySetFire(Vector3 pos)
    {
        List<Vector3> floor = GameManager.instance.FloorTiles;
        return floor.Contains(pos);
    }

    public void UpdateWorld()
    {
        currentStep++;
        if (currentStep % everySteps == 0)
        {
            currentStep = 0;
            List<Fire> newFires = new List<Fire>();
            int count = 0;
            for (var i = 0; i < FirePlaces.Count; i++)
            {
                Fire firePlace = FirePlaces[i];
                if (firePlace == null)
                {
                    FirePlaces.RemoveAt(i);
                    i--;
                    continue;
                }
                count++;
                Vector3 position = firePlace.transform.position;
                Fire fire = firePlace as Fire;
                if (fire.getFireLevel() >= FireLevelForSpread)
                {
                    if (Random.value <= FireLevelSpreadProb)
                    {
                        Vector3[] firePositions = getNeighbours(position);
                        foreach (Vector3 firePos in firePositions)
                        {
                            if (!IsFireExists(firePos, FirePlaces) && !IsFireExists(firePos, newFires) &&
                                maySetFire(firePos))
                            {
                                if (Random.value <= FireLevelSpreadProb)
                                {
                                    GameObject newFire = Instantiate(FireObj, firePos, Quaternion.identity);
                                    newFires.Add(newFire.GetComponent("Fire") as Fire);
                                }
                            }
                            else
                            {
                                if (Random.value <= FireLevelUpProb && IsFireExists(firePos, FirePlaces))
                                {
                                    Fire fire1 = getFireByCoordinates(firePos, FirePlaces) as Fire;
                                    fire1.levelUp();
                                }
                            }
                        }
                    }
                }
                else if (Random.value <= FireLevelUpProb)
                {
                    firePlace.levelUp();
                }
            }
            FirePlaces.AddRange(newFires);
            if (count == 0)
            {
                Init(this.level);
            }
            }
    }

    public void Init(int level)
    {
        FirePlaces.Clear();
        this.level = level;
        for (int i = 0; i <(int) level  * 1.5 + 6; i++)
        {
            int index = Random.Range(0, GameManager.instance.FloorTiles.Count);
            Vector3 firePos = GameManager.instance.FloorTiles[index];
            GameManager.instance.FloorTiles.RemoveAt(index);
            GameObject newFire = Instantiate(FireObj, firePos, Quaternion.identity);
            Debug.Log(newFire);
            FirePlaces.Add(newFire.GetComponent("Fire") as Fire);
        }
    }
}