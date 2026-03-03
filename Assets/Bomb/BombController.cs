using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
	// Constants
	private const int k_timerMaxSeconds = 60;

	// System Vars
    private BombGameBase[] m_Games;

	private int m_gameDigit1;
	private int m_gameDigit2;
	private int m_gameDigit3;
	private int m_gameDigit4;

	private bool m_isBombActive;
	private int m_currentGame;
	private float m_timer;

	#region Monobehaviour Callbacks

	private void Awake()
	{
		m_gameDigit1 = Random.Range(0, 10);
		m_gameDigit2 = Random.Range(0, 10);
		m_gameDigit3 = Random.Range(0, 10);
		m_gameDigit4 = Random.Range(0, 10);

		// Initialize all games
		m_Games = GetComponents<BombGameBase>();
		foreach (BombGameBase game in m_Games)
		{
			game.Initialize(this);
		}
	}

	private void Start()
	{
		ResetSystem();

		m_isBombActive = true;
		IncrementGame();
	}

	private void Update()
	{
		if (m_isBombActive)
		{
			m_timer += Time.deltaTime;

			if (m_timer >= k_timerMaxSeconds)
			{
				Lose();
			}
		}
	}
	#endregion

	private void ResetSystem()
	{
		m_isBombActive = false;
		m_currentGame = -1;
		m_timer = 0.0f;
	}

	// Increments the score. activates a random game, and deactivates the others.
	public void IncrementGame()
	{
		m_currentGame++;

		int gameCount = m_Games.Length;

		// Activate a random game
		int rand = Random.Range(0, gameCount);
		for (int i = 0; i < gameCount; i++) 
		{ 
			if(i == rand)
				m_Games[i].Activate();
			else 
				m_Games[i].Deactivate();
		}
	}

	public void Win()
	{
		ResetSystem();

		Debug.Log("You win!");
	}

	public void Lose()
	{
		ResetSystem();

		Debug.Log("Bomb exploded!");
	}
}
