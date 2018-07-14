using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public int m_Health;
	public Text m_HealthText;

	private void Awake() {
		m_HealthText.text = m_Health.ToString();
	}

	public void RemoveHealth(int damage){
		m_Health -= damage;

		if(m_Health <= 0)
		{
			m_Health = 0;
			Debug.LogError("Game Over");
		}
		m_HealthText.text = m_Health.ToString();
	}
}
