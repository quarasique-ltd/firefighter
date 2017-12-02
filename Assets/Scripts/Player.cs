﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{
    public int savingPoints = 10;
    public int healthPoints = 3;
    
    private Animator animator;
    private int points;
     
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        base.Start ();
    }
    
    private void OnDisable ()
    {
        //TODO: count score
    }
    
    
    private void Update ()
    {
        float horizontal = 0;
        float vertical = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = -stepLength;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = stepLength;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            vertical = stepLength;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            vertical = -stepLength;
        }
        AttemptMove<Fire>(horizontal, vertical);
    }
    
    protected override void AttemptMove <T> (float xDir, float yDir)
    {
        base.AttemptMove <T> (xDir, yDir);
        RaycastHit2D hit;
        if (Move (xDir, yDir, out hit)) 
        {
            //Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
        }
    }
    
    protected override void OnCantMove<T>(T component)
    {
        // TODO: prevent 'going into the wall'
    }
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Fire")
        {
            healthPoints--;
            CheckIfGameOver();
        }
        else if(other.tag == "NPC")
        {
            points += savingPoints;
        }
    }
    
    private void Restart ()
    {
        SceneManager.LoadScene (0);
    }
    
    public void LoseFood (int loss)
    {
        // TODO: play burning animation 
        healthPoints--;
        CheckIfGameOver();
    }
    
    private void CheckIfGameOver ()
    {
        if (healthPoints <= 0) 
        {
            //GameManager.instance.GameOver();
        }
    }
}