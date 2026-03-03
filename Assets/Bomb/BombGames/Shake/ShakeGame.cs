using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShakeComponent))]
public class ShakeGame : BombGameBase
{
	private ShakeComponent m_shakeComponent;

	private void Awake()
	{
		m_shakeComponent = GetComponent<ShakeComponent>();
	}

	public override void Activate()
	{
		base.Activate();

		m_shakeComponent.SetShakingActive(true);
	}

	public override void Deactivate() 
	{ 
		base.Deactivate();
		m_shakeComponent.SetShakingActive(false);
	}

	public override void Tick()
	{
		throw new System.NotImplementedException();
	}

	public override void Win()
	{
		throw new System.NotImplementedException();
	}
}
