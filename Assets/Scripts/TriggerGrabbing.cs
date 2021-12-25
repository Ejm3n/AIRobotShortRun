using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGrabbing : MonoBehaviour
{
    /// <summary>
    /// �������� �������� � ���� ���� �� ������� � ����
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bot"))
        {
            other.GetComponent<BotController>().StartGrabbing(transform);
        }
    }
}
