using UnityEngine;

public class ScaleByY : MonoBehaviour
{
    [Tooltip("��������� ������� �������")]
    public Vector3 initialScale = Vector3.one;  // ����� ������ ��������� ������ � ����������

    [Tooltip("����������� ���������������� �������� � ��������� Y")]
    public float scaleFactor = 0.01f; // ������������ � ����������

    void Start()
    {
        // ������������� ������� ��������� �������
        transform.localScale = initialScale;
    }

    void Update()
    {
        // ��� ���� �� Y � ��� ������ ������ (������ ��������)
        float scale = 1 - transform.position.y * scaleFactor;
        scale = Mathf.Max(scale, 0.1f); // ����������� ������, ����� ������ �� �������

        // �������� ������� ��������������� ����������
        transform.localScale = initialScale * scale;
    }
}
