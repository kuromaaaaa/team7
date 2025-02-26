using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
	[SerializeField] InputField inputField;

	public void InputName()
	{
		Guid guid = Guid.NewGuid();
		PlayerDataManager.Instance.PlayerData.ID = guid.ToString();
		PlayerDataManager.Instance.PlayerData.Name = inputField.text;
	}
}
