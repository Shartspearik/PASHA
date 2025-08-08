using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.AI;

public class SwapRoom : MonoBehaviour
{
    public Transform targetRoom;  // ������ ��� ������� Q
    private Camera mainCamera;

    public Image fadeImage;       // Image �� ���� ����� (������)
    public float fadeDuration = 1f;


    IEnumerator FadeInOut()
    {
        // ��������� (�� ����������� � �������)
        yield return StartCoroutine(Fade(0f, 1f));

        mainCamera.transform.position = new Vector3(targetRoom.position.x, targetRoom.position.y, -10);
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(targetRoom.position.x, targetRoom.position.y - 1.5f, 0);
        GameObject.FindGameObjectWithTag("Player").transform.GetComponent<NavMeshAgent>().enabled = true;
        // ��������� (�� ������� � �����������)
        yield return StartCoroutine(Fade(1f, 0f));
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color c = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        // ��������� ������ �������� � �����
        fadeImage.color = new Color(c.r, c.g, c.b, endAlpha);
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    public void Swap()
    {
        StartCoroutine(FadeInOut());
    }
}
