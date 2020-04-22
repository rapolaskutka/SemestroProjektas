using UnityEngine;
public class ParticleHit : MonoBehaviour
{
    private HealthControl healthscript;
    public GameObject blood;
    private Transform PlayerPosition;
    private GameObject UI;
    void OnParticleCollision(GameObject other) 
    {
        if (healthscript.GetDamage(1, false, 1f))
        {
            Instantiate(blood, PlayerPosition.position, Quaternion.identity);
        }
    }
    private void Awake()
    {
        UI = GameObject.FindGameObjectWithTag("DScreen");
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        healthscript = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthControl>();
    }

    private void Start()
    {
        UI.SetActive(false);
    }
  
}
