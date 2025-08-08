using UnityEngine;

public class PanelItem : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(false);
        }
    }
}
