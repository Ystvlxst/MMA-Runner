using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class BossHealth : MonoBehaviour
{
    [SerializeField] private Slider _health;
    [SerializeField] private Boss _boss;

    private Animator _animator;
    private string _isHit = "isHit";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckHealthValue();
    }

    private void CheckHealthValue()
    {
        _health.value = _boss.Health;
    }

    private IEnumerator Hitting()
    {
        _animator.SetBool(_isHit, true);
        yield return new WaitForSeconds(0.2f);
        _animator.SetBool(_isHit, false);
    }

    public void Hit()
    {
        StartCoroutine(Hitting());
    }
}
