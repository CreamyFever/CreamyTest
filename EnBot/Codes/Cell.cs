using UnityEngine;
using System.Collections;

public abstract class Cell : MonoBehaviour
{
    private SpriteRenderer sprRen;

    protected virtual void Awake()
    {
        sprRen = this.GetComponent<SpriteRenderer>();
    }
}
