using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private BoardManager boardScript;
	private int level = 1;
	public List<Vector3> FloorTiles = new List<Vector3>();
	private bool gameOver = false;
	private float restartTimer = 0;
	private float restartDelay = 0;
	public int points = 1;
	
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
		restartTimer += Time.deltaTime;
		if (gameOver)
		{
			SceneManager.UnloadScene(0);
			SceneManager.LoadScene(0);
			gameOver = false;
		}
		boardScript.Update();
	}

	public void win()
	{
		GameOver();
	}

	public void GameOver()
	{
		if (!gameOver)
		{
			gameOver = true;
			restartDelay += restartTimer +1f;
		}
	}
}
