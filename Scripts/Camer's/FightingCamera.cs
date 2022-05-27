using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FightingCamera : CameraController
{
    private Animator _animator;
    private string _isKick = "isKick";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private IEnumerator Reaction()
    {
        _animator.SetBool(_isKick, true);
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool(_isKick, false);
    }

    public void CameraReaction()
    {
        StartCoroutine(Reaction());
    }
}
