using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Base : MonoBehaviour
{
    [SerializeField] private bool _isItPlayerBase;
    private int _currentItemsInBase;
    private string _characterTag;

    private void Awake()
    {
        if(_isItPlayerBase)
        {
            _characterTag = "Player";
        }
        else
        {
            _characterTag = "Bot";
        }
    }

    /// <summary>
    /// ���������� ������� ���������� ��������� �� ����
    /// </summary>
    /// <returns></returns>
    public int GetCurrentItemsInBase()
    {
        return _currentItemsInBase;
    }

    /// <summary>
    /// ��������� ����� ����� ��������� �� ���� � ������ � ����
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(_characterTag))
        {
            _currentItemsInBase += other.gameObject.GetComponent<ICharacterCubes>().PutCubesToBase();
            Debug.Log(_currentItemsInBase);
        }
    }
}
