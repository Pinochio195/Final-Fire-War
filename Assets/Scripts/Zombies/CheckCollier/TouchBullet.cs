using Ring;
using System.Collections;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Settings.Tag_Bullet))
        {
            _rigidbody.AddForce(PlayerManager.Instance._playerController._directionBullet.normalized * 2.5f, ForceMode.Impulse);
            GetDamage(.12f);
        }
    }

    public void GetDamage(float damage)
    {
        StartCoroutine(ReduceDamageOverTime(damage));
    }

    private IEnumerator ReduceDamageOverTime(float damage)
    {
        float currentFillAmount = _imageDamage.fillAmount;
        float targetFillAmount = Mathf.Clamp01(currentFillAmount - damage);

        while (_imageDamage.fillAmount > targetFillAmount)
        {
            _imageDamage.fillAmount -= _reduceSpeed * Time.deltaTime;
            if (_imageDamage.fillAmount <= 0)
            {
                _botController.ActiveRagdoll();
                PlayerManager.Instance._playerController._listBot.Remove(_botController);
                GameManager.Instance.UpdateAlive();

            }
            yield return null;
        }
    }



}