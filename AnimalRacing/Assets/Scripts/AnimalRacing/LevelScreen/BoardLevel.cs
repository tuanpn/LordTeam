using UnityEngine;
using System.Collections.Generic;

public class BoardLevel : MonoBehaviour {

    public BitmapFont font;
    public GameObject prefabs;
    public List<Sprite> bgSprites;

    private string[] heads;
    private string[] hs;

    public void Awake()
    {
        font = new BitmapFont("Fonts/font_banchoi", "Fonts/font_banchoi_xml", gameObject);
    }

	public void Start () {
        int levelUnlock = Data.getData(Data.KEY_LEVEL + Attr.currentWorld);

        createHeadTexts(Attr.currentWorld);

        float[] lxs = new float[] { -3, -1.5f, 0, 1.5f, 3, -3, -1.5f, 0, 1.5f, 3, -3, -1.5f, 0, 1.5f, 3 };
        float[] lys = new float[] { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, -1, -1, -1, -1, -1 };
        for (int i = 0; i < 15; i++)
        {
            GameObject levelObject = (GameObject)Instantiate(prefabs);
            levelObject.transform.parent = gameObject.transform;
            levelObject.transform.localPosition = new Vector3(lxs[i], lys[i], -1);
            FontLevel fontLevel = levelObject.GetComponent<FontLevel>();
            fontLevel.setBoardLevel(this);

            //fontLevel.setText("" + (i + 1));
            fontLevel.setText(i < levelUnlock ? heads[i] : hs[i]);

            fontLevel.setStar(Data.getData(Data.KEY_STAR + (Attr.currentWorld * 15 + i)), i < levelUnlock);

            if (i < levelUnlock)
            {
                fontLevel.bgObject.GetComponent<SpriteRenderer>().sprite = bgSprites[Attr.currentWorld + 1];

                if (i == levelUnlock - 1)
                {
                    levelObject.AddComponent<Actor>().addAction(new ActionRepeat(ActionRepeat.FOREVER, new ActionSequence(
                        new ActionScaleTo(0.95f, 0.95f, 0.2f, Interpolation.sine),
                        new ActionScaleTo(1, 1, 0.2f, Interpolation.sine)
                        )));
                     
                }
                 
                addClickListener(fontLevel.bgObject, i);
            }
            else
                fontLevel.bgObject.GetComponent<SpriteRenderer>().sprite = bgSprites[0];
        }

        gameObject.AddComponent<Actor>().addAction(new ActionSequence(
            new ActionScaleTo(0, 0, 0),
            new ActionScaleTo(1, 1, 0.5f, Interpolation.swingOut)));
	}

    private void addClickListener(GameObject bgObject, int levelIndex)
    {
        bgObject.AddComponent<InputProcessor>();
        LevelClickListener clickListener = bgObject.AddComponent<LevelClickListener>();
        clickListener.levelIndex = levelIndex;
        clickListener.boardLevel = gameObject;
    }
	
	public void Update () {
	    
	}

    public BitmapFont getFont()
    {
        return font;
    }

    private void createHeadTexts(int world_index)
    {
        switch (world_index)
        {
            case 0:
                heads = new string[] { "1", "q", "3", "4", "5", "w", "7", "8", "9", "10", "e", "12", "13", "14", "15" };
                hs = new string[] { "1", "a", "3", "4", "5", "s", "7", "8", "9", "10", "d", "12", "13", "14", "15" };
                break;
            case 1:
                heads = new string[] { "1", "2", "3", "4", "5", "r", "7", "8", "9", "10", "t", "12", "13", "14", "15" };
                hs = new string[] { "1", "2", "3", "4", "5", "f", "7", "8", "9", "10", "g", "12", "13", "14", "15" };
                break;
            case 2:
                heads = new string[] { "1", "2", "3", "4", "5", "y", "7", "8", "9", "10", "u", "12", "13", "14", "15" };
                hs = new string[] { "1", "2", "3", "4", "5", "h", "7", "8", "9", "10", "j", "12", "13", "14", "15" };
                break;
            case 3:
                heads = new string[] { "1", "2", "3", "4", "5", "i", "7", "8", "9", "10", "o", "12", "13", "14", "15" };
                hs = new string[] { "1", "2", "3", "4", "5", "k", "7", "8", "9", "10", "l", "12", "13", "14", "15" };
                break;
        }
    }
}
