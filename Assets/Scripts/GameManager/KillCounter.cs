using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using Lukas.MyClass;
using TMPro;
using UnityEngine.Assertions;

public class KillCounter : Singleton<KillCounter>
{
    [SerializeField] private TextMeshProUGUI killCounterUIElement;

    private Dictionary<string, int> killStatistics;
    private Dictionary<string, int> killStatisticsInUI;

    protected override void Awake()
    {
        Assert.IsNotNull(killCounterUIElement, "The killCounterUIElement is null");
        
        base.Awake();
        
        killStatistics = new Dictionary<string, int>();
        killStatisticsInUI = new Dictionary<string, int>(); 
    }

    private void Start()
    {
        StartCoroutine(UpdateValues());
    }

    public void EnemyDied(string name)
    {
        if (killStatistics.ContainsKey(name))
        {
            killStatistics[name]++;
        }
        else
        {
            killStatistics.Add(name, 1);
        }
    }

    private void UpdateCounter()
    {
        StringBuilder builder = new StringBuilder();
        foreach (var statName in killStatistics.Keys)
        {
            var textToAdd = String.Concat(statName, ":", killStatistics[statName], "\n");
            builder.Append(textToAdd);
        }

        killCounterUIElement.text = builder.ToString();
    }

    public void Reset()
    {
        killStatistics = new Dictionary<string, int>();
    }

    private IEnumerator UpdateValues()
    {
        yield return new WaitForSeconds(3f);

        foreach (var name in killStatistics.Keys)
        {
            if (killStatisticsInUI.ContainsKey(name))
            {
                if (killStatistics[name] != killStatisticsInUI[name])
                {
                    killStatisticsInUI[name] = killStatistics[name]; 
                    UpdateCounter();
                }
            }
            else
            {
                killStatisticsInUI.Add(name, killStatistics[name]);
                UpdateCounter();
            }
        }

        StartCoroutine(UpdateValues());
    }
}
