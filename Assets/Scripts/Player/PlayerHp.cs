public class PlayerHp :  HpSystem
{
    private void Start()
    {
        UpdateUI();
    }

    protected override void OnHit()
    {
        UpdateUI();
    }

    protected override void OnDeath()
    {
        gameObject.SetActive(false);
        
        GameEnder.Instance.RestartGame();
    }

    protected override void OnHeal()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        UIManager.Instance.UpdateUIHp(currentHp, maxHp);
    }
}
