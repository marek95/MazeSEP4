using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // https://gist.github.com/ftvs/5822103
    private Vector3 _originalPos;
    public static CameraShake _instance;
    void Awake()
    {
        _originalPos = transform.localPosition;
        _instance = this;
    }

    public static void Shake()
    {
        float duration = 0.2f;
        float amount = 0.2f;
        _instance.StopAllCoroutines();
        _instance.StartCoroutine(_instance.CoroutineShake(duration, amount));
    }

    public IEnumerator CoroutineShake(float duration, float amount)
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            transform.localPosition = _originalPos + Random.insideUnitSphere * amount;

            duration -= Time.deltaTime;

            yield return null;
        }
        transform.localPosition = _originalPos;
    }
}
