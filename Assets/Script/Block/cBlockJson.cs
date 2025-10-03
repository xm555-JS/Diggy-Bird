using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class BlockData
{
    public float surfacePosX;
    public float surfacePosY;
    public float surfacePosZ;
}

[System.Serializable]
public class BlockDataList
{
    public List<BlockData> DataList = new List<BlockData>();
}

[System.Serializable]
public class LineData
{
    public int curLine;
    public int maxLine;
}

public class cBlockJson
{
    string path => Application.persistentDataPath + "block";
    string linePath => Application.persistentDataPath + "Line";

    public void SaveBlockData(List<GameObject> blockList)
    {
        BlockDataList blockDataList = new BlockDataList();
        foreach (var block in blockList)
        {
            blockDataList.DataList.Add(new BlockData
            {
                surfacePosX = block.transform.position.x,
                surfacePosY = block.transform.position.y,
                surfacePosZ = block.transform.position.z
            });
        }
        string json = JsonUtility.ToJson(blockDataList, true);
        File.WriteAllText(path, json);
    }

    public List<BlockData> LoadBlockData()
    {
        if (!File.Exists(path))
            return null;

        string json = File.ReadAllText(path);
        BlockDataList data = JsonUtility.FromJson<BlockDataList>(json);

        return data.DataList;
    }

    public void CleanData()
    {
        if (!File.Exists(path))
            return;

        File.Delete(path);
    }

    public void SaveLineData(int curLine, int maxLine)
    {
        LineData lineData = new LineData()
        {
            curLine = curLine,
            maxLine = maxLine
        };
        string lineJson = JsonUtility.ToJson(lineData);
        File.WriteAllText(linePath, lineJson);
    }

    public LineData LoadLineData()
    {
        if (!File.Exists(linePath))
            return null;

        string lineddata = File.ReadAllText(linePath);
        LineData Data = JsonUtility.FromJson<LineData>(lineddata);
        return Data;
    }
}
