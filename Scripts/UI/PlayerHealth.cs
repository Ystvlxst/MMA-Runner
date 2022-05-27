using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider _health;
    [SerializeField] private PlayerFightController _player;

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
        _health.value = _player.Health;
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
