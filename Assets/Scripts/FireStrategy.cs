using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireStrategy : MonoBehaviour, IWorldUpdater, IWorldGenerator
{
	public int FireLevelForSpread;
	public float FireLevelSpreadProb;
	
	private List<Fire> FirePlaces = new List<Fire>();
	public GameObject FireObj; 

	private double eps = 0.1;
	private bool IsFireExists(Vector3 fire, List<Fire> FirePlaces)
	{
		foreach (Fire firePlace in FirePlaces)
		{
			Vector3 currentPos = firePlace.transform.position;
			if (Math.Abs(Math.Round(currentPos.x) - Math.Round(fire.x)) < eps && Math.Abs(Math.Round(currentPos.y) - Math.Round(fire.y)) < eps)
			{
				return true;
			}
		}
		return false;
	}
	
	public Vector3[] getNeighbours(Vector3 position)
	{
		Vector3[] firePositions = new Vector3[8];
		firePositions[0] = new Vector3(position.x, position.y+1);
		firePositions[1] = new Vector3(position.x, position.y-1);
		firePositions[2] = new Vector3(position.x+1, position.y+1);
		firePositions[3] = new Vector3(position.x+1, position.y-1);
		firePositions[4] = new Vector3(position.x+1, position.y);
		firePositions[5] = new Vector3(position.x-1, position.y+1);
		firePositions[6] = new Vector3(position.x-1, position.y-1);
		firePositions[7] = new Vector3(position.x-1, position.y);
		return firePositions;
	}

	private Fire getFireByCoordinates(Vector3 position, List<Fire> FirePlaces)
	{
		foreach (Fire firePlace in FirePlaces)
		{
			Vector3 currentPos = firePlace.transform.position;
			if (Math.Abs(Math.Round(currentPos.x) - Math.Round(position.x)) < eps && Math.Abs(Math.Round(currentPos.y) - Math.Round(position.y)) < eps)
			{
				return firePlace;
			}
		}
		return null;
	}
	
	public  void UpdateWorld()
	{
		List<Fire> newFires = new List<Fire>();
		foreach (Fire firePlace in FirePlaces)
		{
			Vector3 position = firePlace.transform.position;
			Fire fire = firePlace as Fire;
			if (fire.getFireLevel() >= FireLevelForSpread)
			{
				Vector3[] firePositions = getNeighbours(position);
				foreach (Vector3 firePos in firePositions)
				{
					if (!IsFireExists(firePos, FirePlaces) && !IsFireExists(firePos, newFires))
					{
						if (Random.value <= FireLevelSpreadProb)
						{
							GameObject newFire = Instantiate(FireObj, firePos, Quaternion.identity);
							newFires.Add(newFire.GetComponent("Fire") as Fire);
						}
					}
					else
					{
						if (Random.value <= FireLevelSpreadProb && IsFireExists(firePos, FirePlaces))
						{
							Fire fire1 = getFireByCoordinates(firePos, FirePlaces) as Fire;
							fire1.levelUp();
						}
					}
				}
			} else if (Random.value <= FireLevelSpreadProb)
			{
				firePlace.levelUp();
			}
		}
		FirePlaces.AddRange(newFires);
	}

	public void Init()
	{
		Vector3 firePos = new Vector3(0, 0, 0);
		GameObject newFire = Instantiate(FireObj, firePos, Quaternion.identity);
		FirePlaces.Add(newFire.GetComponent("Fire") as Fire);
	}
}
