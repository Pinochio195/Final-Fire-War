using Ring;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BotController _botController;

    [HeaderTextColor(0.5f, .5f, 1f, headerText = "GetDamage")]
    [ChangeColorLabel(0.2f, 1, 1)] public Image _imageDamage;

    [HideInInspector] public float _target = 1;
    [ChangeColorLabel(0.2f, 1, 1)] public float _reduceSpeed;
    [ChangeColorLabel(0.2f, 1, 1)] public List<AudioClip> _audioClipDie;
    [ChangeColorLabel(0.2f, 1, 1)] public AudioSource _audioSourceDie;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Settings.Tag_Bullet))
        {
            _rigidbody.AddForce(PlayerManager.Instance._playerController._directionBullet.normalized * 2.5f, ForceMode.Impulse);
            GetDamage(LevelManager.Instance._levelController._listDataWeapon[LevelManager.Instance.GetWeapon()].damage);
        }
    }

    public void GetDamage(float damage)
    {
        StartCoroutine(ReduceDamageOverTime(damage));
    }

    public IEnumerator ReduceDamageOverTime(float damage)
    {
        float currentFillAmount = _imageDamage.fillAmount;
        float targetFillAmount = Mathf.Clamp01(currentFillAmount - damage);

        while (_imageDamage.fillAmount > targetFillAmount)
        {
            _imageDamage.fillAmount -= _reduceSpeed * Time.deltaTime;
            if (_imageDamage.fillAmount <= 0)//die
            {
                _botController.ActiveRagdoll();//kích hoạt ragdool
                PlayerManager.Instance._playerController._listBot.Remove(_botController);//xóa khỏi danh sách check deadzone
                GameManager.Instance._gameController._listZombie.Remove(_botController);//xóa khỏi danh sách tổng
                GameManager.Instance.UpdateAlive();
                _audioSourceDie.PlayOneShot(_audioClipDie[Random.Range(0, _audioClipDie.Count)]);
            }
            yield return null;
        }
    }



}