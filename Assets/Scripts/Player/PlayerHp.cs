using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerHp : MonoBehaviour, IHpSystem
{
    public float currentHp;
    public float maxHp;
    public Slider hpSlider;
    public TextMeshProUGUI hpText;
    
    private void Start()
    {
        currentHp = maxHp;

        UpdateUI();
    }

    public void Damage(float damageAmount)
    {
        currentHp -= damageAmount;

        UpdateUI();

        if (currentHp <= 0)
        {
            PlayerDied();
        }
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

    private void PlayerDied()
    {
        gameObject.SetActive(false);
        
        GameEnder.Instance.RestartGame();
    }
}
