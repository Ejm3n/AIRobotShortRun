using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// вспомогательный скрипт для передачи информации из аниматора
/// </summary>
public class BotAnimationController : MonoBehaviour
{

    private BotController _bc;

    private void Awake()
    {
        _bc = FindObjectOfType<BotController>();
    }

    /// <summary>
    /// в конце анимации looking запускается метод в основном скрипте у бота
    /// </summary>
    void EndLooking()
    {
        _bc.EndLooking();
    }

    /// <summary>
    /// в начале анимации looking запускается метод в основном скрипте у бота
    /// </summary>
    void StartLooking()
    {
        _bc.StartLooking();
    }

    /// <summary>
    /// в конце анимации picking запускается метод в основном скрипте у бота
    /// </summary>
    void FinishedPicking()
    {
        _bc.FinishedGrabbining();
    }
}
