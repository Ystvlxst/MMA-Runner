using UnityEngine;
using UnityEngine.UI;
public class BoosterCountSlider : MonoBehaviour
{
    [SerializeField] private Slider _countBoosterSlider;
    [SerializeField] private Player _player;
    [SerializeField] private Animator _imageAnimator;

    private string _isAll = "isAll";

    private void Update()
    {
        CheckPlayerBoostersCount();
    }

    private void CheckPlayerBoostersCount()
    {
        _countBoosterSlider.value = Mathf.Lerp(_countBoosterSlider.value, _player.CurrentAmountBoosters, Time.deltaTime);

        if (_player.CurrentAmountBoosters == _player.RequireAmountBoosters)
            _imageAnimator.SetBool(_isAll, true);
    }

    public void FinalFight()
    {
        gameObject.SetActive(false);
    }
}