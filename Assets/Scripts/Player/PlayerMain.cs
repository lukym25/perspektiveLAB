using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [SerializeField] public PlayerAttack playerAttack;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerHp playerHp;

    public void Attack()
    {
        playerAttack.Attack();
    }

    public void Move()
    {
        playerMovement.MovePlayer();
    }

    public void Jump()
    {
        playerMovement.PlayerJump();
    }

    public void GameStarted()
    {
        playerMovement.CalculateMoveVectors();
    }
}
