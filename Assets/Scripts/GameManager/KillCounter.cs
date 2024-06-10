using UnityEngine;
using System.Collections.Generic;
using Lukas.MyClass;
using TMPro;

public class KillCounter : Singelton<KillCounter>
{
    [SerializeField]
    private Transform killCounterUIElement;
    [SerializeField]
    private GameObject killCounterRowPrefab;
    
    private Dictionary<string, int> killStatistics;
    private List<TextMeshProUGUI> textsForValue;

    private void Awake()
    {
        killStatistics = new Dictionary<string, int>();
        textsForValue = new List<TextMeshProUGUI>();
    }

    public void Died(string name)
    {
        foreach (var nameInKillStatistics in killStatistics.Keys)
        {
            if (name == nameInKillStatistics)
            {
                killStatistics[nameInKillStatistics]++;
                UpdateCounter();
                
                return;
            }
        }

        AddNewEnemyKilled(name);
    }

    private void UpdateCounter()
    {
        int i = 0;
        foreach (var statName in killStatistics.Keys)
        {
            var textToShow = statName + ":" + killStatistics[statName];
            textsForValue[i].text = textToShow;
            i++;
        }
    }

    private void AddNewEnemyKilled(string name)
    {
        killStatistics.Add(name, 1);

        var newRow = Instantiate(killCounterRowPrefab, killCounterUIElement);
        
        var newTMP = newRow.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if(newTMP == null) {return;}
        textsForValue.Add(newTMP);

        /*var rectTransform = killCounterUIElement.GetComponent<RectTransform>();
        if(rectTransform == null) {return;}
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y + 60);*/
        
        UpdateCounter();
    }

    public void Reset()
    {
        killStatistics = new Dictionary<string, int>();
        textsForValue = new List<TextMeshProUGUI>();
        
        for(int i = killCounterUIElement.childCount - 1; i >= 0; i--)
        {
            Destroy(killCounterUIElement.GetChild(i).gameObject);
        }
    }
}
