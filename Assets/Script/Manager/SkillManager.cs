using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    static public SkillManager instance;

    int diggySkill = 0;
    int magneticSkill = 0;
    int hasteSkill = 0;

    public void DiggySkillUp() { diggySkill++; }
    public void DiggySkillDown() { diggySkill--; }

    public void MagneticSkillUp() { magneticSkill++; }
    public void MagneticSkillDown() { magneticSkill--; }

    public void HasteSkillUp() { hasteSkill++; }
    public void HasteSkillDown() { hasteSkill--; }

    public int GetkDiggySkill() { return diggySkill; }
    public int GetkMagneticSkill() { return magneticSkill; }
    public int GetHasteSkill() { return hasteSkill; }

    void Awake()
    {
        instance = this;
    }
}
