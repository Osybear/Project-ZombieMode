using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Barricade : MonoBehaviour {

	public int m_Health;

	public void RemoveHealth(int damage){
		m_Health -= damage;

		if(m_Health <= 0)
		{
			GetComponent<MeshRenderer>().enabled = false;
			m_Health = 0;
		}
	}
}
