using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public float posX;
    public float posY;
    public float posZ;

    public int foodCount;
}

public class cPlayerJsonData : MonoBehaviour
{
    string playerPath => Application.persistentDataPath + "Player";
    public void SavePlayerData(Transform playerPos, int foodCount)
    {
        PlayerData data = new PlayerData
        {
            posX = playerPos.position.x,
            posY = playerPos.position.y,
            posZ = playerPos.position.z,

            foodCount = foodCount,
        };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(playerPath, json);
    }

    public PlayerData LoadPlayerData()
    {
        if (!File.Exists(playerPath))
            return null;

        string json = File.ReadAllText(playerPath);
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        return data;
    }
}
