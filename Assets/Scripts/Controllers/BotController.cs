using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : TankController
{
    public float minMoveDuration = 0f;
    public float maxMoveDuration = 2f;
    public float minRotateDuration = 0f;
    public float maxRotateDuration = 5f;

    float MaxFireDuration => (maxBulletSpeed - minBulletSpeed) / bulletSpeedMultiplier;


    protected override void Start()
    {
        base.Start();
        SetColor(PrefsManager.DefaultColor);


        StartCoroutine(SimulateInput()); 
    }


    private IEnumerator SimulateInput()
    {
        while (true)
        {
            float moveDuration = Random.Range(minMoveDuration, maxMoveDuration);
            float rotateDuration = Random.Range(minRotateDuration, maxRotateDuration);
            float fireDuration = Random.Range(0, MaxFireDuration);

            float moveInput = Random.Range(-1f, 1f);
            float finalRotation = Random.Range(minMuzzleRotation, maxMuzzleRotation);


            StartCoroutine(ApplyMoveInput(moveInput, moveDuration));
            yield return new WaitForSeconds(moveDuration);

            StartCoroutine(ApplyRotateInput(finalRotation, rotateDuration));
            yield return new WaitForSeconds(rotateDuration);

            StartCoroutine(ApplyFireInput(fireDuration));
            yield return new WaitForSeconds(fireDuration);

            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }

    private IEnumerator ApplyMoveInput(float input, float duration)
    {
        moveInput = input;

        yield return new WaitForSeconds(duration);

        moveInput = 0f;
    }


    private IEnumerator ApplyRotateInput(float finalRotation, float duration)
    {
        float currentRotation = muzzleTransform.localEulerAngles.z;
        float deltaRotation = Mathf.DeltaAngle(currentRotation, finalRotation);

        rotationInput = deltaRotation > 0f ? 1f : -1f;

        muzzleRotationSpeed = Mathf.Abs(deltaRotation) / duration;

        yield return new WaitForSeconds(duration);

        rotationInput = 0f;

    }

    private IEnumerator ApplyFireInput(float duration)
    {
        fireInput = 1f;

        yield return new WaitForSeconds(duration);

        fireInput = 0f;
    }

}