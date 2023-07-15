using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class DriverStats : MonoBehaviour
{
    [SerializeField] private GameObject carBody;
    private CarController m_CarController;
    private RacerData m_RacerData;
    private bool m_isCarBodyNull;

    private void Start()
    {
        m_isCarBodyNull = carBody == null;
    }

    public void DriverStatsSetup(RacerData racerData)
    {
        transform.name += $"({racerData.name})";
        SetColor(racerData.color);
        RunCar(racerData);
    }

    private void RunCar(RacerData racerData)
    {
        CarController carController = gameObject.GetComponent<CarController>();
        carController.Initialize();
        m_CarController = carController;
        m_RacerData = racerData;
    }

    private void SetColor(Color color)
    {
        if (m_isCarBodyNull) return;
        carBody.TryGetComponent<MeshRenderer>(out var meshRenderer);
        if (!meshRenderer) return;
        var currentMat = meshRenderer.material;
        currentMat.SetColor("_Color", color);
    }

    private void LateUpdate()
    {
        m_CarController.Move(0f,m_RacerData.acceleration,0,0f);
    }
}
