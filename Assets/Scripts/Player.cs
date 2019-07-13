using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    private WeaponBehavoiur _sword;
    private GameObject _torch;
    [SerializeField]
    private GameObject _weapon;
    [SerializeField]
    private GameObject _weapon1;
    [SerializeField]
    private GameObject _spriteSword;
    [SerializeField]
    private GameObject _spawnEnemy;
    private GameObject fire;
    //[SerializeField]
    //private bool _swordEnabled;
    private EnemyBehaviour _enemy;
    [SerializeField]
    private bool _isFireNear = false;
    public bool _isFireOn = false;
    void Start()
    {
        //_sword = _weapon.GetComponent<WeaponBehavoiur>();
        
    }

    
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W)) //move up
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) // movedownwards
        {
            transform.Translate(Vector2.down * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) //moved left
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))//move rigth
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.R))//sword spawner at 0.17f 3.62f -0.09f 
        {
            Debug.Log("Summoning a new sword");
            Instantiate(_spriteSword,new Vector3 (0.17f, 2.70f, -0.09f), Quaternion.identity);
        }

        if(Input.GetKeyDown(KeyCode.T))//enemy spawner at -8.07f 0.047f -0.09
        {
            Debug.Log("Summoning a new enemy");
            Instantiate(_spawnEnemy,new Vector3(-8.07f, 0.047f, -0.09f),Quaternion.identity);
        }

        if(Input.GetKeyDown(KeyCode.E))//picking up torch
        {
            if(_isFireNear == true)//if the player is in the range of the fire make other check
            {
                if (_isFireOn == true)
                {
                    _sword = _weapon.GetComponent<WeaponBehavoiur>();
                    _sword.durability = 0;
                    _weapon.SetActive(false);
                    _weapon1.SetActive(true);
                    
                }else if(_isFireOn == false)
                {
                    Debug.Log("The fire is OFF");
                }
            }else if(_isFireNear == false)
            {
                Debug.Log("I cant reach the fire, its too far away");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) //shout
        {
            Debug.Log("Invernalia!!");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon") // must solve a problem where if you hit the pickup with your weapon it pickup the item
        {
            //Debug.Log("Taking sword" + other.name);
            _sword = _weapon.GetComponent<WeaponBehavoiur>();
            if (_sword.durability < 10 ) //temporarily removed due to a bug that causes the inability to re pick a sword after taking 1 and switching to a torch, to problem resides in the program looking the durability only and not if the sword is enabled
            {
                _weapon.SetActive(true);
                _sword.durability = 10;
                if(_weapon1 != null)
                {
                    _weapon1.SetActive(false);
                }
                Destroy(other.gameObject);
            }
        }
        if(other.tag == "Weapon1")
        {
            _weapon1.GetComponent<GameObject>();
            if(_weapon != null )
            {
                _sword = _weapon.GetComponent<WeaponBehavoiur>();
                _sword.durability = 0;
                _weapon.SetActive(false);
                
            }
            _weapon1.SetActive(true);
            Destroy(other.gameObject);
        }
        if(other.tag == "Enemy")
        {
            Debug.Log("Auch");
        }
        if(other.name == "FireRange")
        {
            _isFireNear = true;
        }
        if(other.name == "FireOutRange")
        {
            _isFireNear = false;
        }
        if (other.tag == "WoodFire")
        {
            _isFireNear = true;
        }
    }
}
