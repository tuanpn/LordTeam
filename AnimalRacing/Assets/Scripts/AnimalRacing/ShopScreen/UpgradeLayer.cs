using UnityEngine;
using System.Collections;

public class UpgradeLayer : MonoBehaviour {

    public Label currentLevelLabel;
    public Label nextLevelLabel;
    public Label goldLabel;
    public Label maxLevelLabel;

    public GameObject nextLevel;
    public GameObject boardGold;
    public GameObject upgradeButton;

    public GameObject chooseSkill;

    public GameObject[] levelIconItems;

    public Sprite[] levelIcons;

    public ShopScreen shopScreen;

    private UpgradeInfo upgradeInfo;

    private string[] itemNames;

    private int itemIndex;

	public void Start () {
	        
	}
	
	void Update () {
	    
	}

    public void setFont(BitmapFont fontShop)
    {
        currentLevelLabel.setFont(fontShop);
        nextLevelLabel.setFont(fontShop);
        maxLevelLabel.setFont(fontShop);
        goldLabel.setFont(fontShop);

        maxLevelLabel.setText("MAX LEVEL", 0, 15);
        itemNames = new string[] {"Speed","Jump" };
    }

    public void setUpgradeInfo(UpgradeInfo upgradeInfo)
    {
        this.upgradeInfo = upgradeInfo;
    }

    
    public void UpdateUI(int itemIndex)
    {
        if (upgradeInfo == null) return;
        this.itemIndex = itemIndex;
        if (upgradeInfo.getLevel(Attr.currentAnimal, itemIndex) < 5)
        {
            currentLevelLabel.gameObject.SetActive(true);
            nextLevelLabel.gameObject.SetActive(true);
            goldLabel.gameObject.SetActive(true);
            nextLevel.SetActive(true);
            maxLevelLabel.gameObject.SetActive(false);
            boardGold.SetActive(true);
            upgradeButton.SetActive(true);

            currentLevelLabel.setText(itemNames[itemIndex] + " Lv" + upgradeInfo.getLevel(Attr.currentAnimal, itemIndex) + " : " + upgradeInfo.getItem(Attr.currentAnimal, itemIndex, false), 0, 12);
            nextLevelLabel.setText(itemNames[itemIndex] + " Lv" + (upgradeInfo.getLevel(Attr.currentAnimal, itemIndex) + 1) + " : " + upgradeInfo.getItem(Attr.currentAnimal, itemIndex, true), 0, 12);
            goldLabel.setText("" + upgradeInfo.getCostItem(Attr.currentAnimal, itemIndex), 0, 0);

        }
        else
        {
            currentLevelLabel.gameObject.SetActive(false);
            nextLevelLabel.gameObject.SetActive(false);
            goldLabel.gameObject.SetActive(false);
            nextLevel.SetActive(false);
            maxLevelLabel.gameObject.SetActive(true);
            boardGold.SetActive(false);
            upgradeButton.SetActive(false);
        }

        levelIconItems[itemIndex].GetComponent<SpriteRenderer>().sprite = levelIcons[upgradeInfo.getLevel(Attr.currentAnimal, itemIndex) - 1];
    }

    public void upgrade()
    {
        if (upgradeInfo.getCostItem(Attr.currentAnimal, itemIndex) > Data.getData(Data.KEY_COIN))
        {
            shopScreen.showNotEnoughCoin();
        }
        else
        {
            Data.saveData(Data.KEY_COIN, Data.getData(Data.KEY_COIN) - upgradeInfo.getCostItem(Attr.currentAnimal, itemIndex));
            upgradeInfo.upgrade(Attr.currentAnimal, itemIndex);
            UpdateUI(itemIndex);
            shopScreen.UpdateCoin();
        }
    }

    public void resetChooseItem()
    {
        chooseSkill.transform.localPosition = new Vector3(1, 0.66f, chooseSkill.transform.localPosition.z);
    }
}
