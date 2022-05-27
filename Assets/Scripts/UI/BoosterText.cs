using UnityEngine;
using TMPro;

public class BoosterText : MonoBehaviour
{
    [SerializeField] private Transform _canvas;
    [SerializeField] private TMP_Text _plusBoost;
    [SerializeField] private TMP_Text _minusBoost;

    public void PlusBoost()
    {
        Instantiate(_plusBoost, _canvas);
        _plusBoost.gameObject.SetActive(true);
    }

    public void MinusBoost()
    {
        Instantiate(_minusBoost, _canvas);
        _minusBoost.gameObject.SetActive(true);
    }
}
