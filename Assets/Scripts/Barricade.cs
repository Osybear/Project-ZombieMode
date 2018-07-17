using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barricade : MonoBehaviour {

	public int m_MaxHealth;
	public int m_Health;
	public bool m_Repair;
	public float m_RepairRate;
	public MeshRenderer m_Renderer;

	public void RemoveHealth(int damage){
		m_Health -= damage;
		m_Repair = false;
		if(m_Health <= 0)
			m_Health = 0;
			
		m_Renderer.material.color = m_Renderer.material.color - new Color(0,0,0,.2f);
	}
	
	public IEnumerator Repair(){
		m_Repair = true;
		while(m_Health < m_MaxHealth){
			yield return new WaitForSeconds(m_RepairRate);
			m_Health++;
			m_Renderer.material.color = m_Renderer.material.color + new Color(0,0,0,.2f);
		}
		m_Repair = false;
	}

	public bool Repairable(){
		return m_Health < m_MaxHealth && !m_Repair;
	}
}
