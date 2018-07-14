using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour {

	public int m_Clip;
	public int m_MaxClip;
	public int m_MaxAmmo;
	public int m_Ammo;
	public bool m_Reloading;
	public float m_ReloadingDelay;
	public float m_DestroyIndicatorDelay;

	public GameObject m_Indicator;
	public Text m_AmmoText;

	private void Awake() {
		m_AmmoText.text = m_Clip + "/"+ m_Ammo;
	}

	void Update () {
		if(!m_Reloading && Input.GetKeyDown(KeyCode.R)){
			StartCoroutine(Reloading());
		}

		if(m_Clip > 0 && !m_Reloading){
			if(Input.GetMouseButtonDown(0)){
				m_Clip--;
				m_AmmoText.text = m_Clip + "/"+ m_Ammo;
				RaycastHit hit;
				Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
				
				if (Physics.Raycast(ray, out hit)) {
					Transform ObjectHit = hit.transform;
					GameObject Indicator = Instantiate(m_Indicator, hit.point, Quaternion.identity);
					StartCoroutine(DestroyIndicator(Indicator));

					if(ObjectHit.name.Contains("Zombie")){
						ObjectHit.GetComponent<Zombie>().RemoveHealth(1);
						Indicator.transform.SetParent(hit.transform);
					}

					Debug.DrawLine(ray.origin, hit.point, Color.red, 1f);
				}
			}
		}

	}

	public IEnumerator Reloading(){
		if(m_Ammo <= 0)
		{
			m_AmmoText.text = "Out Of Ammo";
			yield break;
		}else if(m_Clip == m_MaxClip)
			yield break;

		m_AmmoText.text = "Reloading";
		m_Reloading = true;

		yield return new WaitForSeconds(m_ReloadingDelay);
		int ammoneeded = m_MaxClip - m_Clip;

		if(m_Ammo < ammoneeded){
			m_Clip += m_Ammo;
			m_Ammo = 0;
		}
		else{
			m_Ammo -= ammoneeded;
			m_Clip = m_MaxClip;
		}

		m_AmmoText.text = m_Clip + "/"+ m_Ammo;
		m_Reloading = false;
	}

	public IEnumerator DestroyIndicator(GameObject Indicator){
		yield return new WaitForSeconds(m_DestroyIndicatorDelay);
		Destroy(Indicator);
	}
}
