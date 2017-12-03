using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : MovingObject
{
    private Animator animator;
    private Transform target;
    private bool skipMove;
    private float distance;
    
    protected override void Start ()
    {
        animator = GetComponent<Animator>();
        base.Start ();
    }
    
    protected override void AttemptMove <T> (float xDir, float yDir)
    {
        base.AttemptMove<T>(xDir, yDir);
    }
    
    private void Update ()
    {
        if (null != target)
        {
            float xDir = 0;
            float yDir = 0;

            if (Math.Round(Math.Abs(target.position.x - transform.position.x)) > distance)
            {
                xDir = target.position.x > transform.position.x ? stepLength : -stepLength;
            }
            if (Math.Round(Math.Abs(target.position.y - transform.position.y)) > distance)
            {
                yDir = target.position.y > transform.position.y ? stepLength : -stepLength;
            }
            AttemptMove<Player>(xDir, yDir);
        }
    }
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Fire")
        {
            Burn();
        }
        else if(other.tag == "Player")
        {
            FollowPlayer();
        }
    }

    private void Burn()
    {
        // TODO: method to play burning animation and delete NPC from screen;
    }

    private void FollowPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        distance = Random.Range(1, 3);
    }
    
    protected override void OnCantMove<T>(T component)
    {
        // TODO: prevent 'going into the wall'
    }
}