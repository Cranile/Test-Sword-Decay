using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFireBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _destroyParticle;
    [SerializeField]
    private Sprite[] _fireState;
    [SerializeField]
    private GameObject _fireParticle;
    private GameObject _fireON;
    public bool _isFireOn = false;
    private GameObject _player;
    private Player _playerScript;
    
    void Start()
    {
        //_player = GameObject.Find("Character").GetComponent<Player>();
        _playerScript =GameObject.Find("Character").GetComponent<Player>();
    }

    void Update()
    {
       
    }

    public void FireOn()
    {
        _isFireOn = true;
        _playerScript._isFireOn = true;
        GetComponent<SpriteRenderer>().sprite = _fireState[1];
        _fireON = Instantiate(_fireParticle, transform.position,Quaternion.identity);
    }

    public void Destruction()
    {
        _playerScript._isFireOn = false;
        Instantiate(_destroyParticle,transform.position,Quaternion.identity);
        if(_fireON != null)
        {
            Destroy(_fireON);
        }
        Destroy(this.gameObject);

    }

}
