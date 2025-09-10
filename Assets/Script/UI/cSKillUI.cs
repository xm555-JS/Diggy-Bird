using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cSKillUI : MonoBehaviour
{
    enum Skill { DIGGY, MAGETIC, HASTE }
    [SerializeField] Skill skill;
    [SerializeField] Image skillCountImg;

    Button btn;
    Text countText;
    
    int skillCount = 0;

    void Start()
    {
        btn = GetComponent<Button>();
        countText = skillCountImg.GetComponentInChildren<Text>();

        switch (skill)
        {
            case Skill.DIGGY:
                skillCount = SkillManager.instance.GetkDiggySkill();
                break;
            case Skill.MAGETIC:
                skillCount = SkillManager.instance.GetkMagneticSkill();
                break;
            case Skill.HASTE:
                skillCount = SkillManager.instance.GetHasteSkill();
                break;
        }
    }

    void Update()
    {
        switch (skill)
        {
            case Skill.DIGGY:
                skillCount = SkillManager.instance.GetkDiggySkill();
                break;
            case Skill.MAGETIC:
                skillCount = SkillManager.instance.GetkMagneticSkill();
                break;
            case Skill.HASTE:
                skillCount = SkillManager.instance.GetHasteSkill();
                break;
        }

        if (skillCount <= 0)
        {
            btn.interactable = false;
            skillCountImg.enabled = false;
        }
        else
        {
            btn.interactable = true;
            skillCountImg.enabled = true;
        }
    }

    void LateUpdate()
    {
        countText.text = skillCount.ToString();
    }
}
