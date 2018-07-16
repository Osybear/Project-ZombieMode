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
	public Text m_RepairText;

	private void Awake() {
		m_RepairText.text = null;
	}

	public void RemoveHealth(int damage){
		m_Health -= damage;
		StopAllCoroutines();
		if(m_Health <= 0)
			m_Health = 0;
			
		m_Renderer.material.color = m_Renderer.material.color - new Color(0,0,0,.2f);
	}

	private void OnMouseOver() {
		if(m_Health < m_MaxHealth)
		{
			m_RepairText.text = "Press R To Repair Barricade";
			if(!m_Repair && Input.GetKeyDown(KeyCode.R)){
				m_Repair = true;
				Debug.Log("Swagger");
				StartCoroutine(Repair());
			}
		}else
			m_RepairText.text = null;
	}
	
	public IEnumerator Repair(){
		while(m_Health < m_MaxHealth){
			yield return new WaitForSeconds(m_RepairRate);
			m_Health++;
			m_Renderer.material.color = m_Renderer.material.color + new Color(0,0,0,.2f);
		}
	}
}
