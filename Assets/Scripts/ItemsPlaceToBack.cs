using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPlaceToBack : MonoBehaviour
{
    [SerializeField] private GameObject[] ItemsOnBack;

    private void Awake()
    {
        ChangeItemsCountOnBack(0);
    }

    /// <summary>
    /// изменяет видимые предметы на спине
    /// </summary>
    /// <param name="count"></param>
    public void ChangeItemsCountOnBack(int count)
    {
        for(int i =0;i<count;i++)
        {
            ItemsOnBack[i].SetActive(true);
        }
        for(int k = 2;k>=count;k--)
        {
            ItemsOnBack[k].SetActive(false);
        }
    }
}
