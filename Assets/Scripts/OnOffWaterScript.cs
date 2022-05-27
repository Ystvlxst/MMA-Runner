using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffWaterScript : MonoBehaviour
{
    [SerializeField] private GameObject _water;

    private IEnumerator WaterControl()
    {
        _water.gameObject.SetActive(false);
        yield return new WaitForSeconds(15);
        _water.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            StartCoroutine(WaterControl());
        }
    }
}
