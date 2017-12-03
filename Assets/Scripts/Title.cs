using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Title : MovingObject
{
    private Vector3 finalPosition = new Vector3(0, 0, 0);
    
    protected override void Start ()
    {
        moveTime = 10;
        base.Start ();
    }
    
    protected override void AttemptMove <T> (float xDir, float yDir)
    {
        base.AttemptMove<T>(xDir, yDir);
    }

    private void Update()
    {
//        if (stepLength > Math.Abs(gameObject.transform.position.x - finalPosition.x)) {
//            StartCoroutine(SmoothMovement(gameObject.transform.position.x));
//        }
    }
    
    protected override void OnCantMove<T>(T component)
    {
        // TODO: prevent 'going into the wall'
    }
}