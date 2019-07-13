using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavoiur : MonoBehaviour
{

    public int durability ;
    private int _changes = 0;
    public Sprite[] states;
    public int condition;
    private EnemyBehaviour _enemy;
    [SerializeField]
    private GameObject _particle;
    [SerializeField]
    private GameObject _destroyedSword;
    [SerializeField]
    private int _weaponID;
    private Transform _BladeCenter;
    private GameObject _usedParticle;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        if (_weaponID == 0)
        {
            _BladeCenter = GameObject.Find("BladeCenter").GetComponent<Transform>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        if (_weaponID == 0)
        {
            State();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if weapon collide with the enemy
        if (other.tag == "Enemy")
        {
            Debug.Log(other);
            _enemy = other.GetComponent<EnemyBehaviour>();

            if (this != null)
            {
                //if using weapon 0 (SWORD)
                if (_weaponID == 0)
                {
                    //sword states
                    if (condition == 4)
                    {
                        if (_enemy != null)
                        {
                            _enemy._eHealth -= 7;
                        }
                        durability -= 1;
                    }
                    else if (condition == 3)
                    {
                        //Debug.Log("Tocado");
                        if (_enemy != null)
                        {
                            _enemy._eHealth -= 5;
                        }
                        durability -= 1;
                    }
                    else if (condition == 2)
                    {
                        //Debug.Log("Tocado");
                        if (_enemy != null)
                        {
                            _enemy._eHealth -= 2;
                        }
                        durability -= 1;
                    }
                    else if (condition == 1)
                    {
                        //Debug.Log("Tocado");
                        if (_enemy != null)
                        {
                            _enemy._eHealth--;
                        }
                        durability -= 1;
                    }
                    if (durability <= 0)
                    {
                        Debug.Log("The sword is broken");
                        Instantiate(_destroyedSword, transform.position, transform.rotation);
                        gameObject.SetActive(false);

                    }
                }
            }
            else if (this == null)
            {
                Debug.Log("error");
            }
        }
        //if weapon collides with wood fire
        if (other.tag == "WoodFire")
        {
            if (other != null)
            {
                //if using weapon 1 (TORCH)
                if (_weaponID == 1)
                {
                    other.GetComponent<WoodFireBehaviour>().FireOn();

                }
                if (_weaponID == 0)
                {
                    //call destruction function on woodf behaviour
                    other.GetComponent<WoodFireBehaviour>().Destruction();
                }
            }
        }
    }



    public void State()
        {
        if (durability <= 10 && durability >= 7)
            {
            condition = 4;
            //Debug.Log("1");
            GetComponent<SpriteRenderer>().sprite = states[0];
            }
            else if (durability< 7 && durability >= 5)
            {
                condition = 3;
                //Debug.Log("2");
                GetComponent<SpriteRenderer>().sprite = states[1];
                if (_changes <= 0)
                {
                    _changes++;
                _usedParticle = Instantiate(_particle, _BladeCenter.transform.position, Quaternion.identity);
                Destroy(_usedParticle,1);
                }
            }
            else if (durability< 5 && durability >= 3)
            {
            condition = 2;
                //Debug.Log("3");
                GetComponent<SpriteRenderer>().sprite = states[2];
                if (_changes <= 1)
                {
                    _changes++;
                _usedParticle = Instantiate(_particle, _BladeCenter.transform.position, Quaternion.identity);
                Destroy(_usedParticle,1);
                }
            }
            else if (durability< 3 && durability >= 1)
            {
            condition = 1;
                //Debug.Log("4");
                GetComponent<SpriteRenderer>().sprite = states[3];
                if (_changes <= 2)
                {
                    _changes++;
                _usedParticle = Instantiate(_particle, _BladeCenter.transform.position, Quaternion.identity);
                Destroy(_usedParticle,1);
                }
            }

        }
}
