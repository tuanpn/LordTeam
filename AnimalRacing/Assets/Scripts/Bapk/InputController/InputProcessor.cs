using UnityEngine;
using System.Collections;

public class InputProcessor : MonoBehaviour {
    private Rect rect;
    private Vector2 position;
    private bool isTouchedDown;
    private float width;
    private float height;

    public void Start()
    {
        position = new Vector2((transform.position.x + 4) * 100, (transform.position.y + 2.4f) * 100);
        width = gameObject.GetComponent<SpriteRenderer>().sprite.rect.width;
        height = gameObject.GetComponent<SpriteRenderer>().sprite.rect.height;
        rect = new Rect(position.x - width / 2, position.y - height / 2, width, height);
    }

    private void calculateTransform()
    {
        position = new Vector2((transform.position.x + 4) * 100, (transform.position.y + 2.4f) * 100);
        //width = gameObject.GetComponent<SpriteRenderer>().sprite.rect.width;
        //height = gameObject.GetComponent<SpriteRenderer>().sprite.rect.height;
        rect = new Rect(position.x - width / 2, position.y - height / 2, width, height);
    }

    public void Update()
    {
        if (!InputController.Enabled) return;
        calculateTransform();
        if (Input.GetButtonDown("Fire1"))
        {
            if (checkBounds(rect, convertTo800x480(Input.mousePosition)))
            {
                isTouchedDown = true;
                if(gameObject.GetComponent<InputAdapter>() != null)
                    gameObject.GetComponent<InputAdapter>().OnTouchDown();
                InputController.IsScreen = false;
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            if (gameObject.GetComponent<InputAdapter>() != null)
                gameObject.GetComponent<InputAdapter>().OnCheckUp();
            Vector2 mouse = convertTo800x480(Input.mousePosition);
            if (isTouchedDown && checkBounds(rect, convertTo800x480(Input.mousePosition)))
            {
                if(gameObject.GetComponent<InputAdapter>() != null)
                    gameObject.GetComponent<InputAdapter>().OnTouchUp();
            }
            isTouchedDown = false;
        }
    }

    private bool checkBounds(Rect rect, Vector2 mouse)
    {
        if (rect.x <= mouse.x && rect.x + rect.width >= mouse.x && rect.y <= mouse.y && rect.y + rect.height >= mouse.y)
            return true;
        return false;
    }

    private Vector2 convertTo800x480(Vector3 position)
    {
        float x = position.x * 800 / Screen.width;
        float y = position.y * 480 / Screen.height;
        return new Vector2(x, y);
    }
}
