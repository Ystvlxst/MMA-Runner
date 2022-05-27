using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class FightingControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerFightController _playerFightController;
    [SerializeField] private StaminaSlider _staminaSlider;

    private Animator _animator;
    private bool _isFinalFight;
    private string _isAttack = "isAttack";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckStaminaValue();
    }

    private IEnumerator OnButtonAttackDown()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        _playerFightController.AttackState(true);

        while (_playerFightController.IsAttackState == true)
        {
            _playerFightController.HookOneAttack();

            if (_playerFightController.IsAttackState == false)
                break;

            yield return waitForSeconds;
            _playerFightController.HookTwoAttack();

            if (_playerFightController.IsAttackState == false)
                break;

            yield return waitForSeconds;
            _playerFightController.KickAttack();

            break;
        }
    }

    private void Attack()
    {
        ActiveButton(true);
        StartCoroutine(OnButtonAttackDown());
    }

    private void CheckStaminaValue()
    {
        if (_staminaSlider.Slider.value == 10)
            ActiveButton(false);
        else
            ActiveButton(true);
    }

    public void TryStartFinalFight(bool isFight)
    {
        _isFinalFight = isFight;

        if (isFight == true)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

    public void ActiveButton(bool isClickDown)
    {
        _animator.SetBool(_isAttack, isClickDown);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Attack();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _playerFightController.StopAttack();
        _staminaSlider.Recreation();
    }
}
