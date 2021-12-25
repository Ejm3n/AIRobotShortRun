using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]private Base _playerBase;
    [SerializeField]private Base _botBase;
    [SerializeField] private int _maxItemsInBase;
    [SerializeField] private float _timeToSpawnNewItem;
    private List<GameObject> _activeItems;
    private List<GameObject> _deactivatedItems;
    private PlayerController _pc;
    private BotController _bc;
    private bool _gameFinished = false;
    private bool _isPlayerWinner;
    public static GameHandler Game;
    
    void Awake()
    {
        if(Game==null)
        {
            Game = this;
        }
        _pc = FindObjectOfType<PlayerController>();
        _bc = FindObjectOfType<BotController>();
        _bc.SetMaxItemsInBase(_maxItemsInBase);
        _deactivatedItems = GameObject.FindGameObjectsWithTag("Item").ToList();
        foreach(GameObject item in _deactivatedItems)
        {
            item.SetActive(false);
        }
        _activeItems = new List<GameObject>();
        StartCoroutine(ItemActivateTimer());       
    }

   /// <summary>
   /// контроль количества предметов на сцене и проверка на окончание игры
   /// </summary>
    void Update()
    {
        if (_activeItems.Count < 2)
        {
            ActivateItem();
        }

        if (_playerBase.GetCurrentItemsInBase()>=_maxItemsInBase || _botBase.GetCurrentItemsInBase()>=_maxItemsInBase)
        {
            _gameFinished = true;
            if(_playerBase.GetCurrentItemsInBase() >= _maxItemsInBase)
            {
                _isPlayerWinner = true;
                _pc.WinAnimation();
                _bc.LoseAnimation();
            }
            else if(_botBase.GetCurrentItemsInBase() >= _maxItemsInBase)
            {
                _bc.WinAnimation();
                _pc.LoseAnimation();
            }
        }
    }

    #region Getters&setters
    /// <summary>
    /// возвращает закончилась ли игра
    /// </summary>
    /// <returns></returns>
    public bool GetGameFinished()
    {
        return _gameFinished;
    }

    /// <summary>
    /// возвращает количество предметов у игрока на спине
    /// </summary>
    /// <returns></returns>
    public int GetPlayersItemsOnBack()
    {
        return _pc.GetCurrentItems();
    }

    /// <summary>
    /// возвращает количество предметов у бота на спине
    /// </summary>
    /// <returns></returns>
    public int GetBotItemsOnBack()
    {
        return _bc.GetCurrentBotItems();
    }

    /// <summary>
    /// возвращает максимальное количество предметов на спине у игрока
    /// </summary>
    /// <returns></returns>
    public int GetPlayersMaxItemsOnBack()
    {
        return _pc.GetMaxPlayerItems();
    }

    /// <summary>
    /// возвращает максимальное количество предметов на спине у бота
    /// </summary>
    /// <returns></returns>
    public int GetBotMaxItemsOnBack()
    {
        return _bc.GetMaxBotItems();
    }

    /// <summary>
    /// возвращает количество предметов на базе у игрока
    /// </summary>
    /// <returns></returns>
    public int GetPlayersItemsInBase()
    {
        return _playerBase.GetCurrentItemsInBase();
    }

    /// <summary>
    /// возвращает количество предметов на базе у бота
    /// </summary>
    /// <returns></returns>
    public int GetBotItemsInBase()
    {
        return _botBase.GetCurrentItemsInBase();
    }

    /// <summary>
    /// возвращает макс количество предметов на базе
    /// </summary>
    /// <returns></returns>
    public int GetMaxItemsInBase()
    {
        return _maxItemsInBase;
    }
    
    /// <summary>
    /// возвращает булевую победил ли ИГРОК
    /// </summary>
    /// <returns></returns>
    public bool WhoWon()
    {
        return _isPlayerWinner;
    }
    
    /// <summary>
    /// возвращает лист деактивированных предметов
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetItemList()
    {
        return _deactivatedItems;
    }
    #endregion

    /// <summary>
    /// включить случайный предмет на сцене
    /// </summary>
    private void ActivateItem()
    {      
        if(_deactivatedItems.Count>0)
        {
            CheckActivatedItems();
            int indexToRemove = Random.Range(0, _deactivatedItems.Count);
            _deactivatedItems[indexToRemove].SetActive(true);
            _activeItems.Add(_deactivatedItems[indexToRemove]);
            _deactivatedItems.RemoveAt(indexToRemove);
            
        }       
    }

    /// <summary>
    /// проверка какие предметы включены на сцене
    /// </summary>
    private void CheckActivatedItems()
    {
        for(int i = 0;i<_activeItems.Count; i++)
        {
            if(!_activeItems[i].activeInHierarchy)
            {
                _deactivatedItems.Add(_activeItems[i]);
                _activeItems.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// включение предмета спустя заданное время
    /// </summary>
    /// <returns></returns>
    private IEnumerator ItemActivateTimer()
    {
        while(!_gameFinished)
        {
            yield return new WaitForSeconds(_timeToSpawnNewItem);

            ActivateItem();
        }
        
    }
}
