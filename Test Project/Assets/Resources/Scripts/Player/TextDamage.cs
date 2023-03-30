using UnityEngine;


public class TextDamage : MonoBehaviour
{
    [SerializeField] private TextMesh textMesh;
    private void Start()
    {
        if(!textMesh)
            textMesh = GetComponent<TextMesh>();

        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.up * 5 * Time.deltaTime;
    }

    public void Refresh(int damage)
    {
        textMesh.text = $"-{damage}";
    }
   
}
