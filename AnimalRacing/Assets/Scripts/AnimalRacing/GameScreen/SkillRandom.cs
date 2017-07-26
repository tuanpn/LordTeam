using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class SkillRandom : MonoBehaviour
    {
        public SkillType skillType;

        public static int getSkillIndex(SkillType skillType)
        {
            int index = 0;
            switch (skillType)
            {
                case SkillType.LUA:
                    index = 0;
                    break;
                case SkillType.DONGBANG:
                    index = 1;
                    break;
                case SkillType.THIENTHACH:
                    index = 2;
                    break;
                case SkillType.SET:
                    index = 3;
                    break;
                case SkillType.BOM:
                    index = 4;
                    break;
                case SkillType.DOICHO:
                    index = 5;
                    break;
                case SkillType.BAOVE:
                    index = 6;
                    break;
                case SkillType.TANGTOC:
                    index = 7;
                    break;
            }
            return index;
        }

        public static SkillType getSkillType(int skillIndex)
        {
            SkillType type = SkillType.LUA;
            switch (skillIndex)
            {
                case 0:
                    type = SkillType.LUA;
                    break;
                case 1:
                    type = SkillType.DONGBANG;
                    break;
                case 2:
                    type = SkillType.THIENTHACH;
                    break;
                case 3:
                    type = SkillType.SET;
                    break;
                case 4:
                    type = SkillType.BOM;
                    break;
                case 5:
                    type = SkillType.DOICHO;
                    break;
                case 6:
                    type = SkillType.BAOVE;
                    break;
                case 7:
                    type = SkillType.TANGTOC;
                    break;
            }
            return type;
        }
    }

    public enum SkillType
    {
        LUA, DONGBANG,THIENTHACH,SET,BOM,DOICHO,BAOVE,TANGTOC
    }
}