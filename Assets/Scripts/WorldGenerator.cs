using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorldGenerator {
	
	// Update is called once per frame
	void Init(int levelNumber);
}
