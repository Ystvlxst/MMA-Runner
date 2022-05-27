using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StaminaCircleSlider : MonoBehaviour
{
    [SerializeField] private FightingControl _button;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();

        _image.fillAmount = 1;
    }

    private IEnumerator FillingSlider()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);

        yield return waitForSeconds;
        Filling();
        yield return waitForSeconds;
        Filling();
        yield return waitForSeconds;
        Filling();
        _button.ActiveButton(false);
    }

    private void Filling()
    {
        _image.fillAmount += 0.333f;
    }

    public void Fatigue()
    {
        _image.fillAmount -= 0.333f;
    }

    public void Recreation()
    {
        StartCoroutine(FillingSlider());
    }
}
