using UnityEngine;

public class FinalFight : MonoBehaviour
{
    [SerializeField] private FightUI _buttons;
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private FightingCamera _fightCamera;
    [SerializeField] private FightingControl _fightingControl;
    [SerializeField] private BoosterCountSlider _slider;

    public void TryStartFight()
    {
        _buttons.OnOffUI(true);
        _playerCamera.OnOffCamera(false);
        _fightCamera.OnOffCamera(true);
        _fightingControl.TryStartFinalFight(true);
        _slider.FinalFight();
    }
}
