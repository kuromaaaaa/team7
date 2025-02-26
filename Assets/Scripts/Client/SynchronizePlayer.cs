using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
	public class PlayerMove : MonoBehaviour
	{

		InputSystem_Actions _moveAction;
		public CommunicateData data = new();
		private Vector3 _savePos;
		private Quaternion _saveRot;


		private void Update()
		{

			if (transform.position != _savePos)
			{
				_savePos = transform.position;
				SendTransForm();
			}

			if (_saveRot != transform.rotation)
			{
				_saveRot = transform.rotation;
				SendRotation();
			}
		}

		private async void SendTransForm()
		{
			data.Command = "SynchronizeTransform";
			data.JsonBody = JsonUtility.ToJson(transform.position);
			string json = JsonUtility.ToJson(data);
			await WebManager.Instance.SendCommand(json);
		}
		private async void SendRotation()
		{
			data.Command = "SynchronizeRotation";
			data.JsonBody = JsonUtility.ToJson(transform.rotation);
			string json = JsonUtility.ToJson(data);
			await WebManager.Instance.SendCommand(json);
		}
	}
}
