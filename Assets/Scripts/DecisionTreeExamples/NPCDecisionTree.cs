using UnityEngine;

public class NPCDecisionTree : MonoBehaviour
{
    // Raiz da árvore de decisão
    private DecisionTreeNode root;
    private GameObject player;
    void Start()
    {
        // Obtém a referência do jogador
        player = GameObject.FindGameObjectWithTag("Player");
        // Constrói a árvore de decisão ao iniciar
        root = BuildDecisionTree();
    }

    void Update()
    {
        // Executa a decisão raiz em cada frame
        root.MakeDecision();
    }

    // Constrói a árvore de decisão e retorna o nó raiz
    private DecisionTreeNode BuildDecisionTree()
    {
        // Nós de ação representando diferentes comportamentos do NPC
        ActionNode patrolAction = new ActionNode(Patrol); // Ação de patrulha
        ActionNode chaseAction = new ActionNode(Chase); // Ação de perseguição
        ActionNode attackAction = new ActionNode(Attack); // Ação de ataque

        // Nó de decisão para verificar se o NPC está perto o suficiente para atacar
        // Se não estiver, persegue o player.
        DecisionNode isCloseEnoughToAttack = new DecisionNode(IsCloseEnoughToAttack, attackAction, chaseAction);
        // Nó de decisão raiz para verificar se o jogador pode ser visto
        // Se pode ser visto, verifica se o NPC está perto o suficiente para atacar.
        // Se não está perto para atacar e nem pode ser visto, patrulha a área.
        DecisionNode canSeePlayerDecision = new DecisionNode(CanSeePlayer, isCloseEnoughToAttack, patrolAction);

        // Retorna o nó raiz da árvore
        return canSeePlayerDecision;
    }

    // Verifica se o NPC pode ver o jogador baseado na distância
    private bool CanSeePlayer()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        return playerDistance < 10.0f; // Verdadeiro se a distância for menor que 10
    }

    // Verifica se o NPC está perto o suficiente do jogador para atacar
    private bool IsCloseEnoughToAttack()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        return playerDistance < 3.0f; // Verdadeiro se a distância for menor que 3
    }

    // Define a ação de patrulha do NPC
    private void Patrol()
    {
        Debug.Log("Patrulhando a área...");
        // Implementação da lógica de patrulha
    }

    // Define a ação de perseguição do NPC
    private void Chase()
    {
        Debug.Log("Perseguindo o jogador!");
        // Implementação da lógica de perseguição
    }

    // Define a ação de ataque do NPC
    private void Attack()
    {
        Debug.Log("Atacando o jogador!");
        // Implementação da lógica de ataque
    }
}
