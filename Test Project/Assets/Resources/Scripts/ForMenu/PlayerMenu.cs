using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float Maxhealth = 100f;
    [SerializeField] private float currenthealth;
    [SerializeField] private int Bulletdamage;
    [SerializeField] private float BulletSpeed;
    [SerializeField] private float fireRate;
    [Space]
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform ShootPos;
    private float perHealth;
    [Space]
    [SerializeField] private float ExplosionRadius;
    [SerializeField] private Transform target;
    [SerializeField] private float AttackRadius;
    private float fireCountDown;
    private GameObject Buff;
    private GameObject Heal;
    private GameObject HealAllHp;
    [SerializeField] private int[] costs;

    [SerializeField] private GameObject WarningPanel;
    [SerializeField] private Text Warning;

    [Space]
    [SerializeField] private TextDamage[] textDamages;
    [SerializeField] private int textDamagesID;

    [Space]
    [SerializeField] private GameObject[] enemies;
    private float regenRate = 2f;
    private Vector3 startPos;


    [Header("Effects")]
    private float timer = 10f;
    [SerializeField] private Image fillEffect;
    private void Awake()
    {
        currenthealth = Maxhealth;
        startPos = transform.position;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        healthBar = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();

        costs = new int[4];
        costs[0] = 5;
        costs[1] = 5;
        costs[2] = 10;
        costs[3] = 5;

        perHealth = 1.00f / currenthealth;
        Buff = transform.GetChild(1).gameObject;
        Heal = transform.GetChild(2).gameObject;
        HealAllHp = transform.GetChild(3).gameObject;

        Buff.SetActive(false); 
        Heal.SetActive(false);
        HealAllHp.SetActive(false); 
    }

    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        healthBar.fillAmount -= perHealth * damage;

        textDamages[textDamagesID].Refresh(damage);
        textDamages[textDamagesID].transform.position = transform.position + Vector3.up;

        StartCoroutine(TextActive(textDamages[textDamagesID].gameObject));

        textDamagesID++;

        if (textDamagesID >= textDamages.Length)
        {
            textDamagesID = 0;
        }
        if (currenthealth <= 0)
        {
            Controller.instance.Restart.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        Bullet temp = Instantiate(bulletPref, ShootPos.position, Quaternion.identity).GetComponent<Bullet>();
        if (temp) temp.SetDamageSettings(speed: BulletSpeed, damage: Bulletdamage, ExpRadius: ExplosionRadius, target: target);
        Debug.Log("Shooot");
    }
    void Update()
    {
        if (!target) return;
        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = fireRate;
        }
        fireCountDown -= Time.deltaTime;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void FixedUpdate()
    {
        UpdateTarget();
      
    }
  
    private void UpdateTarget()
    {
        // GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Collider[] enemies = Physics.OverlapSphere(transform.position, AttackRadius, LayerMask.GetMask("Enemy"));
        float shotestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        for (int i = 0; i < enemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (distanceToEnemy < shotestDistance)
            {
                shotestDistance = distanceToEnemy;
                nearestEnemy = enemies[i].gameObject;
            }
        }
        if (nearestEnemy != null && shotestDistance <= AttackRadius) target = nearestEnemy.transform;
        else target = null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }
    public void BuffEffect()
    {
        if (Controller.money < costs[0])
        {
            StartCoroutine(WarningPaanel());
            Warning.text = "вам нехватает : " + (Controller.money - costs[0]) + " денег";
        }
        else
        {
            StartCoroutine(Bufftime());
            Bulletdamage += 2;
            BulletSpeed += 2;
            fireRate -= 0.01f;
            Controller.money -= costs[0];
        }
    }
    public void MeteorEffect()
    {
        if (fillEffect.fillAmount == 1)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Enemy>().Damagable();
            }
            fillEffect.fillAmount = 0;
        }
        else
        {
            Debug.Log("Выжди");
        }

    }
    public void HealEffect()
    {
        if (Controller.money < costs[1])
        {
            StartCoroutine(WarningPaanel());
            Warning.text = "вам нехватает : " + (Controller.money - costs[1]) + " денег";
        }
        else
        {
            StartCoroutine(HealTime());
            Maxhealth += 10;
            currenthealth = Maxhealth;
            perHealth = 1.00f / currenthealth;
            Controller.money -= costs[1];
        }

    }
    IEnumerator Bufftime()
    {
        Buff.SetActive(true);
        yield return new WaitForSeconds(2f);
        Buff.SetActive(false);
    }
    IEnumerator HealTime()
    {
        Heal.SetActive(true);
        yield return new WaitForSeconds(2f);
        Heal.SetActive(false);
    }

    IEnumerator WarningPaanel()
    {
        WarningPanel.SetActive(true);
        yield return new WaitForSeconds(0.98f);
        WarningPanel.SetActive(false);
    }


    private IEnumerator TextActive(GameObject text)
    {
        text.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        text.SetActive(false);
    }
    public void HealthRegen()
    {
        if (Controller.money < costs[3])
        {
            StartCoroutine(WarningPaanel());
            Warning.text = "вам нехватает : " + (Controller.money - costs[3]) + " денег";
        }
        else
        {
            currenthealth += regenRate * 1000 * Time.deltaTime;
            currenthealth = Mathf.Min(currenthealth, Maxhealth);
            healthBar.fillAmount += perHealth * (regenRate * 1000 * Time.deltaTime);
            StartCoroutine(HealthEffect());
            Controller.money -= costs[3];
        }

    }
    IEnumerator HealthEffect()
    {
        HealAllHp.SetActive(true);
        yield return new WaitForSeconds(3f);
        HealAllHp.SetActive(false);
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
}
