using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject
{
    public int savingPoints = 10;
    public int healthPoints = 400;

    public Text score;
    
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
        score.text = "" + GameManager.instance.points;
        float horizontal = 0;
        float vertical = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = -stepLength;
            animator.SetBool("playerLeft", true);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = stepLength;
            animator.SetBool("playerLeft", false);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            vertical = stepLength;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            vertical = -stepLength;
        }

        if (horizontal != 0f || vertical != 0f)
        {
            animator.SetBool("playerRun", true);
            AttemptMove<Fire>(horizontal, vertical);
        }
        else
        {
            animator.SetBool("playerRun", false);
        }
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
            Burn();
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

    private int fired = 0;
    public void Burn()
    {
        // TODO: play burning animation
        fired++;
        if (fired % 10 == 0)
        {
            healthPoints--;
        }
        CheckIfGameOver();
    }
    
    private void CheckIfGameOver()
    {
        if (healthPoints <= 0) 
        {
            GameManager.instance.GameOver();
        }
    }
}