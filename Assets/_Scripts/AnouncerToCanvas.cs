using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnouncerToCanvas : MonoBehaviour
{
    //Get Columns
    [SerializeField] public CheckpointList checkpointList;
    private List<float> m_Zpoints = new List<float>();
    //Get ActiveCar
    [SerializeField] private StartRace m_StartRace;
    private GameObject m_ActiveCar;
    //Get Canvas
    [SerializeField] private Text m_DriverMessageText;

    private void Start()
    {
        if (checkpointList == null) Debug.LogError("The AnnouncerToCanvas.checkPointList property is uninitialized.");
        
        AddAndSortPoints();
        if (m_StartRace == null) gameObject.TryGetComponent(out m_StartRace);
    }

    public void AddAndSortPoints()
    {
        for (int i = 0; i < checkpointList.Checkpoints.Length; i++)
        {
            m_Zpoints.Add(checkpointList.Checkpoints[i].transform.position.z);
        }
        
        m_Zpoints.Sort();
    }

    //Compare positions
    private void FixedUpdate()
    {
        if (m_StartRace.CurrentRacerGameObject)
        {
            //Check if cars changed
            var deltaCar = m_StartRace.CurrentRacerGameObject;
            if (deltaCar != m_ActiveCar)
            {
                m_ActiveCar = m_StartRace.CurrentRacerGameObject;
                AddAndSortPoints();//Resets m_Zpoints;
                m_DriverMessageText.text = $"{m_StartRace.CurrentRacer.name} has started!";
            }

            if (m_Zpoints[0] <= m_ActiveCar.transform.position.z)
            {
                if (m_Zpoints.Count == 1)
                {
                    //Passed Finish
                    m_DriverMessageText.text = $"{m_StartRace.CurrentRacer.name} passed finish";
                    // Debug.LogError($"{m_StartRace.CurrentRacer.name} passed finish");
                    return;
                }

                //Announce to Canvas
                m_DriverMessageText.text =
                    $"{m_StartRace.CurrentRacer.name} passed checkpoint {(m_StartRace.StartPoint.position.z - m_ActiveCar.transform.position.z).ToString()}, {(m_StartRace.CarController.CurrentSpeed).ToString()}";
                //POP out m_Zpoints[0]
                m_Zpoints.RemoveAt(0);
            }
        }
    }
}
[System.Serializable]
public class CheckpointList
{
    public GameObject[] Checkpoints;
}
