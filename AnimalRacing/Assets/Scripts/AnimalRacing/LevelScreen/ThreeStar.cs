using UnityEngine;
using System.Collections;

public class ThreeStar : MonoBehaviour {

    public GameObject[] stars;

    public void setSprite(int star, Sprite starSprite, bool isActive)
    {
        switch (star)
        {
            case 0:
                break;
            case 1:
                stars[0].GetComponent<SpriteRenderer>().sprite = starSprite;
                AddRotaionAction(stars[0]);
                break;
            case 2:
                stars[0].GetComponent<SpriteRenderer>().sprite = starSprite;
                stars[1].GetComponent<SpriteRenderer>().sprite = starSprite;
                AddRotaionAction(stars[0]);
                AddRotaionAction(stars[1]);
                break;
            case 3:
                stars[0].GetComponent<SpriteRenderer>().sprite = starSprite;
                stars[1].GetComponent<SpriteRenderer>().sprite = starSprite;
                stars[2].GetComponent<SpriteRenderer>().sprite = starSprite;
                AddRotaionAction(stars[0]);
                AddRotaionAction(stars[1]);
                AddRotaionAction(stars[2]);
                break;
        }
        if (!isActive)
        {
            gameObject.SetActive(false);
        }
    }

    private void AddRotaionAction(GameObject gObject)
    {
        gObject.AddComponent<Actor>().addAction(new ActionRotateBy(720, 1, Interpolation.sine));
    }

}
