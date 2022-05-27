using System.Collections;
using RunnerMovementSystem;
using RunnerMovementSystem.Examples;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(MovementSystem))]
public class PlayerFightController : MonoBehaviour
{
    [SerializeField] private PlayerHealth _healthSlider;
    [SerializeField] private Boss _boss;
    [SerializeField] private WinScript _winSript;
    [SerializeField] private LoseScript _loseSript;
    [SerializeField] private ParticleSystem[] _effects;
    [SerializeField] private BoosterText _boosterText;
    [SerializeField] private MouseInput _input;
    [SerializeField] private FightingControl _fightButton;
    [SerializeField] private StaminaSlider _staminaSlider;
    [SerializeField] private FightingCamera _fightCamera;

    private Player _player;
    private MovementSystem _playerMovement;
    private bool _isBlock;
    private bool _isAttackState;
    private bool _isBlockState;
    private float _damage = 18f;
    private float _health = 100;

    public bool IsBlock => _isBlock;
    public bool IsAttackState => _isAttackState;
    public float Health => _health;

    private void Start()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<MovementSystem>();

        _isAttackState = false;
        _isBlock = false;
    }

    private void Update()
    {
        CheckHealth();
        CheckStaminaValue();
    }

    private IEnumerator Idle()
    {
        _player.Animator.SetBool(StringConst._isIdleBoxing, true);
        yield return new WaitForSeconds(0.5f);
        _player.Animator.SetBool(StringConst._isIdleBoxing, false);
    }

    private IEnumerator Attack(string nameAttack)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

        _player.Animator.SetBool(nameAttack, true);
        yield return waitForSeconds;
        _player.Animator.SetBool(nameAttack, false);
    }

    private void CheckStaminaValue()
    {
        if (_staminaSlider.Slider != null && _staminaSlider.Slider.value > 4)
        {
            GetBlock();
        }
        else
        {
            _isBlockState = false;
            _isBlock = false;
        }
    }

    private void HitEffectControl()
    {
        if (_damage == 0)
        {
            _effects[3].Play();
            _boss.Blocking();
        }
        else if (_damage == 18)
        {
            _effects[0].Play();
        }
    }

    private IEnumerator StopAllAttack()
    {
        _isAttackState = false;
        IdleBoxing();
        _damage = 0;
        _effects[0].gameObject.SetActive(false);
        _effects[3].gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2.8f);
        _damage = 18;
        _effects[0].gameObject.SetActive(true);
        _effects[3].gameObject.SetActive(false);
    }

    private IEnumerator Hitting()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

        _player.Animator.SetBool(StringConst._isTakeHit, true);
        yield return waitForSeconds;
        _player.Animator.SetBool(StringConst._isTakeHit, false);
    }
    private IEnumerator Block()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);
        _isBlockState = true;

        while (_isBlockState == true)
        {
            _isBlock = true;
            _player.Animator.SetBool(StringConst._isBlock, true);
            yield return waitForSeconds;
            _player.Animator.SetBool(StringConst._isBlock, false);
            _isBlock = false;
        }
    }

    private IEnumerator JumpKick()
    {
        _player.Animator.SetBool(StringConst._isJumpKick, true);
        yield return new WaitForSeconds(0.3f);
        _effects[1].Play();
        _effects[4].Play();
        _player.Animator.SetBool(StringConst._isJumpKick, false);
    }

    private void CheckHealth()
    {
        if (_health <= 0)
        {
            _damage = 0;
            _player.Animator.SetBool(StringConst._isLose, true);
            _boss.Victory();
        }
    }

    private void TakeHit()
    {
        StartCoroutine(Hitting());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FinalFight finalFight))
        {
            if (_player.CurrentAmountBoosters >= _player.RequireAmountBoosters)
            {
                _player.Animator.SetBool(StringConst._isFight, true);
                _player.Animator.SetBool(StringConst._isRinning, false);
                _playerMovement.DontMove();
                _input.StartFinalFight();
                finalFight.TryStartFight();
                _boss.Fight();
            }
            else
            {
                finalFight.TryStartFight();
                _boss.CheckenDance();
                _player.Animator.SetBool(StringConst._isSmall, true);
                _loseSript.PlayerLose(true);
            }
        }

        if (other.TryGetComponent(out Dummy dummy))
        {
            _boosterText.PlusBoost();
            dummy.PlayerHitting();
            StartCoroutine(JumpKick());
            _player.MuscleGaint();
        }

        if (other.TryGetComponent(out Enemy enemy))
        {
            _playerMovement.HitEnemy();
            _boosterText.PlusBoost();
            enemy.PlayerAttack();
            StartCoroutine(JumpKick());
            _player.MuscleGaint();
        }

        if (other.TryGetComponent(out EmotionTrigger emotionTrigger))
        {
            _boosterText.MinusBoost();
            _player.MuscleReduction();
            _effects[2].Play();
        }
    }

    public void IdleBoxing()
    {
        StartCoroutine(Idle());
    }

    public void Victory()
    {
        _player.Animator.SetBool(StringConst._isWin, true);
        _effects[0].gameObject.SetActive(false);
        _winSript.TryWin(true);
    }

    public void HookOneAttack()
    {
        StartCoroutine(Attack(StringConst._isHook));
    }

    public void HookTwoAttack()
    {
        StartCoroutine(Attack(StringConst._isHook2));
    }

    public void KickAttack()
    {
        StartCoroutine(Attack(StringConst._isKick));
    }

    public void GetBlock()
    {
        StartCoroutine(Block());
    }

    public void StopAttack()
    {
        StartCoroutine(StopAllAttack());
    }

    public void AttackState(bool isAttackState)
    {
        _isAttackState = isAttackState;
    }

    public void AttackEffect(string nameBossHit)
    {
        _boss.Hitting(nameBossHit);
        _fightCamera.CameraReaction();
        _boss.TakeDamage(_damage);
        _staminaSlider.Fatigue();
        HitEffectControl();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _healthSlider.Hit();
        TakeHit();
    }
}