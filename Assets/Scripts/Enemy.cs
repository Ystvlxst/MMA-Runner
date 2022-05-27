using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _effects;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private float _kickForce = 10;

    private string _isAttack = "isAttack";
    private string _isFall = "isFall";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private IEnumerator TryAttack()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

        _animator.SetBool(_isAttack, true);
        yield return waitForSeconds;
        _animator.SetBool(_isFall, true);
        _effects[0].Play();
        _rigidbody.AddForce(Vector3.forward * _kickForce, ForceMode.Impulse);
        yield return new WaitForSeconds(1.3f);
        _effects[1].Play();
        yield return waitForSeconds;
        Destroy(gameObject);
    }

    public void PlayerAttack()
    {
        StartCoroutine(TryAttack());
    }
}
