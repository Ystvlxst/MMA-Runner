using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Player))]
public class PlayerBounceEffect : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    public void DoBounce()
    {
        _player.transform.DOShakeScale(0.2f, strength: new Vector3(0.1f, 0.1f, 0.1f), vibrato: 1, fadeOut: true);
    }
}
