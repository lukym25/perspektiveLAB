using UnityEngine;
using System.Collections.Generic;
using Lukas.MyClass;
using TMPro;
using UnityEngine.Assertions;

public class KillCounter : Singleton<KillCounter>
{
    [SerializeField] private TextMeshProUGUI killCounterUIElement;
    
    private Dictionary<string, int> killStatistics;

    private void Awake()
    {
        Assert.IsNotNull(killCounterUIElement, "The killCounterUIElement is null");
        
        killStatistics = new Dictionary<string, int>();
    }

    public void EnemyDied(string name)
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
        var newTextToShow = "";
        foreach (var statName in killStatistics.Keys)
        {
            var textToAdd = statName + ":" + killStatistics[statName] + "\n";
            newTextToShow += textToAdd;
        }

        killCounterUIElement.text = newTextToShow;
    }

    private void AddNewEnemyKilled(string name)
    {
        killStatistics.Add(name, 1);
        
        UpdateCounter();
    }

    public void Reset()
    {
        killStatistics = new Dictionary<string, int>();

        UpdateCounter();
    }


    /*
     in my case I work with events, so I think this type of check is unnecessary
     values update automatically after unit dies
     For this reason, I put code only in comments

     private void Start()
     {
        StartCoroutine(UpdateValues());
     }


    private IEnumerator UpdateValues()
    {
        yield return new WaitForSeconds(3f);

        int i = 0;
        foreach (var numberOfKills in killStatistics.Values)
        {
            //get string of number from UI
            var textUIEnement = textsForValue[i].text;
            string[] splitArray =  textUIEnement.Split(char.Parse(":"));
            var numberInString = splitArray[1];

            //try to parse it to number
            int numberFromUI;
            int.TryParse(numberInString, out numberFromUI);

            if (numberOfKills != numberFromUI)
            {
                UpdateCounter();
            }

            i++;
        }

        StartCoroutine(UpdateValues());
    }
    */
}
