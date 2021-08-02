using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletAi : MonoBehaviour
{
    public GameObject crashSprite;
    public float lifetime = 2;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject clone = Instantiate(crashSprite, transform.position, Quaternion.identity);
        GetComponent<MoveForward>().enabled = false;
        GetComponent<BoxCollider2D>().isTrigger = true;

        Health healthScript = collision.gameObject.GetComponent<Health>();
        if (healthScript) {
            healthScript.takeDamage(1);
        }

        Destroy(clone,3);
        Destroy(gameObject);
    }
}
