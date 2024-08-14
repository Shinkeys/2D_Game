using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bandit1 : MonoBehaviour
{
    public Transform target; // �����, � �������� ���� ������ ���������
    public float attackDistance = 2.0f; // ����������, �� ������� ���� �������� �����
    public float attackCooldown = 1.0f;

    private Animator animator; // ��������� ��������� �����
    private float lastAttackTime = 0f;
    private Rigidbody2D rb;
    private void Start()
    {
        animator = GetComponent < Animator>(); // �������� ��������� ���������
        rb = GetComponent < Rigidbody2D>();
    }

    private void Update()
    {
        if (target != null)
        {
            // ���������� ���������� ����� ������ � ������
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // ���� ����� � �������� �����, �� ���������
            if (distanceToTarget <= attackDistance && Time.time - lastAttackTime >= attackCooldown)
            {
                animator.SetTrigger("Attack");
                lastAttackTime = Time.time; // ��������� ����� �����
            }
        }
    }
}
