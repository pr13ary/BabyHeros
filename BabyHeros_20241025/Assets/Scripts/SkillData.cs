using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Blue,
    Green,
    Red,
    Magnet,
    Max
}

public struct SkillInfo
{
    public Sprite iconSprite;
    public string funcLabel;
}

public class SkillData : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_skillPrefabs;

    static Dictionary<SkillType, SkillInfo> m_skillTable = new Dictionary<SkillType, SkillInfo>();

    public static SkillInfo GetSkillData(SkillType type)
    {
        return m_skillTable[type];
    }
    void SetSkillData()
    {
        m_skillPrefabs = Resources.LoadAll<GameObject>("Prefabs/Skills");

        for (int i = 0; i < m_skillPrefabs.Length; i++)
        {
            var prefab = m_skillPrefabs[i];
            SkillType type = (SkillType)(int.Parse(prefab.name.Split('.')[0]) - 1);
            var sprite = prefab.transform.Find("Icon").GetComponent<UI2DSprite>().sprite2D;
            var label = prefab.transform.Find("Label").GetComponent<UILabel>().text;
            SkillInfo skill = new SkillInfo() { iconSprite = sprite, funcLabel = label };
            m_skillTable.Add(type, skill);
        }
    }
    void Awake()
    {
        SetSkillData();
    }
}
