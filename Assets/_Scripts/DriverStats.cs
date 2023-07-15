using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class DriverStats : MonoBehaviour
{
    [SerializeField] private GameObject carBody;
    private CarController m_CarController;
    private RacerData m_RacerData;
    private bool m_isCarBodyNull;
    private bool m_isCarControllerNull;

    private void Start()
    {
        m_isCarBodyNull = carBody == null;
        m_isCarControllerNull = m_CarController == null;
    }

    public void DriverStatsSetup(RacerData racerData)
    {
        m_RacerData = racerData;
        transform.name += $"({racerData.name})";
        SetColor(racerData.color);
    }

    public void RunCar()
    {
        gameObject.TryGetComponent(out m_CarController);
        if (!m_CarController) return;
        m_CarController.Initialize();
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
