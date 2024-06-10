using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerHp :  HpSystem
{
    public Slider hpSlider;
    public TextMeshProUGUI hpText;
    
    private void Start()
    {
        UpdateUI();
    }

    protected override void Hit()
    {
        UpdateUI();
    }

    protected override void Died()
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
