using Unity.Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera mainCamera;

    [SerializeField]
    private CinemachineCamera pcCamera;

    [SerializeField]
    private CinemachineCamera enemyCamera;

    private void OnEnable()
    {
        PlayerInteraction.OnPCClicked += ZoomCameraToPC;
    }

    private void OnDisable()
    {
        PlayerInteraction.OnPCClicked -= ZoomCameraToPC;
    }


    void ZoomCameraToPC()
    {
        mainCamera.Priority = 0;
        pcCamera.Priority = 1;
    }

    void ZoomCameraBackToMain()
    {
        mainCamera.Priority = 1;
        pcCamera.Priority = 0;
    }

    public void ZoomToEnemy(Transform enemy)
    {
        enemyCamera.Follow = enemy;
        enemyCamera.Priority = 1;
        mainCamera.Priority = 0;
    }


}
