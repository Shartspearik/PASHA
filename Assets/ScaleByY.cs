using UnityEngine;

public class ScaleByY : MonoBehaviour
{
    [Tooltip("Начальный масштаб объекта")]
    public Vector3 initialScale = Vector3.one;  // Можно задать начальный размер в инспекторе

    [Tooltip("Коэффициент чувствительности масштаба к изменению Y")]
    public float scaleFactor = 0.01f; // Регулируется в инспекторе

    void Start()
    {
        // Устанавливаем объекту начальный масштаб
        transform.localScale = initialScale;
    }

    void Update()
    {
        // Чем выше по Y — тем меньше объект (эффект удаления)
        float scale = 1 - transform.position.y * scaleFactor;
        scale = Mathf.Max(scale, 0.1f); // Минимальный размер, чтобы объект не исчезал

        // Изменяем масштаб пропорционально начальному
        transform.localScale = initialScale * scale;
    }
}
