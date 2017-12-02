using UnityEngine;
using System.Collections;

public class NPC : MovingObject
{
    public int savingScore = 100;
    
    private Animator animator;
    private Transform target;
    private bool skipMove;
    
    protected override void Start ()
    {
        GameManager.instance.AddNPCToList(this);
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
        int xDir = 0;
        int yDir = 0;
        
        if(target.position.x - transform.position.x < float.Epsilon)
            yDir = target.position.y > transform.position.y ? 1 : -1;
        else
            xDir = target.position.x > transform.position.x ? 1 : -1;
            AttemptMove<Player>(xDir, yDir);
    }
    
    protected override void OnCantMove <T> (T component)
    {
        Player player = component as Player;
        player.AddPoints(savingScore);
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}