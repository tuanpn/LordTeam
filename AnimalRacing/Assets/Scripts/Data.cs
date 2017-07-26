using UnityEngine;
using System.Collections;

public class Data  {

    public static void InitData()
    {
        if (!PlayerPrefs.HasKey(KEY_COIN))
        {
            createData();
        }
    }

    private static void createData()
    {
#if UNITY_EDITOR
        PlayerPrefs.SetInt(KEY_COIN, 3000000);//GOLD
        PlayerPrefs.SetInt(KEY_WORLD_MAP, 1);//MAP UNLOCK 1
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt(KEY_LEVEL + i, 15);//LEVEL UNLOCK INSIDE MAP 1
        }
        for (int i = 0; i < 60; i++)
        {
            PlayerPrefs.SetInt(KEY_STAR + i, 3);//STAR 0
        }
#else
        PlayerPrefs.SetInt(KEY_COIN, 3000);//GOLD 3000
         PlayerPrefs.SetInt(KEY_WORLD_MAP, 1);//MAP UNLOCK 1
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt(KEY_LEVEL + i, 1);//LEVEL UNLOCK INSIDE MAP 1
        }
        for (int i = 0; i < 60; i++)
        {
            PlayerPrefs.SetInt(KEY_STAR + i, 0);//STAR 0
        }
#endif       
        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 10; j++)
            {
                PlayerPrefs.SetInt(KEY_UPGRADE + i + "" + j, 1);
            }
        PlayerPrefs.SetInt(KEY_ANIMAL_UNLOCK + "0", 1);
        for (int i = 1; i < 10; i ++ )
            PlayerPrefs.SetInt(KEY_ANIMAL_UNLOCK + i, 0);
        PlayerPrefs.SetInt(KEY_GUIDE, 0);

        PlayerPrefs.Save();
    }

    public static int getData(string key)
    {
        return PlayerPrefs.GetInt(key, 0);
    }

    public static void saveData(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public static void Save()
    {
        PlayerPrefs.Save();
    }

    public const string KEY_COIN = "key_coin";
    public const string KEY_WORLD_MAP = "key_world_map";
    public const string KEY_LEVEL = "key_level";
    public const string KEY_STAR = "key_star";
    public const string KEY_ANIMAL_UNLOCK = "key_animal_unlock";
    public const string KEY_GUIDE = "key_guide";
    public const string KEY_UPGRADE = "key_upgrade";

}
