using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDemo : MonoBehaviour
{
    public float speed = 5f;
    public float angle = 45f;
    private float gravity = -9.81f;

    private float horizontalSpeed;
    private float verticalSpeed;
    [SerializeField]private Transform target;

    private void Start()
    {
        // Переводим угол в радианы
        float radians = angle * Mathf.PI / 270f;
        // Вычисляем горизонтальную скорость
        horizontalSpeed = speed * Mathf.Cos(radians);
        // Вычисляем вертикальную скорость
        verticalSpeed = speed * Mathf.Sin(radians);
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
       // float distanceThisFrame = ;
        // Вычисляем изменение позиции пули за фрейм
        float deltaX = horizontalSpeed * Time.deltaTime;
        float deltaY = verticalSpeed * Time.deltaTime + 0.5f * gravity * Time.deltaTime * Time.deltaTime;
        // Обновляем позицию пули
          transform.Translate(deltaX, deltaY, 0f);
        //transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
        // Обновляем вертикальную скорость
        verticalSpeed += gravity * Time.deltaTime;
    }

    




}
//public float angle = 45f;
//public float speed = 10f;
//public float gravity = -9.81f;
//public Transform target;

//private float timeOfFlight;
//private Vector3 targetPosition;
//private Vector3 initialVelocity;
//public float startTime;
//void Start()
//{
//    if (target == null)
//    {
//        Debug.LogError("Target transform is not set!");
//        return;
//    }

//    // Определяем начальную и конечную позиции
//    Vector3 startPosition = transform.position;
//    targetPosition = target.position;

//    // Рассчитываем время полета пули
//    float displacementY = targetPosition.y - startPosition.y;
//    float displacementX = targetPosition.x - startPosition.x;
//    timeOfFlight = Mathf.Sqrt(-2 * displacementY / gravity) + Mathf.Sqrt(2 * (displacementY - angle * displacementY / 90) / gravity);

//    // Рассчитываем начальную скорость пули
//    initialVelocity = new Vector3(displacementX / timeOfFlight, angle * timeOfFlight / 2, 0);

//    // Поворачиваем пулю в направлении цели
//    transform.rotation = Quaternion.LookRotation(initialVelocity, Vector3.up);
//}

//void Update()
//{
//    if (target == null)
//    {
//        return;
//    }

//    // Вычисляем время, прошедшее с начала выстрела
//    float elapsedTime = Time.time - startTime;

//    // Если время прошло, уничтожаем пулю
//    if (elapsedTime >= timeOfFlight)
//    {
//        Destroy(gameObject);
//        return;
//    }

//    // Рассчитываем текущую позицию пули
//    float xPos = initialVelocity.x * elapsedTime;
//    float yPos = initialVelocity.y * elapsedTime + 0.5f * gravity * elapsedTime * elapsedTime;
//    float zPos = initialVelocity.z * elapsedTime;

//    Vector3 currentPosition = new Vector3(transform.position.x + xPos, transform.position.y + yPos, transform.position.z + zPos);

//    // Перемещаем пулю в текущую позицию
//    transform.position = currentPosition;
//}



//public float speed = 10f;
//public float angle = 45f;
//private float gravity = Physics.gravity.magnitude;
//private Vector3 target;
//private Vector3 initialVelocity;
//private Transform targetPos;

//void Start()
//{
//    // Определяем цель, куда будет лететь пуля
//    target = new Vector3(10f, 2f, 0f);

//    // Рассчитываем начальную скорость пули, чтобы она полетела по дуге к цели
//    float displacementY = target.y - transform.position.y;
//    Vector3 displacementXZ = new Vector3(target.x - transform.position.x, 0f, target.z - transform.position.z);
//    float time = Mathf.Sqrt(-2f * angle / gravity) + Mathf.Sqrt(2f * (displacementY - angle * angle / (2f * gravity)) / gravity);
//    initialVelocity = displacementXZ / time;
//    initialVelocity.y = Mathf.Sqrt(-2f * gravity * displacementY + initialVelocity.y * initialVelocity.y);

//    // Задаем начальное направление движения пули
//    transform.rotation = Quaternion.LookRotation(initialVelocity);
//}

//void Update()
//{
//    // Обновляем позицию пули с учетом начальной скорости и гравитации
//    transform.position += initialVelocity * Time.deltaTime + 0.5f * Physics.gravity * Time.deltaTime * Time.deltaTime;
//    initialVelocity += Physics.gravity * Time.deltaTime;
//}



//public Vector3 start;
//public Vector3 target;
//public float speed = 10f;

//private float startTime;
//private float journeyLength;

//void Start()
//{
//    startTime = Time.time;
//    journeyLength = Vector3.Distance(start, target);
//}

//void Update()
//{
//    float distCovered = (Time.time - startTime) * speed;
//    float fracJourney = distCovered / journeyLength;
//    transform.position = Vector3.Lerp(start, target, fracJourney);
//}