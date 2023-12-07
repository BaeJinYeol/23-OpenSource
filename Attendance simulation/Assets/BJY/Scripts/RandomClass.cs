using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ClassRoom
{
    class1 = 101,
    class2 = 102,
    class3 = 103,
    class4 = 104,
    class5 = 111,
    class6 = 112,
    class7 = 113,
    class8 = 114,
    class11 = 201,
    class12 = 202,
    class13 = 203,
    class14 = 204,
    class15 = 211,
    class16 = 212,
    class17 = 213,
    class18 = 214,
}

public class RandomClass : MonoBehaviour
{
    public ClassRoom randomClass;
    [SerializeField]
    private TMP_Text roomClassText;

    private void Start()
    {
        randomClass = GetRandomEnumValue<ClassRoom>();
        roomClassText.text = randomClass.GetHashCode().ToString();
        GameManager.Instance.SetGoalClass(randomClass);
    }

    T GetRandomEnumValue<T>()
    {
        Array enumValues = Enum.GetValues(typeof(T));
        return (T)enumValues.GetValue(UnityEngine.Random.Range(0, enumValues.Length));
    }
}
