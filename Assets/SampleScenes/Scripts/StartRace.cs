using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PrepRace))]
public class StartRace : MonoBehaviour
{
    [SerializeField] private GameObject carAIGO;
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject currentRacerGO;
    private RacerData currentRacerData;

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        //Get on Input.SpaceBar
        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentRacerData = GetRacer();
            BuildCar(currentRacerData);
        }
    }

    //Instantiate object
    private void BuildCar(RacerData racer)
    {
        if(currentRacerGO) DestroyImmediate(currentRacerGO);
        GameObject car = Instantiate(carAIGO, startPoint.position, startPoint.rotation);
        // DriverStats stats = 
            car.TryGetComponent<DriverStats>(out var driverStats);
        driverStats.DriverStatsSetup(racer);
        currentRacerGO = car;
    }

    //Selects one racer at random
    private RacerData GetRacer()
    {
        TryGetComponent<PrepRace>(out var prepRace);
        if (prepRace.Racers.Length <= 0) return null;
        
        var randomInteger = Random.Range(0, prepRace.Racers.Length);
        return prepRace.Racers._RacerDatas[randomInteger];
    }
}
