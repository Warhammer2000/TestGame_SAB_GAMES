using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public Joystick joystick;
    public float speed = 5f;
    public float rotationSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // ¬ычисл€ем вектор направлени€ движени€
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);

        // ѕровер€ем, что вектор направлени€ не равен нулю
        if (direction.magnitude > 0.1f)
        {
            // ¬ычисл€ем угол между направлением движени€ и направлением вперед
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // ¬ычисл€ем новый угол поворота игрока
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            // ѕоворачиваем игрока в направлении движени€
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // ¬ычисл€ем вектор скорости
        Vector3 velocity = direction * speed;

        // ѕримен€ем скорость к игроку
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }
}
