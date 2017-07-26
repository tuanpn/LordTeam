using UnityEngine;
using System.Collections;

public class UpgradeInfo{

    private int[,] upgrades;
    private int[,] costCoinBases;

    private int[] speeds;
    private int[] jumps;

    public UpgradeInfo()
    {
        upgrades = new int[2,10];
        costCoinBases = new int[2, 10];
        for(int i = 0 ; i < 2 ; i ++)
            for (int j = 0; j < 10; j++)
            {
                upgrades[i, j] = Data.getData(Data.KEY_UPGRADE + i + "" + j);
                costCoinBases[i, j] = 500;
            }

        speeds = new int[] { 150, 160, 170, 180, 190, 200, 210, 220, 230, 240 };//so voi 3
        jumps = new int[] { 250, 260, 270, 280, 290, 300, 310, 320, 330, 340 };//so voi 5
    }

    public void upgrade(int animalIndex, int item)
    {
        if (upgrades[item, animalIndex] < 5)
        {
            upgrades[item, animalIndex]++;
            Data.saveData(Data.KEY_UPGRADE + item + "" + animalIndex, upgrades[item,animalIndex]);
        }
    }

    //get speed or jump of current or next
    public int getItem(int animalIndex, int item, bool next)
    {
        switch (item)
        {
            case 0:
                return speeds[animalIndex] + (upgrades[item, animalIndex] - (next ? 0 : 1)) * 10;
            case 1:
                return jumps[animalIndex] + (upgrades[item, animalIndex] - (next ? 0 : 1)) * 10;
        }
        return -1;
    }

    public int getLevel(int animalIndex, int item)
    {
        return upgrades[item, animalIndex];
    }

    public int getCostItem(int animalIndex, int item)
    {
        return costCoinBases[item, animalIndex] * upgrades[item, animalIndex];
    }

    public int getSpeed(int animalIndex)
    {
        return speeds[animalIndex];
    }

    public int getJump(int animalIndex)
    {
        return jumps[animalIndex];
    }
}
