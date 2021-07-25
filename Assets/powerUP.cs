using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum powers { health, shield, heavymachine }
public class powerUP : MonoBehaviour
{
    public powers currentPowerup = powers.health;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (sr.sprite.name){
            case "powerupBlue_bolt.png":
                currentPowerup = powers.health;
                return ;
            case "powerupBlue_shield.png":
                currentPowerup = powers.shield;
                return;
            case "powerupBlue_star.png":
                currentPowerup = powers.heavymachine;
                return;
        }
    
        
    }
}
