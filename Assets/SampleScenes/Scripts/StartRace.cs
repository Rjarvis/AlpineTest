using System;
using UnityEngine;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Vehicles.Car;
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
    [SerializeField] private CarController m_CarController;
    [SerializeField] private GameObject FinishLine;
    [SerializeField] private GameObject Fireworks;

    private bool fireWorksLit;
    private void LateUpdate()
    {
        //Get on Input.SpaceBar
        if (Input.GetKeyUp(KeyCode.Space)) StartEngine();
        if (m_CarController)
        {
            if (FinishedRace() && m_CarController.CurrentSpeed >= 10f)
            {
                if (!fireWorksLit) LightFireWorks();
                m_CarController.Move(0f, 0f, currentRacerData.acceleration*-10f, 0f);
            }
            else if (!FinishedRace()) m_CarController.Move(0, currentRacerData.acceleration, 0f, 0f);
        }
    }

    private void LightFireWorks()
    {
        fireWorksLit = true;
        Fireworks.TryGetComponent(out ParticleSystem particleSystem);
        if (particleSystem) particleSystem.Play();
    }

    private bool FinishedRace()
    {
        return currentRacerGO.transform.position.z >= FinishLine.transform.position.z;
    }

    public void StartEngine()
    {
        fireWorksLit = false;
        currentRacerData = GetRacer();
        BuildCar(currentRacerData);
        m_FreeLookCam.SetTarget(currentRacerGO.transform);
        RunCar();
    }
    
    public void RunCar()
    {
        m_DriverStats.TryGetComponent(out m_CarController);
        if (!m_CarController) return;
        m_CarController.Initialize();
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
