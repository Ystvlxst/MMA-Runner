
using System.Collections;
using UnityEngine;

public class JumpingRope : MonoBehaviour
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
        yield return new WaitForSeconds(1.5f);
        InHamd(false);
    }

    public void PlayerPickUp()
    {
        InHamd(true);
        StartCoroutine(PickUp());
    }
}