using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

	private int FireLevel = 0;

	public Sprite[] FireLevels;
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
			this.GetComponent<SpriteRenderer>().sprite = FireLevels[FireLevel];
		}
	}

	public int getFireLevel()
	{
		return FireLevel + 1;
	}
	
	// Use this for initialization
	void Start ()
	{
		this.pos = gameObject.transform.position;
		this.GetComponent<SpriteRenderer>().sprite = FireLevels[FireLevel];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
