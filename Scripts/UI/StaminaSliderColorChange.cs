using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StaminaSliderColorChange : MonoBehaviour
{
    [SerializeField] private StaminaSlider _slider;

    private Image _fillImage;
    private int _maxValue = 10;
    private int _middleValue = 7;
    private int _minValue = 4;

    private void Start()
    {
        _fillImage = GetComponent<Image>();
        _fillImage.color = Color.green;
    }

    private void Update()
    {
        CheckSliderValue();
    }

    private void CheckSliderValue()
    {
        if (_slider.Slider.value == _maxValue)
            _fillImage.color = Color.green;
        else if (_slider.Slider.value == _middleValue)
            _fillImage.color = Color.white;
        else if (_slider.Slider.value == _minValue)
            _fillImage.color = Color.red;
    }
}
