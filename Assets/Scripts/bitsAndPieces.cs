using UnityEngine;

namespace DefaultNamespace
{
	public class bitsAndPieces : MonoBehaviour
	{
		public void SceneLoadButton(string sceneName)
		{ 
			SceneLoader.Instance.FadeAndLoadSceneAsync(sceneName);
		}
		
		public void Death()
		{
			Destroy(gameObject);
		}

		public void SoundStop()
		{
			BGMManager.Instance.StopBGM();
		}
	}
}
