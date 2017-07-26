using UnityEngine;
using System.Collections;

public class NextPrevClickListener : InputAdapter {

    public BigAnimals bigAnimals;
    public int buttonIndex;
    public ShopScreen shopScreen;

    public void Start()
    {
        
    }

    public override void OnTouchDown()
    {
        if (InputController.Name != InputNames.SHOP) return;
        base.OnTouchDown();
        gameObject.transform.localScale = new Vector3(0.9f, 0.9f, gameObject.transform.localScale.z);
        SoundManager.playButtonSound();
    }
    public override void OnCheckUp()
    {
        if (InputController.Name != InputNames.SHOP) return;
        base.OnCheckUp();
        gameObject.transform.localScale = new Vector3(1, 1, gameObject.transform.localScale.z);
    }
    public override void OnTouchUp()
    {
        if (InputController.Name != InputNames.SHOP) return;
        base.OnTouchUp();
        if (buttonIndex == 0)//prev
        {
            bigAnimals.setAnimalIndex(prevIndex(Attr.currentAnimal));
        }
        else//next
        {
            bigAnimals.setAnimalIndex(nextIndex(Attr.currentAnimal));
        }
        shopScreen.updateAnimalName();
        shopScreen.updateUI();
    }

    private int prevIndex(int animalIndex)
    {
        int temp = animalIndex;
        temp--;
        if (temp == -1)
            temp = 9;
        return temp;
    }

    private int nextIndex(int animalIndex)
    {
        int temp = animalIndex;
        temp++;
        if (temp == 10)
            temp = 0;
        return temp;
    }
}
