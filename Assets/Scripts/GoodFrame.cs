using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GoodFrame : MonoBehaviour
{
    [SerializeField] private ParticleSystem _onEnterTriggerEffect;

    private Animator _animator;
    private string _isEnter = "IsEnter";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void EnterGoodFrame()
    {
        _onEnterTriggerEffect.Play();
        _animator.SetBool(_isEnter, true);
    }
}
