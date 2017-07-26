using UnityEngine;
using System.Collections;
using Dialog;

public class ShopScreen : InputAdapter {

    public GameObject bgObject;
    public BitmapFont nameFont;

    public AnimalNameShop animalName;
    public GameObject lockObject;
    public GameObject nextButton;

    private string[] animalNames;
    //private int animalUnlock;

    public PickLayer pickLayer;

    private BitmapFont shopFont;
    public Label starLabel;
    public Label goldLabel;

    public UpgradeLayer upgradeLayer;
    public BuyLayer buyLayer;

    private int[] starCosts;
    private int[] goldCosts;

    private UpgradeInfo upgradeInfo;

    private int totalStar;

    public GameObject dialog;

    public void Awake()
    {
        nameFont = new BitmapFont("Fonts/bitmapfont1", "Fonts/bitmapfont1_xml", gameObject);
        shopFont = new BitmapFont("Fonts/shop_font","Fonts/shop_font_xml", null);
        starCosts = new int[] {0, 15, 25, 35, 45, 60, 75, 90, 115, 135 };
        goldCosts = new int[] {0, 3000, 5000, 7000, 9000, 12000, 15000, 18000, 22000, 26000};

        upgradeInfo = new UpgradeInfo();
    }

    public void Start()
    {
        InputController.Name = InputNames.SHOP;

        bgObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/bg" + (Attr.currentWorld + 1));

        animalNames = new string[] { "hedgehog", "monkey", "pig", "fox", "giraffe", "panda", "rhino", "tiger", "elephant", "lion" };
        animalName.setFont(nameFont);
        //animalUnlock = Data.getData(Data.KEY_ANIMAL_UNLOCK);

        updateAnimalName();

        starLabel.setFont(shopFont);
        totalStar = 0;
        for (int i = 0; i < 60; i++)
        {
            totalStar += Data.getData(Data.KEY_STAR + i);
        }
        starLabel.setText("" + totalStar, 0, 0);
        goldLabel.setFont(shopFont);
        UpdateCoin();

        upgradeLayer.setFont(shopFont);
        upgradeLayer.setUpgradeInfo(upgradeInfo);
        upgradeLayer.UpdateUI(1);
        upgradeLayer.UpdateUI(0);

        buyLayer.setFont(shopFont);
        updateUI();

        {
            dialog = (GameObject)Instantiate(dialog);
            DialogUnity dialogUnity = dialog.GetComponent<DialogUnity>();
            //dialogUnity.setText("Do you want to play", "game without skills?");
            dialogUnity.setDialogOne(delegate() {
                InputController.Name = InputNames.SHOP;
            });
        }

        ARController.setBannerVisible(false);
        ARController.showInterstitialAd();
    }

    public void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            if (InputController.Name == InputNames.DIALOG)
            {
                if (dialog != null)
                {
                    dialog.GetComponent<DialogUnity>().hideDialog();
                    InputController.Name = InputNames.SHOP;
                }
            }
        }
    }

    public void updateAnimalName()
    {
        animalName.setName(animalNames[Attr.currentAnimal].ToUpper());
        if (Data.getData(Data.KEY_ANIMAL_UNLOCK + Attr.currentAnimal) == 1)
        {
            lockObject.SetActive(false);
            nextButton.SetActive(true);
            upgradeLayer.gameObject.SetActive(true);

            upgradeLayer.UpdateUI(1);
            upgradeLayer.UpdateUI(0);

            buyLayer.gameObject.SetActive(false);
        }
        else
        {
            lockObject.SetActive(true);
            nextButton.SetActive(false);
            upgradeLayer.gameObject.SetActive(false);
            buyLayer.gameObject.SetActive(true);
        }
        
        pickLayer.updateAnimalIndex();
    }

    public void updateUI()
    {
        upgradeLayer.resetChooseItem();
        buyLayer.setParams(upgradeInfo.getSpeed(Attr.currentAnimal), upgradeInfo.getJump(Attr.currentAnimal),
            starCosts[Attr.currentAnimal], goldCosts[Attr.currentAnimal]);

       

    }

    public void UpdateCoin()
    {
        goldLabel.setText(Data.getData(Data.KEY_COIN) + "", 0, 0);
    }

    public void buyAnimal2()//test
    { 
        int gold = Data.getData(Data.KEY_COIN);
        Data.saveData(Data.KEY_COIN, gold - goldCosts[Attr.currentAnimal]);
        Data.saveData(Data.KEY_ANIMAL_UNLOCK + Attr.currentAnimal, 1);
        updateAnimalName();
        updateUI();
        upgradeLayer.resetChooseItem();
    }

    public void buyAnimal()
    {
        int gold = Data.getData(Data.KEY_COIN);
        if (starCosts[Attr.currentAnimal] <= totalStar && goldCosts[Attr.currentAnimal] <= gold)
        {
            Data.saveData(Data.KEY_COIN, gold - goldCosts[Attr.currentAnimal]);
            Data.saveData(Data.KEY_ANIMAL_UNLOCK + Attr.currentAnimal, 1);
            updateAnimalName();
            updateUI();
            upgradeLayer.resetChooseItem();
        }
        else
        {
            if (goldCosts[Attr.currentAnimal] > gold)
            {
                dialog.GetComponent<DialogUnity>().setText("Not enough golds!");
                dialog.GetComponent<DialogUnity>().showDialog();
                InputController.Name = InputNames.DIALOG;
            }
            else if (starCosts[Attr.currentAnimal] > totalStar)
            {
                dialog.GetComponent<DialogUnity>().setText("Not enough stars!");
                dialog.GetComponent<DialogUnity>().showDialog();
                InputController.Name = InputNames.DIALOG;
            }
        }
        UpdateCoin();
    }

    public void showNotEnoughCoin()
    {
        dialog.GetComponent<DialogUnity>().setText("Not enough golds!");
        dialog.GetComponent<DialogUnity>().showDialog();
        InputController.Name = InputNames.DIALOG;
    }
}
