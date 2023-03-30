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

        // ��������� ������ ����������� ��������
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);

        // ���������, ��� ������ ����������� �� ����� ����
        if (direction.magnitude > 0.1f)
        {
            // ��������� ���� ����� ������������ �������� � ������������ ������
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // ��������� ����� ���� �������� ������
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            // ������������ ������ � ����������� ��������
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // ��������� ������ ��������
        Vector3 velocity = direction * speed;

        // ��������� �������� � ������
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }
}
