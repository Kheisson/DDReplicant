using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public enum ArrowType {UP, DOWN, LEFT, RIGHT};
    public ArrowType type;
    public Vector3 StartingPos { get => _startingPos; set => _startingPos = value; }

    private float _speed = 1f;
    private Vector3 _startingPos;


    private void OnEnable()
    {
        _speed = SpawnManager.Spawner.Speed;
    }

    private void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * _speed;
    }

    //Resets position on disable
    private void OnDisable()
    {
        transform.position = StartingPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            gameObject.SetActive(false);
        }
    }

}
