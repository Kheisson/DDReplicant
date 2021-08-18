using UnityEngine;
using DG.Tweening;
public class Arrow : MonoBehaviour
{
    public enum ArrowType {UP, DOWN, LEFT, RIGHT};
    public ArrowType type;
    public Vector3 StartingPos { get => _startingPos; set => _startingPos = value; }

    private float _speed = 1f;
    private Vector3 _startingPos;
    private Vector3 _animationStrength = new Vector3(0, .2f, 0);


    private void OnEnable()
    {
        _speed = SpawnManager.Spawner.Speed;
        AudioManager.Instance.OnSongEnded += GameEnded;
    }

    private void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * _speed;
        if (!DOTween.IsTweening(transform))
        {
            transform.DORewind();
            transform.DOShakeScale(1f, _animationStrength);
        }
    }

    //Resets position on disable
    private void OnDisable()
    {
        transform.position = StartingPos;
    }

    //Upon touching the lower bounds, disable gameobject
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            gameObject.SetActive(false);
        }
    }

    //Disables object when song ends
    private void GameEnded() => gameObject.SetActive(false);

}
