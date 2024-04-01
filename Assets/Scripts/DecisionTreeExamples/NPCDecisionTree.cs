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
        // A��es
        ActionNode patrolAction = new ActionNode(Patrol);
        ActionNode chaseAction = new ActionNode(Chase);
        ActionNode attackAction = new ActionNode(Attack);

        // Decis�es
        DecisionNode isCloseEnoughToAttack = new DecisionNode(IsCloseEnoughToAttack, attackAction, chaseAction);
        DecisionNode canSeePlayerDecision = new DecisionNode(CanSeePlayer, isCloseEnoughToAttack, patrolAction);

        return canSeePlayerDecision;
    }

    private bool CanSeePlayer()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        return playerDistance < 10.0f; // O NPC pode ver o jogador se estiver a uma dist�ncia menor que 10.
    }

    private bool IsCloseEnoughToAttack()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        return playerDistance < 3.0f; // O NPC est� perto o suficiente para atacar se a dist�ncia for menor que 3.
    }

    private void Patrol()
    {
        Debug.Log("Patrulhando a �rea...");
        // Sua l�gica de patrulha
    }

    private void Chase()
    {
        Debug.Log("Perseguindo o jogador!");
        // Sua l�gica de persegui��o
    }

    private void Attack()
    {
        Debug.Log("Atacando o jogador!");
        // Sua l�gica de ataque
    }
}
