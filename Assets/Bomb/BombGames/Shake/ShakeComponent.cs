using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ShakeComponent : MonoBehaviour, IInputHandler
{
	private const float m_smoothingSpeed = 1.0f;

	// System
	private bool m_isShakingAllowed;

	// Mouse input
	private bool m_isShakeHeld;
	private Vector2 m_mouseDelta;

	private void Start()
	{
		((IInputHandler)this).SetControlsSubscription(true);
	}

	private void OnDisable()
	{
		((IInputHandler)this).SetControlsSubscription(false);
	}

	public void Subscribe()
	{
		InputManager.Controls.Player.Look.performed += OnMouseInput;

		InputManager.Controls.Player.Fire1.performed += OnShakeToggle;
		InputManager.Controls.Player.Fire1.canceled += OnShakeToggle;
	}

	public void Unsubscribe()
	{
		InputManager.Controls.Player.Look.performed -= OnMouseInput;

		InputManager.Controls.Player.Fire1.performed -= OnShakeToggle;
		InputManager.Controls.Player.Fire1.canceled -= OnShakeToggle;
	}

	public void OnMouseInput(InputAction.CallbackContext context) => m_mouseDelta = context.ReadValue<Vector2>();
	public void OnShakeToggle(InputAction.CallbackContext context) => m_isShakeHeld = context.ReadValueAsButton();

	private void Update()
	{
		Vector3 posDelta = Vector3.zero - transform.position;
		float distToCenter = posDelta.magnitude;

		Vector3 mousePos = new Vector3(m_mouseDelta.x, 0, m_mouseDelta.y);

		if (m_isShakingAllowed && m_isShakeHeld)
		{
			// Lerp the bomb towards the mouse delta
			transform.position = Vector3.Lerp(transform.position, mousePos, m_smoothingSpeed * Time.deltaTime);
		}
		else if (!m_isShakeHeld && distToCenter > 0.05f)
		{
			// Lerp the bomb back to origin
			transform.position = Vector3.Lerp(transform.position, Vector3.zero, m_smoothingSpeed * Time.deltaTime);
		}
	}

	public void SetShakingActive(bool active)
	{
		m_isShakingAllowed = active;
	}
}
