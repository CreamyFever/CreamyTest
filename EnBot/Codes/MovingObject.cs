using UnityEngine;
using System.Collections;

public abstract class MovingObject : Cell
{
    private Rigidbody2D rb2d;
    private Collider2D col2d;
    private const float mulFloat = 0.64f;

    protected bool isMoving;

    public float moveSpeed;

    protected virtual void Awake ()
    {
        base.Awake();
    }

    protected virtual void Start ()
    {
        moveSpeed = 5.0f;
        isMoving = false;
        rb2d = this.GetComponent<Rigidbody2D>();
        col2d = this.GetComponent<Collider2D>();
    }
	
    protected bool CheckToMove(float xDir, float yDir, out RaycastHit2D hit)
    {
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(xDir * mulFloat, yDir * mulFloat, 0);

        col2d.enabled = false;

        hit = Physics2D.Linecast(start, end, 1 << LayerMask.NameToLayer("Block"));

        col2d.enabled = true;

        if(hit.transform == null)
        {
            StartCoroutine (SmoothMovement(end));
            return true;
        }

        return false;
    }

    protected IEnumerator SmoothMovement (Vector3 end)
    {
        float leftDist = (transform.position - end).sqrMagnitude;

        while ( leftDist > Mathf.Epsilon )
        {
            Vector3 pos = Vector3.MoveTowards(rb2d.position, end, moveSpeed * Time.deltaTime);

            rb2d.MovePosition (pos);

            leftDist = (transform.position - end).sqrMagnitude;

            if (leftDist <= Mathf.Epsilon)
                isMoving = false;

            yield return null;
        }
    }

    protected virtual void TryToMove <T> (float xDir, float yDir) where T : Cell
    {
        RaycastHit2D hit;

        bool canMove = CheckToMove(xDir, yDir, out hit);

        if (hit.transform == null)
            return;

        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
        {
            DoWhenBlocked<T> (hitComponent, xDir, yDir);
        }
    }

    protected abstract void DoWhenBlocked <T> (T block, float xDir, float yDir) where T : Cell;
}
