using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Lukas.MyClass;
using UnityEngine.Assertions;

public class UIManager :  Singleton<UIManager>
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TextMeshProUGUI hpText;

    protected override void Awake()
    {
        Assert.IsNotNull(hpSlider, "The hpSlider is null");
        Assert.IsNotNull(hpText, "The hpText is null");
        
        base.Awake();
    }

    public void UpdateUIHp(float currentHp, float maxHp)
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
