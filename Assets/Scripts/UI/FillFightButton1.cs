using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FillFightButton1 : MonoBehaviour
{
    [SerializeField] private float _lerpDuration;
    [SerializeField] private float _recoveryDuration;

    private Image _imageButton;


    private void Start()
    {
        _imageButton = GetComponent<Image>();

        _imageButton.fillAmount = 1;
    }

    private IEnumerator Filling(float startValue, float endValue, float duration, UnityAction<float> lerpingEnd)
    {
        float elapsed = 0;
        float nextValue;

        while (elapsed < duration)
        {
            nextValue = Mathf.Lerp(startValue, endValue, elapsed / duration);
            _imageButton.fillAmount = nextValue;
            elapsed += Time.deltaTime;
            yield return null;
        }
        lerpingEnd?.Invoke(endValue);
    }

    private void Fill(float value)
    {
        _imageButton.fillAmount = value;
    }

    private void ToFill(float startValue, float endValue, float duration)
    {
        StartCoroutine(Filling(startValue, endValue, duration, Fill));
    }
}
