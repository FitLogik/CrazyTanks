using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1P : MonoBehaviour
{
    public float speed = 1f;
    public float maxRotationAngle = 70f;

    // Start is called before the first frame update


    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontal, 0f) * speed * Time.deltaTime;
        transform.Translate(movement);


        float currentRotation = transform.eulerAngles.z;

        // Ограничиваем вращение по оси Z
        if (currentRotation > 180)
        {
            currentRotation -= 360;
        }

        currentRotation = Mathf.Clamp(currentRotation, -maxRotationAngle, maxRotationAngle);

    }
}
