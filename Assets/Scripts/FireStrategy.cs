using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGenerator : WorldUpdater
{
		
	public override void UpdateWorld()
	{
		GameObject FirePlaces [];
		FirePlaces = GameObject.FindGameObjectWithTag("firePlaces");
	}
}
