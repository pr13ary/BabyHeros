using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    

    [SerializeField]
    UI2DSprite m_iconSprite;
    [SerializeField]
    UILabel m_funcLabel; // 기능 설명
    [SerializeField]
    GameObject[] m_skillPrefabs;

    Dictionary<SkillType, SkillData> m_skillTable = new Dictionary<SkillType, SkillData>();

    public void SetSkill(SkillType type)
    {
        m_iconSprite = m_skillTable[type].iconSprite;
        m_funcLabel = m_skillTable[type].funcLabel;
    }
    void SetSkillData()
    {
        m_skillPrefabs = Resources.LoadAll<GameObject>("Prefabs/Skills");

        for(int i=0; i<m_skillPrefabs.Length; i++)
        {
            var prefab = m_skillPrefabs[i];
            SkillType type = (SkillType)(int.Parse(prefab.name.Split('.')[0]) - 1);
            var sprite = prefab.transform.Find("Icon").GetComponent<UI2DSprite>();
            var label = prefab.transform.Find("Label").GetComponent<UILabel>();
            SkillData skill = new SkillData() { iconSprite = sprite, funcLabel = label };
            m_skillTable.Add(type, skill);
            print("dd");
        }
    }
    void Awake()
    {
        SetSkillData();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_iconSprite = GetComponent<UI2DSprite>();
        m_funcLabel = GetComponent<UILabel>();
    }
}
