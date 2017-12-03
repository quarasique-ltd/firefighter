using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.1f;
    protected float stepLength = 0.1f;
    public LayerMask blockingLayer;
    
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;
    
    protected virtual void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = (10 * stepLength) / moveTime;
    }
    
    protected bool Move (float xDir, float yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 movement = new Vector2(xDir, yDir).normalized * stepLength;
        Vector2 end = start + movement;
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;
        if(hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }
    
    protected IEnumerator SmoothMovement(Vector3 end)
    {
        Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
        rb2D.MovePosition(newPostion);
        yield return null;
    }
    
    protected virtual void AttemptMove<T>(float xDir, float yDir) where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);
        
        if(hit.transform == null)
            return;
        
        T hitComponent = hit.transform.GetComponent<T>();
        if(!canMove && hitComponent != null)
            OnCantMove(hitComponent);
    }
    
    protected abstract void OnCantMove<T>(T component) where T : Component;
}