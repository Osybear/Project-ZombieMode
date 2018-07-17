using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public int m_Health;
	public float m_InteractDistance;

	public Text m_HealthText;
	public Text m_RepairText;

	private void Awake() {
		m_HealthText.text = m_Health.ToString();
		m_RepairText.text = null;
	}

	private void Update() {
		Interact();
	}

	private void Interact(){
		RaycastHit hit;
		Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		
		if (Physics.Raycast(ray, out hit, m_InteractDistance)) {
			Transform ObjectHit = hit.transform;

			if(ObjectHit.name.Contains("Barricade")){
				Barricade Barricade = ObjectHit.GetComponent<Barricade>();
				if(Barricade.Repairable()){
					m_RepairText.text = "Press E To Repair Barricade";
					if(Input.GetKeyDown(KeyCode.E))
						StartCoroutine(Barricade.Repair());
				}else
					m_RepairText.text = null;
			}
			Debug.DrawLine(ray.origin, hit.point, Color.blue);
		}else{
			m_RepairText.text = null;
		}	
	}

	public void RemoveHealth(int damage){
		m_Health -= damage;

		if(m_Health <= 0)
		{
			m_Health = 0;
			Debug.LogError("GameOver");
			Debug.Break();
		}
		m_HealthText.text = m_Health.ToString();
	}
}
