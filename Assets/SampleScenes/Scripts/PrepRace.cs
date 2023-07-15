using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static System.Int32;

public class PrepRace : MonoBehaviour
{
    // public string filePath;
    public RacerList Racers { get; set; }
    private bool verbose = false;

    private const int lineFieldMinimum = 5;

    private void Start()
    {
        ReadFileAtLocation();
    }

    private void ReadFileAtLocation()
    {
        //Get the raw csv data
        var rawData = File.ReadAllText("Assets/Resources/drivers.csv");
        
        //Divide data by each new line \n
        var lines = rawData.Split("\n"[0]);
        
        //Temp list
        List<RacerData> tmpList = new List<RacerData>();
        
        //For each line create a new RacerData to hold properties and add to List _racers
        foreach (var line in lines)
        {
            var fields = line.Split(","[0]);
            
            //Data validation
            if (fields.Length != lineFieldMinimum)
            {
                bool nameIsNull = fields[0].Length == 0;
                if (!nameIsNull) Debug.LogWarning($"{fields[0]} will not be joining us today on account of data corruption.");
                continue;
            }
            
            //object instantiation
            var newRacer = new RacerData
            {
                name = fields[0],
                acceleration = Parse(fields[1]),
                color = new Color(Parse(fields[2]), Parse(fields[3]), Parse(fields[4]))
            };

            tmpList.Add(newRacer);
        }

        Racers = new RacerList(tmpList);
        if(Racers.Length > 0) Debug.Log($"<color=green>{Racers.Length} are ready!</color>");
        else Debug.Log($"<color=red>0 racers are ready!</color>");
    }
}

[System.Serializable]
public class RacerList
{
    public RacerList(List<RacerData> list)
    {
        _RacerDatas = list.ToArray();
    }
    public RacerData[] _RacerDatas;

    public int Length => _RacerDatas.Length;
}

public class RacerData
{
    public string name;
    public int acceleration;
    public Color color;
}