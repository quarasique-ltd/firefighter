using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	public IWorldGenerator[] WorldGenerators;

	public IWorldUpdater []WorldUpdaters;
	
	// Use this for initialization
	public void BoardSetup (int level) {
		WorldGenerators = GetComponents<IWorldGenerator>();
		WorldUpdaters = GetComponents<IWorldUpdater>();
		foreach (IWorldGenerator worldGenerator in WorldGenerators)
		{
			worldGenerator.Init();
		}
	}
	
	// Update is called once per frame
	public void Update () {
		foreach (IWorldUpdater worldUpdater in WorldUpdaters)
		{
			worldUpdater.UpdateWorld();
		}	
	}
}
