using System;
using UnityEngine;

public class AnimalProperties {

    public readonly float speedX;
    public readonly float speedY;
    public readonly float speedXBooster;
    public readonly float speedYBooster;
    public readonly float speedXSprings;
    public readonly float speedYSprings;

    public AnimalProperties(float speedX_decimal, float speedY_decimal)
    {
        this.speedX = speedX_decimal / 50;
        this.speedY = speedY_decimal / 50;
        this.speedXBooster = speedX + 3;
        this.speedYBooster = speedY + 1;
        this.speedXSprings = speedX + 4;
        this.speedYSprings = speedY + 2;
    }

    public void printInfo()
    {
        Debug.Log("SpeedX=" + speedX + ", speedY=" + speedY + ", SpeedXBooster=" + speedXBooster + ", SpeedYBooster=" + speedYBooster + ", SpeedXSprings=" + speedXSprings + ", SpeedYSprings=" + speedYSprings );
    }

}
