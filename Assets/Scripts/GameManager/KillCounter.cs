using System.Collections;
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
