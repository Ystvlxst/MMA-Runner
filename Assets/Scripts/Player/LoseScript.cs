using UnityEngine;

public class LoseScript : MonoBehaviour
{
    [SerializeField] private FightUI _fightUI;
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private FightingCamera _fightCamera;
    [SerializeField] private LoseCamera _loseCamera;
    [SerializeField] private FightingControl _playerFightingControl;

    private bool _isLose;

    private void Start()
    {
        _isLose = false;
    }

    public void PlayerLose(bool isLose)
    {
        _isLose = isLose;

        if (isLose == true)
        {
            gameObject.SetActive(true);
            _playerFightingControl.TryStartFinalFight(false);
            _fightUI.OnOffUI(false);
            _fightCamera.OnOffCamera(false);
            _playerCamera.OnOffCamera(false);
            _loseCamera.OnOffCamera(true);
        }
    }
}
