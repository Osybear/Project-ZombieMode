using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour {

	public List<SpawnPoint> m_SpawnPoints;
	public int m_Round;
	public Text m_RoundText;

	public float m_RoundDelay;
	public float m_SpawnRate;
	public GameObject m_ZombiesPrefab;
	public Player m_Player;
	public int m_ZombiesDead;

	private void Awake() {
		m_Round++;
		m_RoundText.text = m_Round.ToString();
		StartCoroutine(SpawnZombies());
	}

	private IEnumerator SpawnZombies(){
		yield return new WaitForSeconds(m_RoundDelay);
		
		int ZombiesSpawned = 0;
		while(ZombiesSpawned != m_Round){
			yield return new WaitForSeconds(m_SpawnRate);
			ZombiesSpawned++;
			SpawnPoint SpawnPoint = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Count)];
			GameObject Prefab = Instantiate(m_ZombiesPrefab, SpawnPoint.transform.position, Quaternion.identity);
			Zombie Zombie = Prefab.GetComponent<Zombie>();
			Zombie.m_Barricade = SpawnPoint.m_Barricade;
			Zombie.m_Player = m_Player;
			Zombie.m_SpawnManager = this;
		}
	}

	public void ZombieDied(){
		m_ZombiesDead++;
		if(m_ZombiesDead == m_Round){
			m_Round++;
			m_RoundText.text = m_Round.ToString();
			StartCoroutine(SpawnZombies());
			m_ZombiesDead = 0;
		}
	}
}
