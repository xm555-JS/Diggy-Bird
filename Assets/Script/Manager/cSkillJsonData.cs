using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SkillData
{
    public int diggySkill;
    public int magneticSkill;
    public int hasteSkill;
}

public class cSkillJsonData : MonoBehaviour
{
    string skillPath => Application.persistentDataPath + "Skill";

    public void SaveSkillData(int diggySkill, int magneticSkill, int hasteSkill)
    {
        SkillData skillData = new SkillData
        {
            diggySkill = diggySkill,
            magneticSkill = magneticSkill,
            hasteSkill = hasteSkill
        };
        string json = JsonUtility.ToJson(skillData);
        File.WriteAllText(skillPath, json);
    }

    public SkillData LoadSkillData()
    {
        if (!File.Exists(skillPath))
            return null;

        string json = File.ReadAllText(skillPath);
        SkillData data = JsonUtility.FromJson<SkillData>(json);
        return data;
    }
}
