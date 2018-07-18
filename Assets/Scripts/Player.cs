using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public int m_Health;
	public int m_MeleeDamage;
	public int m_Points;
	public float m_InteractDistance;
	public LayerMask m_LayerMask;

	public Text m_HealthText;
	public Text m_RepairText;
	public Text m_PointsText;
	public Text m_BuyText;
	public Text m_AmmoText;
	public GameObject m_HitMarker;
	private void Awake() {
		m_HealthText.text = m_Health.ToString();
		m_RepairText.text = null;
		m_PointsText.text = "0";
		m_BuyText.text = null;
	}

	private void Update() {
		RaycastHit hit;
		Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		
		if (Physics.Raycast(ray, out hit, m_InteractDistance)) {
			Transform ObjectHit = hit.transform;
			if(ObjectHit.name.Contains("Barricade")){ // Repair Barricade
				Barricade Barricade = ObjectHit.GetComponent<Barricade>();
				if(Barricade.Repairable()){
					m_RepairText.text = "Press E To Repair Barricade";
					if(Input.GetKeyDown(KeyCode.E))
						Barricade.StartRepair();
				}else
					m_RepairText.text = null;
			}

			if(ObjectHit.name.Contains("Weapon")){ // Buy Weapon
				if(ObjectHit.name.Contains("Pistol")){
					BuyPistol BuyPistol = ObjectHit.GetComponent<BuyPistol>();
					if(GetComponent<Pistol>() == null){
						m_BuyText.text = "Press E To Buy Pistol $" + BuyPistol.m_GunCost;
						if(Input.GetKeyDown(KeyCode.E) && m_Points >= BuyPistol.m_GunCost){
							m_Points -= BuyPistol.m_GunCost;
							gameObject.AddComponent<Pistol>();
						}
					}
					else{
						m_BuyText.text = "Press E To Buy Pistol Ammo $" + BuyPistol.m_AmmoCost;
						if(Input.GetKeyDown(KeyCode.E) && m_Points >= BuyPistol.m_AmmoCost){
							m_Points -= BuyPistol.m_AmmoCost;
							Pistol Pistol = GetComponent<Pistol>();
							Pistol.m_Ammo = Pistol.m_MaxAmmo;
							Pistol.m_AmmoText.text = Pistol.m_Clip + "/"+ Pistol.m_Ammo;
						}
					}
				}
			}
			Debug.DrawLine(ray.origin, hit.point, Color.blue);
		}else{
			m_RepairText.text = null;
			m_BuyText.text = null;
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

	public void AddPoints(int received){
		m_Points += received;
		m_PointsText.text = m_Points.ToString();
	}
}
