using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

	public Barricade m_Barricade;
	public Player m_Player;
	public SpawnManager m_SpawnManager;
	public NavMeshAgent m_Agent;
	public float m_AttackRate;
	public bool m_Attacking;
	public int m_AttackDamage;
	public int m_Health;

	public string m_Target;

	private void Update() {
		if(m_Barricade != null && m_Barricade.m_Health > 0)
		{
			m_Agent.destination = m_Barricade.transform.position;
			m_Target = "Barricade";
		}
		else{
			m_Agent.destination = m_Player.transform.position;
			m_Barricade = null;
			m_Target = "Player";
		}

		if (!m_Agent.pathPending)
		{
			if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
			{
				if (!m_Agent.hasPath || m_Agent.velocity.sqrMagnitude == 0f)
				{
					if(!m_Attacking){
						m_Attacking = true;
						StartCoroutine(Attack());
					}
				}
			}else{
				//Moving Towards Object
				m_Attacking = false;
			}
		}
	}

	private IEnumerator Attack(){
		while(m_Attacking){
			yield return new WaitForSeconds(m_AttackRate);
			if(m_Target == "Player")
				m_Player.RemoveHealth(m_AttackDamage);
			else if(m_Target == "Barricade")
				m_Barricade.RemoveHealth(m_AttackDamage);
		}
	}

	public void RemoveHealth(int damage){
		m_Health -= damage;
		
		if(m_Health == 0)
		{
			m_SpawnManager.ZombieDied();
			Destroy(gameObject);
		}
	}
}
	