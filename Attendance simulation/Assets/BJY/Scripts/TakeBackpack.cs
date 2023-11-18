using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeBackpack : MonoBehaviour
{
    private void OnDestroy()
    {
        GameManager.Instance.TakeBackPack();
    }
}
