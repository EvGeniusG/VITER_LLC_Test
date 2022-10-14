using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    bool Taken = false;
    List<Collider2D> CeilsAndPocketsAround = new List<Collider2D>();

    Transform DefaultPocket;// Ёто нужно, чтобы начать игру сначала
    Transform PreviousPocket;
    SpriteRenderer sprite;
    GameObject Settings;
    private void Awake()
    {
        DefaultPocket = transform.parent;

        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.white;

        Settings = Game.instance.Settings;
    }
    
    void FixedUpdate()
    {
        if (Taken)//“ут просто следование подобранного кубика за пальцем игрока
        {
            Vector2 TitlePosition;
#if (UNITY_EDITOR)
            TitlePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#else
            TitlePosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
            transform.position = TitlePosition;
        }
    }

    private void OnMouseDown()
    {
        if(transform.parent.tag != "Ceil" && !Settings.activeSelf)// Ceil - клетка на поле, Pocket - карман внизу
        {
            PreviousPocket = transform.parent;
            Taken = true;
            CeilsAndPocketsAround = new List<Collider2D>();
            CeilsAndPocketsAround.Add(transform.parent.GetComponent<Collider2D>());
        }
    }

    private void OnMouseUp()
    {
        Taken = false;
        if(CeilsAndPocketsAround.Count > 0)
        {
            transform.parent = FindClosestCeil();
            if(transform.parent.tag == "Ceil")
            {
                Game.instance.CheckResult();
            }
        }
        transform.localPosition = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CeilsAndPocketsAround.Add(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CeilsAndPocketsAround.Remove(collision);
    }

    Transform FindClosestCeil()// ѕри отпускании пальца игрока кубик переместитс€ в ближайший карман
    {
        Transform closest = null;
        float distance = 0;
        for(int i = 0; i < CeilsAndPocketsAround.Count; i++)
        {
            var newClosest = CeilsAndPocketsAround[i].transform;
            float newDistance = Vector3.Distance(newClosest.position, transform.position);
            if (closest == null || newDistance < distance)
            {
                closest = newClosest;
                distance = newDistance;
            }
        }
        return closest.childCount > 0 ? PreviousPocket : closest;
    }

    public void ReturnToDefaultPosition() //Ёто нужно, чтобы все кубики при новой игре вернулись на место
    {
        transform.parent = DefaultPocket;
        transform.localPosition = Vector3.zero;
        sprite.color = Color.white;
    }
}
