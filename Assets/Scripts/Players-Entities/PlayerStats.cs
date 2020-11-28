using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int currentLevel;
    public int currentEXP;
    public int[] toLevelUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEXP >= toLevelUp[currentLevel])
        {
            currentLevel++;
        }
    }
    public void AddExperience(int expToAdd)
    {
        currentEXP += expToAdd;
    }
}
