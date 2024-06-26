using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformHandler : MonoBehaviour
{
    private Rigidbody2D myBody;
    [SerializeField] float maxX, minX;
    float speed = 1.0f;
    bool movingRight = true;
    private Vector2 tempPos;

    // Start is called before the first frame update
    private void Awake() {
        myBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movePlatform();
    }

    void LateUpdate() {
        tempPos = transform.position;
        tempPos.x = myBody.position.x;

        if (tempPos.x < minX) {
            movingRight = true;
        } else if (tempPos.x > maxX) {
            movingRight = false;
        }
    }

    private void movePlatform() {
        if (movingRight) {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
        } else {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
        }
    }
}
