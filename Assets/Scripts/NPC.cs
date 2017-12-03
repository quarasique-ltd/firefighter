using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : MovingObject
{
    private Animator animator;
    private Transform target;
    private Vector3 currentTarget;
    private bool skipMove;
    private float distance;
    public float maxDistance = 3;
    public float minDistance = 1;
    public double changeDistanceProb = 0.01;
    public double walkEbanca = 0.01;
    
    
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

            if (Random.value <= changeDistanceProb)
            {
                distance = Random.Range(minDistance, maxDistance);
            }
            if (Random.value <= changeDistanceProb)
            {
                currentTarget = target.position;
            }
            
            float xDir = 0;
            float yDir = 0;

            double currentDistance = Math.Sqrt(Math.Pow(currentTarget.x - transform.position.x, 2) +
                                               Math.Pow(currentTarget.y - transform.position.y, 2));
            
            if (currentDistance < distance)
            {
                currentTarget = target.position;
                currentTarget.x += Random.Range((float) -walkEbanca, (float) walkEbanca);
                currentTarget.y += Random.Range((float) -walkEbanca, (float) walkEbanca);
            }
            
            if (Math.Round(Math.Abs(currentTarget.x - transform.position.x)) > distance) {
                xDir = currentTarget.x > transform.position.x ? stepLength : -stepLength;
                if (currentTarget.x != transform.position.x)
                {
                    animator.SetBool("NPCLeft", currentTarget.x < transform.position.x);
                    animator.SetBool("NPCRun", true);
                }
                else
                {
                    animator.SetBool("NPCRun", false);
                }
            }
            if (Math.Round(Math.Abs(currentTarget.y - transform.position.y)) > distance)
            {
                yDir = currentTarget.y > transform.position.y ? stepLength : -stepLength;
            }
        
            double currentVectorLength = Math.Sqrt(Math.Pow(xDir, 2) + Math.Pow(yDir, 2));

            xDir /= (float) currentVectorLength * stepLength;
            yDir /= (float) currentVectorLength * stepLength ;
            
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
        this.enabled = false;
        this.animator.enabled = false;
        Destroy(this.gameObject);
        // TODO: method to play burning animation and delete NPC from screen;
    }

    private void FollowPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentTarget = target.position;
        distance = Random.Range(minDistance, maxDistance);
    }
    
    protected override void OnCantMove<T>(T component)
    {
        // TODO: prevent 'going into the wall'
    }
}