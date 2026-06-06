using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public static class MathUtilities
{
    public static float AverageMean(this int[] array)
    {
        if (array.Length == 0) return 0;
        int sum = 0;
        for (int i = 0; i < array.Length; i++) sum += array[i];
        return (float) sum / array.Length;
    }
    public static float AverageMedian(this int[] array)
    {
        if (array.Length == 0) return 0;
        int[] sortedArray = array.OrderBy(x => x).ToArray();
        if (array.Length % 2 == 0) return (sortedArray[sortedArray.Length / 2] + sortedArray[sortedArray.Length / 2 - 1])/2f; 
        return sortedArray[sortedArray.Length / 2];
    }
    public static int[] AverageMode(this int[] array)
    {
        Dictionary<int, int> keyPairs = new();
        // The first int represents the number, the second int represents how many times you've seen them
        for (int i = 0; i < array.Length; i++)
        {
            if (!keyPairs.TryAdd(array[i], 1))
            {
                keyPairs[array[i]]++;
            }
        }

        List<int> answers = new();
        int currentMax = 0;
        foreach (var kvp in keyPairs)
        {
            if (kvp.Value > currentMax)
            {
                answers.Clear();
                answers.Add(kvp.Key);
                currentMax = kvp.Value;
            } else if (kvp.Value == currentMax)
            {
                answers.Add(kvp.Key);
            }
        }
        return answers.ToArray();
    }

    public static ulong Factorial(int num, int stop = 0)
    {   
        if(num < 0 || stop < 0) throw new ArgumentOutOfRangeException(nameof(num));
        ulong answer = 1;
        while (num > stop)
        {
            answer *= (ulong) num;
            num -= 1;
        }
        return answer;   
    }

    public static ulong Choose(int n, int k)
    {
        
        return Factorial(n, n-k)/Factorial(k);
    }

    public static void Shuffle<TMatthew>(this TMatthew[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (array[k], array[n]) = (array[n], array[k]); 
        }
    }
}