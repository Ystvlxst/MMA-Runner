using UnityEngine;

public class FightUI : MonoBehaviour
{
    private bool _isFinalFignt;

    private void Start()
    {
        _isFinalFignt = false;
    }

    public void OnOffUI(bool isFinalFight)
    {
        _isFinalFignt = isFinalFight;

        if (isFinalFight == true)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
