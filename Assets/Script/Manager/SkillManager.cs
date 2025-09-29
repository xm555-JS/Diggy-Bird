using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    static public SkillManager instance;

    int diggySkill = 0;
    int magneticSkill = 0;
    int hasteSkill = 0;

    public void DiggySkillUp() { diggySkill++; PlayerPrefs.SetInt("diggySkill", diggySkill); }
    public void DiggySkillDown() { diggySkill--; PlayerPrefs.SetInt("diggySkill", diggySkill); }

    public void MagneticSkillUp() { magneticSkill++; PlayerPrefs.SetInt("magneticSkill", magneticSkill); }
    public void MagneticSkillDown() { magneticSkill--; PlayerPrefs.SetInt("magneticSkill", magneticSkill); }

    public void HasteSkillUp() { hasteSkill++; PlayerPrefs.SetInt("hasteSkill", hasteSkill); }
    public void HasteSkillDown() { hasteSkill--; PlayerPrefs.SetInt("hasteSkill", hasteSkill); }

    public int GetkDiggySkill() { return diggySkill; }
    public int GetkMagneticSkill() { return magneticSkill; }
    public int GetHasteSkill() { return hasteSkill; }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        diggySkill = PlayerPrefs.GetInt("diggySkill");
        magneticSkill = PlayerPrefs.GetInt("magneticSkill");
        hasteSkill = PlayerPrefs.GetInt("hasteSkill");
    }
}
