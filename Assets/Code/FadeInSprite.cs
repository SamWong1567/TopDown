using UnityEngine;
using System.Collections;

public class FadeInSprite : MonoBehaviour {

    SpriteRenderer sprite;
    public float startTime;
    public float duration;
    public float flashSpeed;
    public float minimum = 0f;
    public float maximum = 1f;

    float t;

    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        startTime = Time.time;
    }
    void Update() {
        t = (Time.time - startTime) / duration;
        sprite.color = new Color(1f, 0.5f, 0f, Mathf.PingPong(Time.time*4,1));
        float pingpong = Mathf.PingPong(Time.time * 2, 1);
        sprite.transform.localScale = new Vector3(Mathf.Lerp(0, 7, t), Mathf.Lerp(0, 7, t), 0);
    }

}
