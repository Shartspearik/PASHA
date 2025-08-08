using UnityEngine;
using UnityEngine.AI;

public class Movecontroller : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform targetItem;

    // Расстояние, на котором считаем, что игрок подошел к предмету
    public float reachDistance = 1.5f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    void Update()
    {
        // Обработка правой кнопки мыши
        if (UnityEngine.Input.GetMouseButtonDown(1)) // ПКМ
        {

            // Преобразуем позицию курсора в координаты мира 2D
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

            // Выполняем 2D Raycast, луч "направлен" нулевым вектором, чтобы зацепить только точку
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null)
            {
                NavMeshHit navHit;

                Vector3 targetPosition = new Vector3(hit.point.x, hit.point.y, transform.position.z);

                // Проверяем, есть ли точка на NavMesh в радиусе 2 единиц
                if (NavMesh.SamplePosition(targetPosition, out navHit, 2.0f, NavMesh.AllAreas))
                {
                    navMeshAgent.SetDestination(navHit.position);
                }
                else
                {
                    Debug.Log("Мимо — здесь нельзя ходить");
                }
            }
        }



        // Клик мыши в 2D - преобразуем экранную позицию в мир
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero); 
            if (hit.collider != null && hit.collider.CompareTag("Item"))
            {
                targetItem = hit.collider.transform;
                navMeshAgent.SetDestination(targetItem.position);
            }
            if (hit.collider != null && hit.collider.CompareTag("SwapRoom"))
            {
                targetItem = hit.collider.transform;
                navMeshAgent.SetDestination(targetItem.position);
            }
        }

        if (targetItem != null)
        {

            float distance = Vector2.Distance(transform.position, targetItem.position);
            if (distance <= reachDistance)
            {
                navMeshAgent.ResetPath();
                if (targetItem.tag == "Item")
                {
                    targetItem.GetComponent<Item>().OnPanel();
                }
                if (targetItem.tag == "SwapRoom")
                {
                    navMeshAgent.enabled = false;
                    targetItem.GetComponent<SwapRoom>().Swap();
                }
                targetItem = null;
                
            }
        }

    }
}
