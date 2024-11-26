using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    UI2DSprite m_iconSprite;
    UILabel m_funcLabel; // 기능 설명
    
    public void UseSkill()
    {
        Enum.TryParse(gameObject.name.Split('_')[1], true, out SkillType skilltype);
        SkillType type = skilltype;
        EnemyManager.Instance.m_player.ApplySkill(type);
    }
    public void SetSkill(SkillType type)
    {
        SkillInfo skillInfo = SkillData.GetSkillData(type);
        gameObject.name = "Skill_" + type;
        m_iconSprite.sprite2D = skillInfo.iconSprite;
        m_funcLabel.text = skillInfo.funcLabel;
    }
    void Awake()
    {
        m_iconSprite = GetComponentInChildren<UI2DSprite>();
        m_funcLabel = GetComponentInChildren<UILabel>();
    }
}
