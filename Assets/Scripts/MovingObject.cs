using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.1f;
    protected float stepLength = 0.1f;
    public List<LayerMask> blockingLayer = new List<LayerMask>();
    
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;
    
    protected virtual void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = (1000 * stepLength) / moveTime;
    }
    
    protected bool Move (float xDir, float yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 movement = new Vector2(xDir, yDir).normalized * stepLength;
        Vector2 end = start + movement;
        boxCollider.enabled = false;
        bool isMoveAvail = true; 
        foreach (LayerMask layerMask in blockingLayer)
        {
            hit = Physics2D.Linecast(start, end, layerMask);
            isMoveAvail &= hit.transform == null;
        }
        hit = Physics2D.Linecast(start, end, blockingLayer[0]);
        if (isMoveAvail)
        {
            boxCollider.enabled = true;
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        boxCollider.enabled = true;
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