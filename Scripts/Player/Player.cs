using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerBounceEffect))]
[RequireComponent(typeof(PlayerFightController))]
public class Player : MonoBehaviour
{
    [SerializeField] private Dumbell _dumbellLeft;
    [SerializeField] private Dumbell _dumbellRight;
    [SerializeField] private JumpingRope _jumpingRope;
    [SerializeField] private BoosterText _boosterText;
    [SerializeField] private int _requireAmountBoosters;
    [SerializeField] private float _muscleBoostFactor;
    [SerializeField] private float _muscleReductionFactor;
    [SerializeField] private float _jumpFlipTime;

    private PlayerBounceEffect _bounceEffect;
    private PlayerFightController _playerFightingController;
    private Transform _scale;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private int _currentAmountBoosters;

    public Animator Animator => _animator;
    public int CurrentAmountBoosters => _currentAmountBoosters;
    public int RequireAmountBoosters => _requireAmountBoosters;
    public float MuscleBoostFactor => _muscleBoostFactor;
    public float MuscleReductionFactor => _muscleReductionFactor;

    private void Start()
    {
        _scale = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _bounceEffect = GetComponent<PlayerBounceEffect>();
        _playerFightingController = GetComponent<PlayerFightController>();
    }

    private IEnumerator PickUp()
    {
        _animator.SetBool(StringConst._isPickUp, true);
        yield return new WaitForSeconds(0.25f);
        _animator.SetBool(StringConst._isPickUp, false);
    }

    private IEnumerator RunBiceps()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.25f);

        yield return waitForSeconds;
        _animator.SetBool(StringConst._isRunBiceps, true);
        yield return waitForSeconds;
        _animator.SetBool(StringConst._isRunBiceps, false);
    }

    private IEnumerator JumpingRope()
    {
        yield return new WaitForSeconds(0.25f);
        _animator.SetBool(StringConst._isJumpingRope, true);
        yield return new WaitForSeconds(1f);
        _animator.SetBool(StringConst._isJumpingRope, false);
    }

    private IEnumerator JumpFlip()
    {
        _animator.SetBool(StringConst._isJumping, true);
        _rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
        yield return new WaitForSeconds(0.7f);
        _animator.SetBool(StringConst._isJumping, false);
        _animator.SetBool(StringConst._isFlip, true);
        yield return new WaitForSeconds(_jumpFlipTime);
        _animator.SetBool(StringConst._isFlip, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MuscleBooster muscleBooster))
        {
            MuscleGaint();
            muscleBooster.PickUp();
            _boosterText.PlusBoost();
            _bounceEffect.DoBounce();

            if (other.TryGetComponent(out Lifted lifted))
            {
                StartCoroutine(PickUp());

                if (other.TryGetComponent(out JumpingRope jumpingRope))
                {
                    StartCoroutine(JumpingRope());
                    _jumpingRope.PlayerPickUp();
                }

                if (other.TryGetComponent(out Dumbell dumbell))
                {
                    _dumbellLeft.PlayerPickUp();
                    _dumbellRight.PlayerPickUp();
                    StartCoroutine(RunBiceps());
                }
            }
        }

        if (other.TryGetComponent(out AntiMuscleBooster antiMuscleBooster))
        {
            MuscleReduction();
            _boosterText.MinusBoost();
            Destroy(other.gameObject);
        }

        if (other.TryGetComponent(out Trampoline trampoline))
        {
            StartCoroutine(JumpFlip());
        }

        if (other.TryGetComponent(out LoseInWater loseInWater))
        {
            _animator.SetBool(StringConst._isJumping, true);
            _rigidbody.AddForce(Vector3.forward * 2, ForceMode.Impulse);
            loseInWater.PlayerInWater();
        }

        if (other.TryGetComponent(out GoodFrame goodFrame))
        {
            MuscleGaint();
            _boosterText.PlusBoost();
            goodFrame.EnterGoodFrame();
        }

        if (other.TryGetComponent(out BadFrame badFrame))
        {
            MuscleReduction();
            _boosterText.MinusBoost();
            badFrame.EnterBadFrame();
        }
    }

    public void Run()
    {
        _animator.SetBool(StringConst._isRinning, true);
    }

    public void Idle()
    {
        _animator.SetBool(StringConst._isRinning, false);
    }

    public void MuscleGaint()
    {
        _currentAmountBoosters++;
        _scale.localScale = Vector3.Lerp(_scale.localScale, new Vector3(_muscleBoostFactor, _muscleBoostFactor, _muscleBoostFactor), Time.deltaTime);
    }

    public void MuscleReduction()
    {
        _currentAmountBoosters--;
        _scale.localScale = Vector3.Lerp(_scale.localScale, new Vector3(_muscleReductionFactor, _muscleReductionFactor, _muscleReductionFactor), Time.deltaTime);
    }
}

public static class StringConst
{
    public const string _isRinning = "isRinning";
    public const string _isRunningHook = "isFlyingKick";
    public const string _isIdleBoxing = "isIdleBoxing";
    public const string _isFight = "isFight";
    public const string _isSmall = "isSmall";
    public const string _isHook = "isHook";
    public const string _isHook2 = "isHook2";
    public const string _isKick = "isKick";
    public const string _isPickUp = "isPickUp";
    public const string _isJumpKick = "isJumpKick";
    public const string _isLose = "isLose";
    public const string _isWin = "isWin";
    public const string _isBlock = "isBlock";
    public const string _isTakeHit = "isTakeHit";
    public const string _isRunBiceps = "isRunBiceps";
    public const string _isJumpingRope = "isJumpingRope";
    public const string _isJumping = "isJumping";
    public const string _isFlip = "isFlip";

}