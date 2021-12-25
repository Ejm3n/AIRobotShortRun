using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������������� ������ ��� �������� ���������� �� ���������
/// </summary>
public class BotAnimationController : MonoBehaviour
{

    private BotController _bc;

    private void Awake()
    {
        _bc = FindObjectOfType<BotController>();
    }

    /// <summary>
    /// � ����� �������� looking ����������� ����� � �������� ������� � ����
    /// </summary>
    void EndLooking()
    {
        _bc.EndLooking();
    }

    /// <summary>
    /// � ������ �������� looking ����������� ����� � �������� ������� � ����
    /// </summary>
    void StartLooking()
    {
        _bc.StartLooking();
    }

    /// <summary>
    /// � ����� �������� picking ����������� ����� � �������� ������� � ����
    /// </summary>
    void FinishedPicking()
    {
        _bc.FinishedGrabbining();
    }
}
