using UnityEngine;
using System.Collections.Generic;

public class PickLayer : MonoBehaviour {

    public BigAnimals bigAnimals;

    public List<Sprite> skillSprites;
    public List<Sprite> skillSpriteLocks;
    public Transform[] skillTransforms;
    public Descriptions descriptions;

    public CirclesChoosed circleChoosed;

    private SkillDescription skillDescription;

	public void Start () {
        bigAnimals.setAnimalIndex(Attr.currentAnimal);
        int[] unlockStars = new int[] { 0, 0, 35, 135, 15, 55, 75, 95 };
        //int[] unlockStars = new int[] { 0, 0, 0, 0, 0, 0, 0, 0};
        
        int totalStar = 0;
        for (int i = 0; i < 60; i++)
            totalStar += Data.getData(Data.KEY_STAR + i);

        skillDescription = new SkillDescription();


        for (int i = 0; i < 8; i++)
        {
            if (totalStar >= unlockStars[i])
            {
                GameObject skillObject = new GameObject("Skill");
                skillObject.transform.parent = gameObject.transform;
                skillObject.AddComponent<SpriteRenderer>().sprite = skillSprites[i];
                skillObject.transform.localPosition = new Vector3(skillTransforms[i].localPosition.x, skillTransforms[i].localPosition.y + 0.005f, -1);
                addSkillClickListener(skillObject, i, skillDescription, descriptions, -1);
                //la cac skill unlock
            }
            else
            {
                GameObject skillObject = new GameObject("Skill");
                skillObject.transform.parent = gameObject.transform;
                skillObject.AddComponent<SpriteRenderer>().sprite = skillSpriteLocks[i];
                skillObject.transform.localPosition = new Vector3(skillTransforms[i].localPosition.x, skillTransforms[i].localPosition.y + 0.005f, -1);
                addSkillClickListener(skillObject, i, skillDescription, descriptions, unlockStars[i]);
            }
        }
	}
	
	public void updateAnimalIndex () {
        bigAnimals.setAnimalIndex(Attr.currentAnimal);
	}

    private void addSkillClickListener(GameObject skillObject, int skillIndex, SkillDescription skillDes, Descriptions descriptions, int starUnlock)
    {
        skillObject.AddComponent<InputProcessor>();
        SkillClickListener skillClickListemer = skillObject.AddComponent<SkillClickListener>();
        skillClickListemer.skillIndex = skillIndex;
        skillClickListemer.skillDescription = skillDes;
        skillClickListemer.descriptions = descriptions;
        skillClickListemer.starUnlock = starUnlock;
        skillClickListemer.circleChoosed = circleChoosed;
    }
}
