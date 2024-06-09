using UnityEngine;

public class PlayerHp : MonoBehaviour, IHpSystem
{
    public float currentHp;
    public float maxHp;
    
    private void Awake()
    {
        currentHp = maxHp;
    }

    public void Damage(float damageAmount)
    {
        currentHp -= damageAmount;

        if (currentHp <= 0)
        {
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        //Destroy(gameObject);
    }
}
