using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string text;

    public void OnPanel()
    {
        GameObject.Find("Panel_Item").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Panel_Item").transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = text;
    }
}
