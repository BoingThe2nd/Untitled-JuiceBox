using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    public static Farm TheFarm;
    public enum HealthMode {Highest, Equal };
    public static HealthMode TheHealthMode = HealthMode.Highest;
    public float damageTakeDelay;
    Queue<int> damageQueue = new Queue<int>();
    bool dealingDamage;
    public int[] FruitCount = new int[4];
    //Apple//Orange//Grape//Melon?
    //Total number of each fruit 
    public int[] FruitFarms = new int[4];
    int trackedEqual;
    //The amount of fruit that is generated per round for each fruit
    // Start is called before the first frame update
    void Start()
    {
        TheFarm = this;
    }

    public void Update()
    {
        if (damageQueue.Count > 0 && !dealingDamage)
        {
            StartCoroutine(TakeDamageWithDelay(damageQueue.Dequeue()));
        }
    }

    public void GenerateFarmFruits()
    {
        for (int i = 0; i < FruitCount.Length; i++)
        {
            FruitCount[i] += FruitFarms[i];
        }
    }

    public void TakeDamage(int damageToTake)
    {
        damageQueue.Enqueue(damageToTake);
       
        //for (int i = 0; i < damageToTake; i++)
        //{
        //    FruitCount[FarmToTakeFrom()] -= 1;
        //}
    }


    IEnumerator TakeDamageWithDelay(int damageToTake)
    {
        dealingDamage = true;
        for (int i = 0; i < damageToTake; i++)
        {
            FruitCount[FarmToTakeFrom()] -= 1;

            yield return new WaitForSeconds(damageTakeDelay);
        }
        dealingDamage = false;
    }

    public int FarmToTakeFrom()
    {
        switch (TheHealthMode)
        {
            case HealthMode.Highest:
                int currentHighestHealth = -1;
                int HighestFarm = -1;
                for (int i = 0; i < FruitFarms.Length; i++)
                {
                    if (FruitCount[i] > currentHighestHealth)
                    {
                        currentHighestHealth = FruitCount[i];
                        HighestFarm = i;
                    }
                }
                return HighestFarm;
            case HealthMode.Equal:
                if (FruitCount[trackedEqual] > 0)
                {
                    int newTrack = trackedEqual;
                    trackedEqual += 1;
                    return newTrack;
                }
                else
                {
                    for (int i = 0; i < FruitFarms.Length; i++)
                    {
                        trackedEqual += 1;
                        if (trackedEqual >= FruitFarms.Length)
                        {
                            trackedEqual = 0;
                        }
                        if (FruitCount[trackedEqual] > 0)
                        {
                            int newTrack = trackedEqual;
                            trackedEqual += 1;
                            return newTrack;
                        }
                    }
                }
                break;
            default:

                break;
        }
        return -1;
    }




}
