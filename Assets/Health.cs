using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    public float overallHealth = 5;
    private float currentHealth;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = overallHealth;
        healthBar.fillAmount = currentHealth / overallHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage) {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / overallHealth;
        if(currentHealth <= 0) {
            if(gameObject.tag == "Enemy")
            {
                GameObject.Find("GameManager").GetComponent<gameManager>().addKill();

            }
            Destroy(gameObject);   
         }
    }
}
