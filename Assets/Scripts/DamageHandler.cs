using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DamageHandler : MonoBehaviour {

	public float overallHealth = 1;
	private float currentHealth;
	public float invulnPeriod = 0;
	float invulnTimer = 0;
	int correctLayer;
	public GameObject shield;
	private float shieldHealth;
	public Image healthbar;
	SpriteRenderer spriteRend;
	public GameObject healthHeal_icon;
	private float healIcon_timeOut;
	void Start() {
		correctLayer = gameObject.layer;
		currentHealth = overallHealth;
		healthbar.fillAmount = currentHealth / overallHealth;
		
		// NOTE!  This only get the renderer on the parent object.
		// In other words, it doesn't work for children. I.E. "enemy01"
		spriteRend = GetComponent<SpriteRenderer>();

		if(spriteRend == null) {
			spriteRend = transform.GetComponentInChildren<SpriteRenderer>();

			if(spriteRend==null) {
				Debug.LogError("Object '"+gameObject.name+"' has no sprite renderer.");
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Enemy"))
		{

			if (shield.activeSelf)
			{
				shieldHealth--;
				
				shield.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, (5.0f - shieldHealth) / 5.0f);
				if (shieldHealth <= 0)
				{
					shield.SetActive(false);
				}
			}
			else {
				currentHealth--;
				healthbar.fillAmount = currentHealth / overallHealth;
				if (currentHealth <= 0)
				{
					Die();
				}
				if (invulnPeriod > 0)
				{
					invulnTimer = invulnPeriod;
					gameObject.layer = 8;
				}
			}

			
		}
	}

	void Update() {

		if(invulnTimer > 0) {
			invulnTimer -= Time.deltaTime;

			if(invulnTimer <= 0) {
				gameObject.layer = correctLayer;
				if(spriteRend != null) {
					spriteRend.enabled = true;
				}
			}
			else {
				if(spriteRend != null) {
					spriteRend.enabled = !spriteRend.enabled;
				}
			}
		}
		if (healIcon_timeOut >= 0)
		{
			healIcon_timeOut -= Time.deltaTime;
			if (healIcon_timeOut <= 0) {
				healthHeal_icon.SetActive(false);
			}

		}

		
	}

	void Die() {
		GameObject.Find("GameManager").GetComponent<gameManager>().gameOver = true;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject enemy in enemies) {
			Destroy(enemy, 3);
		}
		Destroy(gameObject);
	}

	public void heal() {
		currentHealth += 3;
		healthbar.fillAmount = currentHealth / overallHealth;
		healthHeal_icon.SetActive(true);
		healIcon_timeOut = 3;
	}

	public void shields() {
		shield.SetActive(true);
		shieldHealth = 5;
		shield.GetComponent<SpriteRenderer>().color = Color.white;
	}

	
}
