using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    ParticleSystem particle;
    private void Awake()
    {
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        StartCoroutine(PlayFewSec());   
    }

    /// <summary>
    /// включает партиклы на какое-то время
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayFewSec()
    {
        particle.Play();
        yield return new WaitForSeconds(.4f);
        particle.Stop();
    }
}
