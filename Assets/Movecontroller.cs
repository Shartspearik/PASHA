using UnityEngine;
using UnityEngine.AI;

public class Movecontroller : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform targetItem;

    // ����������, �� ������� �������, ��� ����� ������� � ��������
    public float reachDistance = 1.5f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    void Update()
    {
        // ��������� ������ ������ ����
        if (UnityEngine.Input.GetMouseButtonDown(1)) // ���
        {

            // ����������� ������� ������� � ���������� ���� 2D
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

            // ��������� 2D Raycast, ��� "���������" ������� ��������, ����� �������� ������ �����
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null)
            {
                NavMeshHit navHit;

                Vector3 targetPosition = new Vector3(hit.point.x, hit.point.y, transform.position.z);

                // ���������, ���� �� ����� �� NavMesh � ������� 2 ������
                if (NavMesh.SamplePosition(targetPosition, out navHit, 2.0f, NavMesh.AllAreas))
                {
                    navMeshAgent.SetDestination(navHit.position);
                }
                else
                {
                    Debug.Log("���� � ����� ������ ������");
                }
            }
        }



        // ���� ���� � 2D - ����������� �������� ������� � ���
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
