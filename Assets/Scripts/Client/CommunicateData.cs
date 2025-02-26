using System;

public class CommunicateData
{
	public string ID;
	public string Command;
	public string JsonBody;
}

public struct UserData
{
	public string ID;
	public string Name;
}

[Serializable]
public class RoomPlayersData
{
	public string[] Players;
}
