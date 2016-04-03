using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MovingObject
{
    private Animator animator;
    private int energy;
    private int leftTime;
    private Text levelText;
    private Text leftTimeText;

	protected override void Start ()
    {
        animator = this.GetComponent<Animator>();
        leftTime = (int) GameManager.instance.leftTime;

        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "LEVEL  " + GameManager.instance.level;

        leftTimeText = GameObject.Find("LeftTimeText").GetComponent<Text>();
        leftTimeText.text = "TIME " + leftTime;

        base.Start();
	}
	
	private void Update()
    {
        if(GameManager.instance.canControlPlayer)
            ControlPlayer();

        leftTime = (int) GameManager.instance.leftTime;
        leftTimeText.text = "TIME " + leftTime;
    }

    private void ControlPlayer()
    {
        if (isMoving) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (horizontal != 0)
            vertical = 0;
        if (vertical != 0)
            horizontal = 0;

        if (horizontal != 0 || vertical != 0)
        {
            TryToMove<Cell>(horizontal, vertical);
        }
    }

    protected override void TryToMove<T>(float xDir, float yDir)
    {
        isMoving = true;
        base.TryToMove<T>(xDir, yDir);
    }


    protected override void DoWhenBlocked <T> (T block, float xDir, float yDir)
    {
        if (block is Wall)
        {
            //Debug.Log("Wall!");
        }
        else if (block is MovableBlock)
        {
            MovableBlock moveBlock = block as MovableBlock;
            //Debug.Log("Movable!");
            moveBlock.MovedByPlayer(xDir, yDir);
        }
        isMoving = false;
    }
}
