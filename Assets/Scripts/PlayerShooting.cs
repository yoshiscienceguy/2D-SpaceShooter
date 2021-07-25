using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerShooting : MonoBehaviour {

	public Vector3 bulletOffset = new Vector3(0, 0.5f, 0);

	public GameObject bulletPrefab;
	int bulletLayer;
	
	public float fireDelay = 0.25f;
	float cooldownTimer = 0;
	bool HeavyWeapons;
	public GameObject HeavyIcon;
	public Image HeavyWeaponsCoolDown;
	private float hw_time;
	void Start() {
		bulletLayer = gameObject.layer;
		HeavyWeaponsCoolDown.fillAmount = 0;
		oldDelay = fireDelay;
	}
	float oldDelay;
	public void HeavyMachineGun() {


		
		HeavyWeapons = true;
		fireDelay = .1f;
		hw_time = 5;
		HeavyIcon.SetActive(true);
	}

	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;

		if( Input.GetButton("Fire1") && cooldownTimer <= 0 ) {
			// SHOOT!
			cooldownTimer = fireDelay;

			Vector3 offset = transform.rotation * bulletOffset;

			GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position + offset, transform.rotation);
			bulletGO.layer = bulletLayer;
		}
		if (HeavyWeapons) {
			hw_time -= Time.deltaTime;
			HeavyWeaponsCoolDown.fillAmount = hw_time / 5.0f;

			if (hw_time <= 0) {
				fireDelay = oldDelay;
				HeavyWeapons = false;
				HeavyIcon.SetActive(false);
			}
		}
		
	}


}
