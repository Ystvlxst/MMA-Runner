using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BadFrame : MonoBehaviour
{
    [SerializeField] private ParticleSystem _onEnterTriggerEffect;

    private Animator _animator;
    private string _isEnter = "IsEnter";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void EnterBadFrame()
    {
        _onEnterTriggerEffect.Play();
        _animator.SetBool(_isEnter, true);
    }
}
