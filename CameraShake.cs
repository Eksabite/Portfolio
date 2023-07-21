using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    static CameraShake Instance;
    public Coroutine shakeRoutine;


    public void Awake()
    {
        Instance = this;
    }

    public static void ShakeCamera(EShakeStrenght JD, float czas)
    {
        if (Instance.shakeRoutine != null)
        {
            Instance.StopCoroutine(Instance.shakeRoutine);
        }
        Instance.shakeRoutine = Instance.StartCoroutine(Instance.Shake(czas, JD));
    }


    public IEnumerator Shake(float time, EShakeStrenght shakeStrenght)
    {
        float shakeEnumStrenght = ShakeStrenghtToFloat(shakeStrenght);
        Vector3 startPoint = transform.position;
        while (time > 0)
        {
            transform.position = startPoint + Random.onUnitSphere * shakeEnumStrenght;
            time -= Time.unscaledDeltaTime;
            yield return null;

        }
        transform.position = startPoint;
        shakeRoutine = null;
        yield return null;
    }

    public static float ShakeStrenghtToFloat(EShakeStrenght strenght)
    {
        switch (strenght)
        {
            case EShakeStrenght.weak:
                {
                    return 0.05f;
                }
            case EShakeStrenght.medium:
                {
                    return 0.03f;
                }
            case EShakeStrenght.strong:
                {
                    return 0.1f;
                }
        }
        return 10;
    }
}


public enum EShakeStrenght
{
    weak,
    medium,
    strong
}