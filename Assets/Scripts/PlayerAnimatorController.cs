using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������������� ������ ��� �������� ���������� �� ���������
/// </summary>
public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerController _pc;

    private void Awake()
    {
        _pc = GetComponentInParent<PlayerController>();       
    }

    /// <summary>
    /// ������ �������� � ������ � �������� �� � �������� ������
    /// </summary>
    void FinishedPicking()
    {
        _pc.FinishedPicking();
    }
}
