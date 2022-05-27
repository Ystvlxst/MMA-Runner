using UnityEngine;

public class WinScript : MonoBehaviour
{
    [SerializeField] private FightUI _fightUI;
    [SerializeField] private ParticleSystem _winEffect;
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private WinCamera _winCamera;
    [SerializeField] private FightingCamera _fightCamera;
    [SerializeField] private Boss _boss;
    [SerializeField] private FightingControl _playerFightingControl;

    private bool _isWin;

    private void Start()
    {
        _isWin = false;
    }

    public void TryWin(bool isWin)
    {
        _isWin = isWin;

        if (isWin == true)
        {
            _playerFightingControl.TryStartFinalFight(true);
            gameObject.SetActive(true);
            _boss.StopAttack();
            _fightUI.OnOffUI(false);
            _winEffect.Play();
            _fightCamera.OnOffCamera(false);
            _playerCamera.OnOffCamera(false);
            _winCamera.OnOffCamera(true);
        }
    }
}
