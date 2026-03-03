using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputHandler 
{
	#region Input Methods

	public void SetControlsSubscription(bool isInputEnabled)
	{
		if (isInputEnabled)
			Subscribe();
		else
			Unsubscribe();
	}

	public abstract void Subscribe();

	public abstract void Unsubscribe();

	#endregion
}
