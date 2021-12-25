using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// вспомогательный скрипт для передачи информации из аниматора
/// </summary>
public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerController _pc;

    private void Awake()
    {
        _pc = GetComponentInParent<PlayerController>();       
    }

    /// <summary>
    /// ссылка анимации у игрока и передача ее в основной скрипт
    /// </summary>
    void FinishedPicking()
    {
        _pc.FinishedPicking();
    }
}
