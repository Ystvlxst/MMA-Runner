using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class PlayerMuscle : MonoBehaviour
{
    [SerializeField] private float _massFactor;

    private Transform _scale;

    private void Start()
    {
        _scale = GetComponent<Transform>();
    }

    public void GetMass()
    {
        _scale.localScale = Vector3.Lerp(_scale.localScale, new Vector3(_massFactor, _massFactor, _massFactor), Time.deltaTime);
    }
}
