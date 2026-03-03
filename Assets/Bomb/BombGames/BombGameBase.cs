using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BombGameBase : MonoBehaviour
{
	protected BombController m_controller;
	[SerializeField] protected GameObject[] m_activeElements;

	protected bool m_isActive;

	public void Initialize(BombController bombController)
	{
		m_controller = bombController;
	}

	public virtual void Activate()
	{
		m_isActive = true;

		foreach (GameObject i in m_activeElements)
		{
			i.SetActive(true);
		}
	}

	public virtual void Deactivate()
	{
		m_isActive = false;

		foreach (GameObject i in m_activeElements)
		{
			i.SetActive(false);
		}
	}

	public abstract void Tick();

	public abstract void Win();

	public virtual void Lose() { }
}
