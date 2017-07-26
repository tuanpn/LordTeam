using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class BitmapFont{
    private Dictionary<string, Sprite> sprites;
    private Dictionary<string, Vector2> yoffsets_xadvances;
    private Dictionary<string, Rect> rects;

    private GameObject gameObject;
    private GameObject fontObject;

    private Color color = new Color(1, 1, 1, 1);

    public float width;

    public BitmapFont(BitmapFont bitmapFont, GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.sprites = bitmapFont.getSprites();
        this.yoffsets_xadvances = bitmapFont.getOAs();
        this.rects = bitmapFont.getRects();
    }

    public BitmapFont(string pathPNG, string pathXML, GameObject gameObject)
    {
        this.gameObject = gameObject;
        sprites = new Dictionary<string, Sprite>();
        yoffsets_xadvances = new Dictionary<string, Vector2>();
        rects = new Dictionary<string, Rect>();

        Texture2D texture = Resources.Load<Texture2D>(pathPNG);
        float height = texture.height;
        TextAsset xml = Resources.Load<TextAsset>(pathXML);
        
        XmlDocument test = new XmlDocument();
        test.Load(new StringReader(xml.text));
        
        //test.LoadXml(new StringReader(xml.text).ReadToEnd());
        //string[] keys = new string[] {"x","y","width","height","yoffset","xadvance","letter"};

        foreach (XmlNode node in test.DocumentElement.ChildNodes)
        {
            XmlAttributeCollection collection = node.Attributes;
            Rect rect = new Rect(float.Parse(collection.Item(0).Value), height - float.Parse(collection.Item(1).Value) - float.Parse(collection.Item(3).Value), float.Parse(collection.Item(2).Value), float.Parse(collection.Item(3).Value));
            rects.Add(collection.Item(6).Value, rect);
            sprites.Add(collection.Item(6).Value, Sprite.Create(texture, rect, Vector2.zero));
            yoffsets_xadvances.Add(collection.Item(6).Value, new Vector2(float.Parse(collection.Item(4).Value), float.Parse(collection.Item(5).Value))); 
        }
    }

    public void setColor(Color color)
    {
        this.color = color;
    }

    public void setText(string text, float kerning, float space)
    {
        float k = kerning / 100;
        float sp = space / 100;
        if (fontObject != null)
            Object.Destroy(fontObject);

        this.fontObject = new GameObject("BitmapFont");
        this.fontObject.transform.parent = this.gameObject.transform;
        //this.fontObject.transform.localPosition = new Vector3(0, 0, fontObject.transform.localPosition.z);
        this.fontObject.transform.localPosition = new Vector3(0, 0, 0);

        if (text.Trim().Equals(""))
        {
            removeChildren();
            return;
        }

        float width = 0;
        {
            string c = text[0].ToString();
            GameObject go = new GameObject(c);
            go.AddComponent<SpriteRenderer>().sprite = sprites[c];
            go.GetComponent<SpriteRenderer>().color = color;
            go.transform.parent = fontObject.transform;
            Vector2 p = yoffsets_xadvances[c];
            //go.transform.localPosition = new Vector3(width, p.x/100, go.transform.localPosition.z);
            //go.transform.localPosition = new Vector3(width, 0, go.transform.localPosition.z);
            //go.transform.localPosition = new Vector3(width, p.x/100 -rects[c].height/200, 0);
            go.transform.localPosition = new Vector3(width, -rects[c].height/200, 0);
            //go.transform.localPosition = new Vector3(width, -p.x / 100 + rects[c].height/200, 0);
            width += p.y / 100 + k;
        }

        for (int i = 1; i < text.Length; i++)
        {
            string c = text[i].ToString();
            if (c == " ")
            {
                width += sp;
            }
            else
            {
                GameObject go = new GameObject(c);
                go.AddComponent<SpriteRenderer>().sprite = sprites[c];
                go.GetComponent<SpriteRenderer>().color = color;
                go.transform.parent = fontObject.transform;

                Rect rect = rects[c];
                Vector2 p = yoffsets_xadvances[c];
                //go.transform.localPosition = new Vector3(width, p.x/100, go.transform.localPosition.z);
                //go.transform.localPosition = new Vector3(width, 0, go.transform.localPosition.z);
                //go.transform.localPosition = new Vector3(width, p.x/100 - rect.height/200, 0);
                go.transform.localPosition = new Vector3(width, -rect.height/200, 0);
                //go.transform.localPosition = new Vector3(width, -p.x/100 + rects[c].height/200, 0);
                width += p.y / 100 + k;
            }
        }
        this.width = width;
        fontObject.transform.localPosition -= new Vector3(width/2, 0, 0);
    }

    public void setText(string text, float kerning, float space, string layerName, string sortingLayerName)
    {
        float k = kerning / 100;
        float sp = space / 100;
        if (fontObject != null)
            Object.Destroy(fontObject);

        this.fontObject = new GameObject("BitmapFont");
        this.fontObject.transform.parent = this.gameObject.transform;
        this.fontObject.layer = LayerMask.NameToLayer(layerName);
        //this.fontObject.transform.localPosition = new Vector3(0, 0, fontObject.transform.localPosition.z);
        this.fontObject.transform.localPosition = new Vector3(0, 0, 0);

        if (text.Trim().Equals(""))
        {
            removeChildren();
            return;
        }

        float width = 0;
        {
            string c = text[0].ToString();
            GameObject go = new GameObject(c);
            go.layer = LayerMask.NameToLayer(layerName);
            go.AddComponent<SpriteRenderer>().sprite = sprites[c];
            go.GetComponent<SpriteRenderer>().color = color;
            go.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
            go.transform.parent = fontObject.transform;
            Vector2 p = yoffsets_xadvances[c];
            //go.transform.localPosition = new Vector3(width, p.x/100, go.transform.localPosition.z);
            //go.transform.localPosition = new Vector3(width, 0, go.transform.localPosition.z);
            //go.transform.localPosition = new Vector3(width, p.x/100 -rects[c].height/200, 0);
            go.transform.localPosition = new Vector3(width, 0, 0);
            //go.transform.localPosition = new Vector3(width, -p.x / 100 + rects[c].height/200, 0);
            width += p.y / 100 + k;
        }

        for (int i = 1; i < text.Length; i++)
        {
            string c = text[i].ToString();
            if (c == " ")
            {
                width += sp;
            }
            else
            {
                GameObject go = new GameObject(c);
                go.layer = LayerMask.NameToLayer(layerName);
                go.AddComponent<SpriteRenderer>().sprite = sprites[c];
                go.GetComponent<SpriteRenderer>().color = color;
                go.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
                go.transform.parent = fontObject.transform;

                Rect rect = rects[c];
                Vector2 p = yoffsets_xadvances[c];
                //go.transform.localPosition = new Vector3(width, p.x/100, go.transform.localPosition.z);
                //go.transform.localPosition = new Vector3(width, 0, go.transform.localPosition.z);
                //go.transform.localPosition = new Vector3(width, p.x/100 - rect.height/200, 0);
                go.transform.localPosition = new Vector3(width, 0, 0);
                //go.transform.localPosition = new Vector3(width, -p.x/100 + rects[c].height/200, 0);
                width += p.y / 100 + k;
            }
        }
        this.width = width;
        //fontObject.transform.localPosition -= new Vector3(width / 2, 0, 0);
    }

    private void removeChildren()
    {
        if (fontObject.transform.childCount > 0)
        {
            Transform[] children = fontObject.GetComponentsInChildren<Transform>();
            for (int i = 0; i < children.Length; i++)
                Object.Destroy(children[i].gameObject);
        }
    }

    public Dictionary<string, Sprite> getSprites()
    {
        return this.sprites;
    }

    public Dictionary<string, Vector2> getOAs()
    {
        return this.yoffsets_xadvances;
    }

    public Dictionary<string, Rect> getRects()
    {
        return rects;
    }
}
