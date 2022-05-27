using System.Collections;
using System.Collections.Generic;
using RunnerMovementSystem;
using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    [SerializeField] private MovementSystem _playerMovement;
    [SerializeField] private ParticleSystem[] _boostEffects;
    [SerializeField] private float _timeSpeedEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _playerMovement.Boosted();
            _boostEffects[0].Play();
            StartCoroutine(SpeedEffect());
        }
    }

    private IEnumerator SpeedEffect()
    {
        _boostEffects[1].Play();
        yield return new WaitForSeconds(_timeSpeedEffect);
        _boostEffects[1].Stop();
    }
}
