using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

	private int FireLevel = 0;

	public GameObject[] FireLevels;
	private GameObject currentFire;
	private Vector3 pos;
	
	public Fire(Vector3 pos)
	{
		
	}

	public void levelUp()
	{
		if (FireLevels.Length - FireLevel >= 2)
		{
			FireLevel++;
			Destroy(currentFire);
			currentFire =(GameObject) Instantiate(FireLevels[FireLevel], pos, Quaternion.identity);
		}
	}

	public int getFireLevel()
	{
		return FireLevel + 1;
	}
	
	// Use this for initialization
	void Start ()
	{
		currentFire = (GameObject) Instantiate(FireLevels[FireLevel], pos, Quaternion.identity);
		this.pos = gameObject.transform.position;
		Debug.Log(pos.x + " " + pos.y);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
