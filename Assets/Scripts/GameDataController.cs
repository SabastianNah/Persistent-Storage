using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public class GameDataController
{
    public int playerScore;
    public float[] playerPosition;
    public float[] doorPosition;
    public GameDataController(PlayerController player)
    {
        //save update score
        playerScore = player.score;
        // save update position
        playerPosition = new float[3];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;
    }
}
