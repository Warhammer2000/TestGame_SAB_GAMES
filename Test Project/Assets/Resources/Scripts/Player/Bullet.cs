using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform myTarget;
    private int myDamage;
    private float expRadius;
    private float mySpeed;


    public void SetDamageSettings(float speed, int damage, float ExpRadius, Transform target)
    {
        mySpeed = speed;
        myDamage = damage;
        expRadius = ExpRadius;
        myTarget = target;
    }
    
    private void Damage(Transform enemy)
    {
        Enemy en = enemy.GetComponent<Enemy>();
        en.TakeDamage(myDamage);
    }
    void Update()
    {
        if(!myTarget)
        {
            Destroy(gameObject); return;
        }
        Vector3 dir = myTarget.position - transform.position;
        float distanceThisFrame = mySpeed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag.Contains("Enemy"))
    //    {
    //        Enemy en = collision.GetComponent<Enemy>();
    //        Damage(myTarget);
    //    }
    //}
    private void HitTarget()
    {
        if (expRadius > 0)
        {
            Debug.Log("Explode");
        }
        else
        {
            Damage(myTarget);
        }

        Destroy(gameObject);
    }
}
