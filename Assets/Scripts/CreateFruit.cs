using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFruit : MonoBehaviour
{

    [SerializeField] GameObject[] fruits;
    Vector2 fruitBasePosition = new(0f, -4.5f);

    void Start()
    {
        InvokeRepeating(nameof(CreatingFruit), 15f, 10f);
    }

    void CreatingFruit()
    {
        GameObject fruit = GameObject.FindGameObjectWithTag("Fruit");
        if(fruit == null)
        {
            Instantiate(fruits[Random.Range(0, fruits.Length)], fruitBasePosition, Quaternion.identity);
        }
    }
}
