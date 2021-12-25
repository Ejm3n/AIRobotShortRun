using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// управление ботом
/// </summary>
public class BotController : MonoBehaviour, ICharacterCubes
{
    [SerializeField] private int _maxItems;
    [SerializeField] private Transform _basePosition;
    private int _currentItems;
    private int _maxItemsInBase;
    private int _currentItemsInBase;
    private ItemsPlaceToBack _itemsPlace;
    private NavMeshAgent _agent;
    private GameObject _closestItem;
    private Animator _anim;
    private bool _IsLooking;
    private bool _isGrabbing;
    private bool _runningToBase;
    private GameObject[] _items;

    void Start()
    {
        _items = GameHandler.Game.GetItemList().ToArray();
        _anim = transform.GetChild(0).GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _itemsPlace = GetComponent<ItemsPlaceToBack>();
        _currentItemsInBase = 0;
    }

    /// <summary>
    /// переключение с бега на поиск предмета при выполненных условиях
    /// </summary>
    void Update()
    {
        if (_closestItem != null && _closestItem.activeInHierarchy && !_runningToBase && !_IsLooking && !_isGrabbing)
        {
            _anim.SetBool("Running", true);
        }
        else if (_closestItem != null && !_closestItem.activeInHierarchy && !_runningToBase && !_IsLooking && !_isGrabbing)
        {
            FindClosestItem();
        }
    }
    #region animations stuff
    /// <summary>
    /// вызывается в начале анимации высматривания
    /// </summary>
    public void StartLooking()
    {
        _IsLooking = true;
    }

    /// <summary>
    /// вызывается в конце анимации высматривания
    /// </summary>
    public void EndLooking()
    {
        FindClosestItem();
        _IsLooking = false;
    }

    /// <summary>
    /// начала анимации подбора
    /// </summary>
    public void StartGrabbing(Transform where)
    {
        if (_currentItems < _maxItems && _closestItem != null && _closestItem.transform.position == where.position &&
            _closestItem.transform.position != _basePosition.position)
        {
            _anim.SetBool("Grabbing", true);
            _isGrabbing = true;
            _agent.SetDestination(transform.position);
        }
    }

    /// <summary>
    /// конец анимации подбора
    /// </summary>
    public void FinishedGrabbining()
    {
        if (_closestItem != null && _closestItem.activeInHierarchy && _closestItem.transform.position != _basePosition.position)
        {
            _currentItems++;
            _itemsPlace.ChangeItemsCountOnBack(_currentItems);
            _closestItem.SetActive(false);
        }
        _isGrabbing = false;

        if (_currentItems == _maxItems || _currentItems >= (_maxItemsInBase - _currentItemsInBase))
        {
            _anim.SetBool("Running", true);

        }
        _anim.SetBool("Grabbing", false);
    }
    public void WinAnimation()
    {
        _anim.SetTrigger("Win");
    }

    public void LoseAnimation()
    {
        _anim.SetTrigger("Lose");
    }
    #endregion
    /// <summary>
    /// устанавливает значение максимальных предметов на базе
    /// </summary>
    /// <param name="max"></param>
    public void SetMaxItemsInBase(int max)
    {
        _maxItemsInBase = max;
    }
    /// <summary>
    /// возвращает текущие предметы у бота
    /// </summary>
    /// <returns></returns>
    public int GetCurrentBotItems()
    {
        return _currentItems;
    }
    /// <summary>
    /// возвращает максимальное количество предметов, которое можно таскать
    /// </summary>
    /// <returns></returns>
    public int GetMaxBotItems()
    {
        return _maxItems;
    }

    /// <summary>
    /// когда достигает базы убирает текущие предметы из текущих и передает их на базу
    /// </summary>
    /// <returns></returns>
    public int PutCubesToBase()
    {
        int itemsToBase = _currentItems;
        _currentItemsInBase += itemsToBase;
        _currentItems = 0;
        _itemsPlace.ChangeItemsCountOnBack(_currentItems);

        _anim.SetTrigger("LookingInBase");
        return itemsToBase;
    }

    /// <summary>
    /// найти ближайший предмет
    /// </summary>
    private void FindClosestItem()
    {
        _closestItem = null;
        if (_currentItems == _maxItems || _currentItems >= (_maxItemsInBase - _currentItemsInBase))
        {
            _runningToBase = true;
            _closestItem = _basePosition.gameObject;
        }
        else
        {
            _runningToBase = false;
            float distance = float.MaxValue;
            Debug.Log(_items.Length);
            foreach (GameObject item in _items)
            {
                Debug.Log(item.name + " " + (Vector3.Distance(transform.position, item.transform.position) < distance && item.activeInHierarchy));
                if (item.activeInHierarchy && Vector3.Distance(transform.position, item.transform.position) < distance)
                {
                    Debug.Log(item.name);
                    _closestItem = item;
                    distance = Vector3.Distance(transform.position, item.transform.position);
                }
            }
        }
        if (_closestItem != null)
            _agent.SetDestination(_closestItem.transform.position);
    }
}
