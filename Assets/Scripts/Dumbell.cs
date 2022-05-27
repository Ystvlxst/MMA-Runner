using System.Collections;
using UnityEngine;

public class Dumbell : MonoBehaviour
{
    private void InHamd(bool isActive)
    {
        if (isActive == true)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

    private IEnumerator PickUp()
    {
        yield return new WaitForSeconds(2);
        InHamd(false);
    }

    public void PlayerPickUp()
    {
        InHamd(true);
        StartCoroutine(PickUp());
    }
}
