using Lean.Pool;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _effectTouchBorder;
    [SerializeField] private ParticleSystem _effectTouchZombie;

    private void OnEnable()
    {
        _rigidbody.velocity = PlayerManager.Instance._playerController._directionBullet * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Settings.Tag_Border))
        {
            LeanPool.Spawn(_effectTouchBorder, transform.position, Quaternion.identity);
            MusicManager.Instance.PlayAudio(Ring.MusicController.TypeMusic.TouchBorder);
            LeanPool.Despawn(this);
        }
        else if (other.gameObject.CompareTag(Settings.Tag_Zombie))
        {
            LeanPool.Spawn(_effectTouchBorder, transform.position, Quaternion.identity);
            //LeanPool.Spawn(_effectTouchZombie, transform.position, Quaternion.identity);
            MusicManager.Instance.PlayAudio(Ring.MusicController.TypeMusic.TouchZombie);
            LeanPool.Despawn(this);
        }
    }
}