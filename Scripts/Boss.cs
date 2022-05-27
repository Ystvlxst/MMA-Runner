using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Boss : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private BossHealth _healthSlider;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerFightController _playerFightingController;
    [SerializeField] private LoseScript _loseSript;
    [SerializeField] private ParticleSystem[] _hitEffects;

    private Animator _animator;
    private float _damage;

    public float Health => _health;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _damage = 7;
    }

    private void Update()
    {
        CheckHealth();
        CheckPlayerBlock();
    }

    private IEnumerator Attack(string nameAttack)
    {
        while (_health > 0)
        {
            yield return new WaitForSeconds(2);
            _animator.SetBool(nameAttack, true);
            yield return new WaitForSeconds(0.5f);
            _animator.SetBool(nameAttack, false);
        }
    }

    private IEnumerator TakeHit(string hitName)
    {
        _damage = 0;
        _hitEffects[0].gameObject.SetActive(false);
        _hitEffects[1].gameObject.SetActive(false);
        _animator.SetBool(hitName, true);
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool(hitName, false);
        yield return new WaitForSeconds(1.5f);
        _hitEffects[0].gameObject.SetActive(true);
        _hitEffects[1].gameObject.SetActive(true);
        _damage = 7;
    }

    private IEnumerator Block()
    {
        _animator.SetBool(StringConstBoss._isBlock, true);
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool(StringConstBoss._isBlock, false);
    }

    private void CheckHealth()
    {
        if (_health <= 0)
        {
            _animator.SetBool(StringConstBoss._isLose, true);
            StopAttack();
            _playerFightingController.Victory();
        }

        if (_health <= 60)
            _animator.speed = 1f;
    }

    private void CheckPlayerBlock()
    {
        if (_playerFightingController.IsBlock == true)
            _damage = 1;
        else
            _damage = 7;
    }

    private IEnumerator AttackQueue()
    {
        StartCoroutine(Attack(StringConstBoss._isHook));
        yield return new WaitForSeconds(1);
        StartCoroutine(Attack(StringConstBoss._isKick));
    }

    public void Victory()
    {
        _damage = 0;
        _hitEffects[0].gameObject.SetActive(false);
        _animator.SetBool(StringConstBoss._isWin, true);
        _loseSript.PlayerLose(true);
    }

    public void Fight()
    {
        StartCoroutine(AttackQueue());
    }

    public void Blocking()
    {
        StartCoroutine(Block());
    }

    public void CheckenDance()
    {
        _animator.SetBool(StringConstBoss._isDance, true);
    }

    public void Hitting(string nameHit)
    {
        StopAttack();
        StartCoroutine(TakeHit(nameHit));
        StopAttack();
    }

    public void PlayerAttackEffect()
    {
        if (_playerFightingController.IsBlock == false)
            _hitEffects[0].Play();
        else
            _hitEffects[1].Play();

        _playerFightingController.TakeDamage(_damage);
    }

    public void TakeDamage(float damage)
    {
        _healthSlider.Hit();
        _health -= damage;
    }

    public void StopAttack()
    {
        StopCoroutine(Attack(StringConstBoss._isHook));
        StopCoroutine(Attack(StringConstBoss._isKick));
    }
}

public static class StringConstBoss
{
    public const string _isDance = "isDance";
    public const string _isHook = "isHook";
    public const string _isKick = "isKick";
    public const string _isLose = "isLose";
    public const string _isWin = "isWin";
    public const string _isHeadHit = "isHeadHit";
    public const string _isHeadHitRightHand = "isHeadHitRightHand";
    public const string _isBodyHit = "isBodyHit";
    public const string _isBlock = "isBlock";
}
