using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int _eHealth = 3;
    [SerializeField]
    private GameObject _deathParticle;
    private GameObject _usedObject;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(_eHealth <= 0)
        {
            _usedObject = Instantiate(_deathParticle, transform.position, Quaternion.identity);
            Destroy(_usedObject,1);
            Debug.Log("Arrgghh");
            Destroy(this.gameObject);
        }
    }

    
}
