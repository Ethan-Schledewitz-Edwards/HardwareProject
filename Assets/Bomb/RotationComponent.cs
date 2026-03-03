using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationComponent : MonoBehaviour, IInputHandler
{
	[SerializeField] private float rotationSpeed = 0.2f;
	[SerializeField] private float deadzoneThreshold = 0.2f;

	private Vector2 m_mouseDelta;
	private bool m_isRotating;

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

		InputManager.Controls.Player.Fire2.performed += OnRotateToggle;
		InputManager.Controls.Player.Fire2.canceled += OnRotateToggle;
	}

	public void Unsubscribe()
	{
		InputManager.Controls.Player.Look.performed -= OnMouseInput;

		InputManager.Controls.Player.Fire2.performed -= OnRotateToggle;
		InputManager.Controls.Player.Fire2.canceled -= OnRotateToggle;
	}

	public void OnMouseInput(InputAction.CallbackContext context) => m_mouseDelta = context.ReadValue<Vector2>();
	public void OnRotateToggle(InputAction.CallbackContext context) => m_isRotating = context.ReadValueAsButton();

	void Update()
	{
		if (m_isRotating)
		{
			if (m_mouseDelta.magnitude < deadzoneThreshold)
				return;

			Vector2 input = m_mouseDelta.normalized * ((m_mouseDelta.magnitude - deadzoneThreshold) / (1 - deadzoneThreshold));

			float xRotation = input.y * rotationSpeed;
			float zRotation = -input.x * rotationSpeed;

			transform.Rotate(new Vector3(xRotation, 0, zRotation), Space.World);
		}
	}
}
