using UnityEngine;

public class ControllInterface : MonoBehaviour
{
    private Player player;
    private float moveDirection;
    private bool needMove;

    public Player Player {
        set
        {
            player = value;
            
            if (player.useKeyBoard)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (needMove)
        {
            player.Move(moveDirection);
        }
    }

    public void Jump()
    {
        player.Jump();
    }

    public void Use()
    {
        player.InteractWithObj();
    }

    public void MoveLeft()
    {
        needMove = true;
        moveDirection = -1f;
    }

    public void MoveRight()
    {
        needMove = true;
        moveDirection = 1f;
    }

    public void StopMove()
    {
        moveDirection = 0;
        player.StopMoving();

        needMove = false;
    }
}
