using DG.Tweening;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [Header("Materials")]
    public Material unlitMaterial;
    public Material litMaterial;

    private SpriteRenderer _sprite;
    private PolygonCollider2D _polyCol;
    private string _gameObjectName;
    private Vector3 _animationStrength = new Vector3(1f, 1f, 0);

    private void Start()
    {
        AudioManager.Instance.OnSongEnded += HideArrow;
        _gameObjectName = gameObject.name.Trim(); 
        _sprite = GetComponent<SpriteRenderer>();
        _polyCol = GetComponent<PolygonCollider2D>();
    }

    private void FixedUpdate()
    {
        HandleInput();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            HandleScore(collision.gameObject.transform, transform);
            collision.gameObject.SetActive(false);
        }
    }

    //Handles input based on key pressed.
    private void HandleInput()
    {
        if (Input.GetButton(_gameObjectName))
        {
            _polyCol.enabled = true;
            _sprite.material = litMaterial;
            if (!DOTween.IsTweening(transform))
            {
                transform.DORewind();
                transform.DOPunchScale(_animationStrength, .25f);
            }
        }
        else
        {
            _polyCol.enabled = false;
            _sprite.material = unlitMaterial;
        }
    }

    //Updates score based on timing
    private void HandleScore(Transform arrow, Transform origin)
    {
        byte score = 0;
        float distance = Vector3.Distance(arrow.position, origin.position);
        if (distance < 0.25)
            score = 100;
        else if (distance < 0.5)
            score = 85;
        else if (distance < 0.7)
            score = 65;
        else if (distance < 0.9)
            score = 45;
        UIManager.Instance.UpdateScoreText(score);
    }

    //Subscribed to audio event, hides all arrow controllers
    private void HideArrow()
    {
        gameObject.SetActive(false);
    }
}
