using UnityEngine.UI;
using TMPro;

public class PlayerHp :  HpSystem
{
    //doesnt need assert values because it can work without them
    public Slider hpSlider;
    public TextMeshProUGUI hpText;
    
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
        if (hpSlider != null)
        {
            hpSlider.value = currentHp / maxHp;
        }

        if (hpText != null)
        {
            var newText = currentHp + "/" + maxHp;
            hpText.text = newText;
        }
    } 
}
