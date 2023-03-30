using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveAI : MonoBehaviour
{




    public float enemySpeed = 5f; // скорость врага
    [SerializeField] private Vector3 startPos;
    public float playerDistance = 50f; // расстояние, на котором враг замечает игрока

  

    public float wanderRadius = 10f; // радиус области брождения
    public float wanderDistance = 5f; // расстояние, на которое враг будет смещаться от центра области
    public float wanderJitter = 1f; // сила случайной составляющей в направлении движения
    private NavMeshAgent agent;
    private Vector3 wanderTarget; // точка, к которой будет двигаться враг
    private LayerMask GroundL;
    void Start()
    {
        GroundL = LayerMask.GetMask("GroundL");
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
       


        wanderTarget = Random.insideUnitSphere * wanderRadius;
        wanderTarget += transform.position;
        wanderTarget.y = transform.position.y;
    }

    void Update()
    {


      
        
           
         
          
        

    


        transform.position = Vector3.MoveTowards(transform.position, wanderTarget, Time.deltaTime);

        // если приблизились к wanderTarget, то выбираем новую точку для брождения
        Vector3 distance = transform.position - wanderTarget;

        if (distance.magnitude < 1f)
        {

            StartCoroutine(newWalkPoint());
            if (Physics.Raycast(wanderTarget, -transform.up, 1f, GroundL))
            {
                WanderingTarget();
                StartCoroutine(newWalkPoint());
            }
            float randomX = UnityEngine.Random.Range(-4f, 4f);
            float randomZ = UnityEngine.Random.Range(-4f, 4f);
            wanderTarget = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        }
        agent.SetDestination(wanderTarget);

    }
    private IEnumerator newWalkPoint()
    {
        yield return new WaitForSeconds(2f);
        float randomX = UnityEngine.Random.Range(-4f, 4f);
        float randomZ = UnityEngine.Random.Range(-4f, 4f);
        wanderTarget = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
    }
    private void WanderingTarget()
    {
        float randomX = UnityEngine.Random.Range(-4f, 4f);
        float randomZ = UnityEngine.Random.Range(-4f, 4f);
        wanderTarget = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tep")
        {
            transform.position = startPos;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tep")
        {
            transform.position = startPos;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wanderTarget, 1f);
    }
}

