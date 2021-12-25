using Assets.Scripts;
using UnityEngine;
[RequireComponent(typeof(Rigidbody), typeof(ItemsPlaceToBack))]
public class PlayerController : MonoBehaviour, ICharacterCubes
{
    [SerializeField] private float _speed;
    [SerializeField] private int _maxItems;
    private int currentItems;
    private Joystick _joystick;
    private Rigidbody _rb;
    private Animator _anim;
    private ItemsPlaceToBack _itemsPlace;
    private GameObject _closestItem;
    private bool _isRunning;
    private bool _isIdle;
    private bool _isWalking;
    private bool _isPicking;
    private bool _nearItem;
    private bool _gameEnded;

    private void Awake()
    {
        _itemsPlace = GetComponent<ItemsPlaceToBack>();
        _rb = GetComponent<Rigidbody>();
        _joystick = FindObjectOfType<Joystick>();
        _anim = transform.GetChild(0).GetComponent<Animator>();
    }

    /// <summary>
    /// регулируютс€ анимации
    /// </summary>
    private void Update()
    {
            if (_rb.velocity.magnitude < .05f)
            {
                _isIdle = true;
                _isWalking = false;
                _isRunning = false;
            }
            else if (_rb.velocity.magnitude < 1.3f)
            {
                _isIdle = false;
                _isWalking = true;
                _isRunning = false;
            }
            else
            {
                _isIdle = false;
                _isWalking = false;
                _isRunning = true;
            }
            _anim.SetBool("Idle", _isIdle);
            _anim.SetBool("Walking", _isWalking);
            _anim.SetBool("Running", _isRunning);              
    }

    /// <summary>
    /// работа с физикой
    /// </summary>
    private void FixedUpdate()
    {
        if (!_isPicking && !_gameEnded)
        {
            _rb.velocity = new Vector3(_joystick.Horizontal * _speed, _rb.velocity.y, _joystick.Vertical * _speed);
            if (_joystick.Vertical != 0 || _joystick.Horizontal != 0)
            {
                transform.rotation = Quaternion.LookRotation(_rb.velocity);
            }
        }
        else if(_gameEnded || _isPicking)
        {
            _rb.velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// подбор куба с проверкой р€дом ли находитс€ игрок
    /// </summary>
    public void PickUpCube()
    {
        if (currentItems < _maxItems && !_isPicking && _nearItem
            && _closestItem != null && _closestItem.activeInHierarchy)
        {
            _anim.SetTrigger("Grabbing");
            _isPicking = true;
        }
    }

    /// <summary>
    /// возвращает текущее количество предметов
    /// </summary>
    /// <returns></returns>
    public int GetCurrentItems()
    {
        return currentItems;
    }

    /// <summary>
    /// возвращает макс количество предметов у игрока на руках
    /// </summary>
    /// <returns></returns>
    public int GetMaxPlayerItems()
    {
        return _maxItems;
    }

    /// <summary>
    /// закончить анимацию подбора
    /// </summary>
    public void FinishedPicking()
    {
        if (_closestItem != null && _closestItem.activeInHierarchy)
        {
            currentItems++;
            _itemsPlace.ChangeItemsCountOnBack(currentItems);
            _closestItem.SetActive(false);
        }
        _isPicking = false;
    }

    /// <summary>
    /// сталкива€сь с базой мы складываем туда кубы
    /// </summary>
    /// <returns></returns>
    public int PutCubesToBase()
    {
        int itemsToBase = currentItems;
        currentItems = 0;
        _itemsPlace.ChangeItemsCountOnBack(currentItems);
        return itemsToBase;
    }
    /// <summary>
    /// анимаци€ победы
    /// </summary>
    public void WinAnimation()
    {
        _anim.SetTrigger("Win");
        _gameEnded = true;
    }

    /// <summary>
    /// анимаци€ поражени€
    /// </summary>
    public void LoseAnimation()
    {
        _anim.SetTrigger("Lose");
        _gameEnded = true;
    }
    /// <summary>
    /// возле подбираемого предмета добавл€ем его в переменную 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            _nearItem = true;
            _closestItem = other.gameObject;
        }
    }
    /// <summary>
    /// удал€€сь от подбираемого предмета отключаем возможность подбора 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            _nearItem = false;
        }
    }
}
