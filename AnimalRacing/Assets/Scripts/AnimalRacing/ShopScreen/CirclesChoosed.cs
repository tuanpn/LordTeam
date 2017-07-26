using UnityEngine;
using System.Collections;

public class CirclesChoosed : MonoBehaviour {

    public Transform[] circles;

    private int[] skills;

    public void Start()
    {
        skills = new int[] {-1, -1, -1};

    }

    public void choosedSkill(GameObject skillObject, int skillIndex, int starUnlock)
    {
        if (starUnlock > -1)
            return;
        SkillClickListener skillClick = skillObject.GetComponent<SkillClickListener>();
        if (skillClick.picked)//Neu da chon roi
        {
            skillClick.picked = false;
            skillClick.transform.localPosition = skillClick.oldPosition;
            skills[skillClick.cirlceIndex] = -1;
        }
        else//Neu chua chon
        {
            for (int i = 0; i < 3; i++)
            {
                if (skills[i] == -1)
                {
                    skillObject.transform.localPosition = new Vector3(circles[i].localPosition.x, circles[i].localPosition.y + 0.005f, skillObject.transform.localPosition.z);
                    skills[i] = skillIndex;
                    skillClick.picked = true;
                    skillClick.cirlceIndex = i;
                    break;
                }
            }
        }
    }

    public int[] getSkillChoosed()
    {
        return skills;
    }
    
}
