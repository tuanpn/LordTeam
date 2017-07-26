using UnityEngine;
using System.Collections.Generic;

public class GoldAddition : MonoBehaviour {

    private List<GameObject> coins;

    private float duration;
    
    private int numberCoins1;
    private int numberCoins2;

    private float stateTime1;
    private float stateTime2;

    private float destX;
    private float destY;

    public void Start()
    {
        duration = 0.05f;
        coins = new List<GameObject>();
        destX = -2;
        destY = 1.7f;
    }

    //-2, 1.7

    public void addGold1(int gold)
    {
        numberCoins1 += gold;
        /*
        for (int i = 0; i < gold; i++)
        {
            addGold(1.2f, 1.2f);
        }
         * */
    }

    public void addGold2(int gold)
    {
        /*
        for (int i = 0; i < gold; i++)
        {
            addGold(1.2f, 0.6f);
        }
         * */
        numberCoins2 += gold;
    }

    public void Update()
    {
        if (numberCoins1 > 0)
        {
            stateTime1 += Time.deltaTime;
            if (stateTime1 >= duration)
            {
                addGold(1.2f, 1.2f);
                numberCoins1--;
            }
        }

        if (numberCoins2 > 0)
        {
            stateTime2 += Time.deltaTime;
            if (stateTime2 >= duration)
            {
                addGold(1.2f, 0.6f);
                numberCoins2--;
            }
        }
    }

    private void addGold(float x, float y)
    {
        GameObject gObject = (GameObject)Instantiate(Resources.Load<GameObject>("Add/Gold"));
        gObject.transform.parent = gameObject.transform;
        gObject.transform.localPosition = new Vector3(x, y, 0);
        coins.Add(gObject);

        int ra = Random.Range(0, 1);
        if (ra == 0)
        {//Tren
            gObject.AddComponent<Bezier>().setBezier(2f, 
                new Vector2(x, y),
                new Vector2(x, y + Random.Range(0.1f, 1.5f)),
                new Vector2(destX + Random.Range(0.1f, 1.5f), y + Random.Range(0.1f, 1.5f)),
                new Vector2(destX, destY));
        }
        else
        {//Duoi
            gObject.AddComponent<Bezier>().setBezier(2f,
                new Vector2(x, y),
                new Vector2(x, y - Random.Range(0.1f, 1.5f)),
                new Vector2(destX + Random.Range(0.1f, 1.5f), y - Random.Range(0.1f, 1.5f)),
                new Vector2(destX, destY));
        }
        Destroy(gObject, 2);
    }
}
