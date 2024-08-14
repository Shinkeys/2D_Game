using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bandit1 : MonoBehaviour
{
    public Transform target; // Герой, к которому враг должен атаковать
    public float attackDistance = 2.0f; // Расстояние, на котором враг начинает атаку
    public float attackCooldown = 1.0f;

    private Animator animator; // Компонент аниматора врага
    private float lastAttackTime = 0f;
    private Rigidbody2D rb;
    private void Start()
    {
        animator = GetComponent < Animator>(); // Получаем компонент аниматора
        rb = GetComponent < Rigidbody2D>();
    }

    private void Update()
    {
        if (target != null)
        {
            // Определите расстояние между врагом и героем
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // Если герой в пределах атаки, то атаковать
            if (distanceToTarget <= attackDistance && Time.time - lastAttackTime >= attackCooldown)
            {
                animator.SetTrigger("Attack");
                lastAttackTime = Time.time; // Запомните время атаки
            }
        }
    }
}
