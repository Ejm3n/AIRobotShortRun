using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    #region GameCanvas
    [SerializeField] Text[] _maxItemsInBase;
    [SerializeField] Text _playerItemsInBase;
    [SerializeField] Text _playerItemsOnBack;
    [SerializeField] Text _playerMaxItemsOnBack;
    [SerializeField] Text _botMaxItemsOnBack;
    [SerializeField] Text _botItemsInBase;
    [SerializeField] Text _botItemsOnBack;   
    #endregion

    #region EndCanvas
    [SerializeField] Text _winnerName;
    #endregion
    [SerializeField] CanvasGroup _gameCanvasGroup;
    [SerializeField] CanvasGroup _pauseCanvasGroup;
    [SerializeField] CanvasGroup _preLevelCanvasGroup;
    private bool _finished;
    private GameHandler _gameHandler;
    private Joystick joystick;
    
    /// <summary>
    /// �������� ��������� ���� �������
    /// </summary>
    private void Start()
    {
        ChangeStates(_pauseCanvasGroup,_preLevelCanvasGroup);
        ChangeCanvasGroup(_gameCanvasGroup, false);
        Time.timeScale = 0;
        joystick = FindObjectOfType<Joystick>();
        _gameHandler = GameHandler.Game;
        foreach (Text text in _maxItemsInBase)
        {
            text.text = _gameHandler.GetMaxItemsInBase().ToString();
        }
        _playerMaxItemsOnBack.text = _gameHandler.GetPlayersMaxItemsOnBack().ToString();
        _botMaxItemsOnBack.text = _gameHandler.GetBotMaxItemsOnBack().ToString();
    }

    /// <summary>
    /// �������� ������� ���-�� ��������� � ������� � �������� �� ����������� �� ����
    /// </summary>
    void Update()
    {
        _playerItemsInBase.text = _gameHandler.GetPlayersItemsInBase().ToString();
        _playerItemsOnBack.text = _gameHandler.GetPlayersItemsOnBack().ToString();
        _botItemsInBase.text = _gameHandler.GetBotItemsInBase().ToString();
        _botItemsOnBack.text = _gameHandler.GetBotItemsOnBack().ToString();
        if(_gameHandler.GetGameFinished() && !_finished)
        {
            joystick.enabled = false;
            ChangeStates(_gameCanvasGroup, _pauseCanvasGroup);
            _finished = true;

            if(_gameHandler.WhoWon())
            {
                _winnerName.text = "�� ��������!";
            }
            else
            {
                _winnerName.text = "�� ���������!";
            }
        }
    }

    /// <summary>
    /// ����� ��� ������ ����������� ������� �������
    /// </summary>
    public void StartLevel()
    {
        Time.timeScale = 1;
        ChangeStates(_preLevelCanvasGroup, _gameCanvasGroup);
    }

    /// <summary>
    /// ����� ��� ������ ��������������� ������� �����
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// �������� ��������� ���� ������ ����� - ������ ���������, ������ ��������
    /// </summary>
    /// <param name="whatOff"></param>
    /// <param name="whatOn"></param>
    private void ChangeStates(CanvasGroup whatOff, CanvasGroup whatOn)
    {
        ChangeCanvasGroup(whatOff, false);
        ChangeCanvasGroup(whatOn, true);
    }

    /// <summary>
    /// �������� ��������� ������ ������
    /// </summary>
    /// <param name="canvasGroup"></param>
    /// <param name="what"></param>
    private void ChangeCanvasGroup(CanvasGroup canvasGroup,bool what)
    {
        if (what)       
            canvasGroup.alpha = 1;        
        else
            canvasGroup.alpha = 0;
        canvasGroup.interactable = what;
        canvasGroup.blocksRaycasts = what;        
    }
}
