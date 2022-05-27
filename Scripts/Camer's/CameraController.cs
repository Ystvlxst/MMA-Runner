using UnityEngine;

public abstract class CameraController : MonoBehaviour
{
    private bool _isOn;

    public void OnOffCamera(bool isOn)
    {
        _isOn = isOn;

        if (isOn == true)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
