using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class StaminaSlider : MonoBehaviour
{
    [SerializeField] private FightingControl _button;

    private Slider _staminaSlider;
    private float _energyAttack;
    private int _maxValue = 10;

    public Slider Slider => _staminaSlider;

    private void Start()
    {
        _staminaSlider = GetComponent<Slider>();

        _staminaSlider.value = _maxValue;
        _energyAttack = 3.3f;
    }

    private void Filling()
    {
        _staminaSlider.value = Mathf.Lerp(_staminaSlider.value, _staminaSlider.value += _energyAttack, 1);
    }

    private IEnumerator FillingSlider()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);

        while (_staminaSlider.value != _maxValue)
        {
            yield return waitForSeconds;

            if (_staminaSlider.value != _maxValue)
                Filling();
            else
                break;

            yield return waitForSeconds;

            if (_staminaSlider.value != _maxValue)
                Filling();
            else
                break;

            yield return waitForSeconds;

            if (_staminaSlider.value != _maxValue)
                Filling();
            else
                break;

            _button.ActiveButton(false);

            break;
        }
    }

    public void Recreation()
    {
        StartCoroutine(FillingSlider());
    }

    public void Fatigue()
    {
        _staminaSlider.value = Mathf.Lerp(_staminaSlider.value, _staminaSlider.value -= _energyAttack, 1);
    }
}
