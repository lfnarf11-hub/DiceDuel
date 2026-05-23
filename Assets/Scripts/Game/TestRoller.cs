using Given.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestRoller : MonoBehaviour
{
    [SerializeField] private EDiceType[] diceToRoll;
    [SerializeField] private int numRolls;
    
    //Recommended to either use start or context menu, not both.
    [ContextMenu("Run Experiment With Current Settings")]
    void CommitExperimentWithCurrentSettings()
    {
        RunExperiment(diceToRoll, numRolls);   
    }

    //This function is lazy, it focuses not on optimization, but on testing.
    void RunExperiment(EDiceType[] dice, int rolls)
    {
        int low = dice.Length; //Assume all have a low of 1.
        int high = 0;
        
        //Add the highs of each dice type
        foreach (EDiceType diceType in dice)
        {
            high += DataManager.DiceValues[diceType].High;
        }
        
        int [] data = new int[rolls];
        int [] rollCount = new int[high - low + 1]; //Add one because we need to include the max. 1,2,3,4,5,6. (6-1+1 --> 6)
        for (int i = 0; i < rolls; i++)
        {
            data[i] = 0;
            foreach (EDiceType currentDice in dice)
            {
                data[i] += Random.Range(1, DataManager.DiceValues[currentDice].High + 1);
            }
            rollCount[data[i] - low] += 1; // data[i] is the number we rolled, then we need to remove low to find the correct cell. 1-1 ==> 5
        }


        for (int i = 0; i < rollCount.Length; ++i)
        {
            Debug.Log("We rolled "+(i+low)+": " + ((float)rollCount[i] / rolls * 100) +"% of the time");
        }
        
                
        Debug.Log("Mean: " + MathUtilities.AverageMean(data));
        Debug.Log("Median: " + MathUtilities.AverageMedian(data));

        string mode = "";
        foreach (int modeValue in MathUtilities.AverageMode(data))
        {
            mode += modeValue + ", ";
        }
        Debug.Log("Mode: " + mode);
    }
}