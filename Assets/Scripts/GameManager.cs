using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private BoardManager boardScript;
	private int level = 3;
	public List<Vector3> FloorTiles = new List<Vector3>();
	
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		boardScript = GetComponent<BoardManager>();
		InitGame();
	}
	
	void InitGame () {
		boardScript.BoardSetup(level);
	}
	
	void Update () {
		boardScript.Update();
	}

	public void GameOver()
	{
		this.enabled = false;
	}
}
