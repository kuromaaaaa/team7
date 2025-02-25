using System;

namespace DefaultNamespace
{
	public class PlayerDataManager : SingletonMonoBehavior<PlayerDataManager>
	{
		public PlayerData PlayerData = new PlayerData();
	}

	[Serializable]
	public class RankingData
	{
		public PlayerData[] Ranking ;
	}

	[Serializable]
	public class PlayerData
	{
		public string ID;
		public string Name;
		public float Score;
	}
}
