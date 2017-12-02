using UnityEngine;
using System.Collections;

public class NPC : MovingObject
{
    private Animator animator;
    private Transform target;
    private bool skipMove;
    private float distance;
    
    protected override void Start ()
    {
        //GameManager.instance.AddNPCToList(this);
        animator = GetComponent<Animator>();
        base.Start ();
    }
    
    protected override void AttemptMove <T> (float xDir, float yDir)
    {
        if(skipMove)
        {
            skipMove = false;
            return;
        }
        
        base.AttemptMove<T>(xDir, yDir);
        skipMove = true;
    }
    
    public void MoveNPC ()
    {
        if (null != target)
        {
            float xDir = 0;
            float yDir = 0;

            if (target.position.x - transform.position.x < distance)
                yDir = target.position.y > transform.position.y ? stepLength : -stepLength;
            else
                xDir = target.position.x > transform.position.x ? stepLength : -stepLength;
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
    }
    
    protected override void OnCantMove<T>(T component)
    {
        // TODO: prevent 'going into the wall'
    }
}