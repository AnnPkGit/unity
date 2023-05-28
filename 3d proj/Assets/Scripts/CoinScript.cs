using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinScript : MonoBehaviour
{
    private GameObject _character;
    private Animator _animator;
    private System.Random _random;

    void Start()
    {
        _character = GameObject.Find("Character");
        _animator = GetComponent<Animator>();
        _random = new System.Random();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collector"))
        {
            _animator.SetBool("isCollected", true);
        }
    }

    public void Dissapeared()
    {
        Debug.Log("Disapeared");
        Vector3 minPosition = _character.transform.position + (_character.transform.forward * 10f);
        Vector3 maxPosition = _character.transform.position + (_character.transform.forward * 20f);

        this.transform.position = new Vector3(Random.Range(minPosition.x, maxPosition.x),
                                             transform.position.y,
                                             Random.Range(minPosition.z, maxPosition.z));

        _animator.SetBool("isCollected", false);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider collider in colliders)
        {
            if (collider.tag != "Collector")
            {
                do
                {
                    transform.position += Vector3.up * 2f;
                    colliders = Physics.OverlapSphere(transform.position, 1f);
                } while (colliders.Length > 0);
            }
        }
    }
}
