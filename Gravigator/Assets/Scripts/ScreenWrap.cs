using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    [Header("Variables")]
    public GameObject sisterPos;

    public bool canWrap;
    public bool isVertical;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canWrap)
            return;

        if (collision.tag == "Player")
        {
            sisterPos.GetComponent<ScreenWrap>().canWrap = false;
            TeleportToSister(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canWrap = true;
        }
    }

    void TeleportToSister(GameObject player)
    {
        Vector3 newPos;

        if (isVertical)
            newPos = new Vector3(player.transform.position.x, sisterPos.transform.position.y, 0);
        else
            newPos = new Vector3(sisterPos.transform.position.x, player.transform.position.y, 0);

        player.GetComponent<Transform>().position = newPos;
    } 
}
