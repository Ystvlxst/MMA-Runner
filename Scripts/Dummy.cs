using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Dummy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitEffect;

    private Animator _animator;

    private string _isHit = "isHit";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private IEnumerator DestroyDummy()
    {
        yield return new WaitForSeconds(0.15f);
        _animator.SetBool(_isHit, true);
        _hitEffect.Play();
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }

    public void PlayerHitting()
    {
        StartCoroutine(DestroyDummy());
    }
}
