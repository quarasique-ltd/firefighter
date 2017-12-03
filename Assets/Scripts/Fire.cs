using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
	private GameObject currentFire;
	private Vector3 pos;
	private Animator animator;
	private int fireLevel = 0;
	public int maxFireLevel = 2;
	
	public Fire(Vector3 pos)
	{
	}

	public void levelUp()
	{
		if (fireLevel < maxFireLevel)
		{
			fireLevel++;
			animator.SetInteger("fireLevel", fireLevel);
		}
	}

	public int getFireLevel()
	{
		return fireLevel + 1;
	}
	
	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator>();
		pos = gameObject.transform.position;
	}
}
