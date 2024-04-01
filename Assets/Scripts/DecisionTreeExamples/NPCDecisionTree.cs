using UnityEngine;

public class NPCDecisionTree : MonoBehaviour
{
    private DecisionTreeNode root;
    private GameObject player;

    void Start()
    {
        root = BuildDecisionTree();
    }

    void Update()
    {
        player = GameObject.FindWithTag("Player");
        root.MakeDecision();
    }

    private DecisionTreeNode BuildDecisionTree()
    {
        // Ações
        ActionNode patrolAction = new ActionNode(Patrol);
        ActionNode chaseAction = new ActionNode(Chase);
        ActionNode attackAction = new ActionNode(Attack);

        // Decisões
        DecisionNode isCloseEnoughToAttack = new DecisionNode(IsCloseEnoughToAttack, attackAction, chaseAction);
        DecisionNode canSeePlayerDecision = new DecisionNode(CanSeePlayer, isCloseEnoughToAttack, patrolAction);

        return canSeePlayerDecision;
    }

    private bool CanSeePlayer()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        return playerDistance < 10.0f; // O NPC pode ver o jogador se estiver a uma distância menor que 10.
    }

    private bool IsCloseEnoughToAttack()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        return playerDistance < 3.0f; // O NPC está perto o suficiente para atacar se a distância for menor que 3.
    }

    private void Patrol()
    {
        Debug.Log("Patrulhando a área...");
        // Sua lógica de patrulha
    }

    private void Chase()
    {
        Debug.Log("Perseguindo o jogador!");
        // Sua lógica de perseguição
    }

    private void Attack()
    {
        Debug.Log("Atacando o jogador!");
        // Sua lógica de ataque
    }
}
