using UnityEngine;

public class LoseInWater : MonoBehaviour
{
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private WaterCamera _waterCamera;
    [SerializeField] private GameObject _losePanel;

    public void PlayerInWater()
    {
        _playerCamera.OnOffCamera(false);
        _waterCamera.OnOffCamera(true);
        _losePanel.gameObject.SetActive(true);
    }
}
