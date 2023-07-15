using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnouncerToCanvas : MonoBehaviour
{
    //Get ActiveCar
    [SerializeField] private StartRace m_StartRace;
    private GameObject m_ActiveCar;
    //Get Columns
    [SerializeField] public static CheckpointList checkpointList;
    private int[] m_Zpoints = new int[checkpointList.Checkpoints.Length];

    private void Start()
    {
        throw new NotImplementedException();
    }


    //Compare positions


}
[System.Serializable]
public class CheckpointList
{
    public GameObject[] Checkpoints;
}
