using UnityEngine;
using UnityStandardAssets.Cameras;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PrepRace))]
public class StartRace : MonoBehaviour
{
    [SerializeField] private GameObject carAIGO;
    [SerializeField] private Transform startPoint;
    [SerializeField] private FreeLookCam m_FreeLookCam;
    [SerializeField] private RacerData currentRacerData;
    [SerializeField] private DriverStats m_DriverStats;
    [SerializeField] private GameObject currentRacerGO;
    

    private void LateUpdate()
    {
        //Get on Input.SpaceBar
        if (Input.GetKeyUp(KeyCode.Space)) StartEngine();
    }

    public void StartEngine()
    {
        currentRacerData = GetRacer();
        BuildCar(currentRacerData);
        m_FreeLookCam.SetTarget(currentRacerGO.transform);
        m_DriverStats.RunCar();
    }

    //Instantiate object
    private void BuildCar(RacerData racer)
    {
        if(currentRacerGO) DestroyImmediate(currentRacerGO);
        GameObject car = Instantiate(carAIGO, startPoint.position, startPoint.rotation);
        car.TryGetComponent<DriverStats>(out m_DriverStats);
        m_DriverStats.DriverStatsSetup(racer);
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
