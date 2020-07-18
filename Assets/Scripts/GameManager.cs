using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float density;
    public Transform prefab;
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        Timer = 2 - (float).50*density;
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= 1 * Time.deltaTime;
        if (Timer <= 0)
        {
            Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            Timer = 2;
        }
    }

}
