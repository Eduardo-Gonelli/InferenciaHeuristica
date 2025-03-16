using UnityEngine;

public class NPCDecisionTree : MonoBehaviour
{
    // Raiz da �rvore de decis�o
    private DecisionTreeNode root;
    private GameObject player;
    void Start()
    {
        // Obt�m a refer�ncia do jogador
        player = GameObject.FindGameObjectWithTag("Player");
        // Constr�i a �rvore de decis�o ao iniciar
        root = BuildDecisionTree();
    }

    void Update()
    {
        // Executa a decis�o raiz em cada frame
        root.MakeDecision();
    }

    // Constr�i a �rvore de decis�o e retorna o n� raiz
    private DecisionTreeNode BuildDecisionTree()
    {
        // N�s de a��o representando diferentes comportamentos do NPC
        ActionNode patrolAction = new ActionNode(Patrol); // A��o de patrulha
        ActionNode chaseAction = new ActionNode(Chase); // A��o de persegui��o
        ActionNode attackAction = new ActionNode(Attack); // A��o de ataque

        // N� de decis�o para verificar se o NPC est� perto o suficiente para atacar
        // Se n�o estiver, persegue o player.
        DecisionNode isCloseEnoughToAttack = new DecisionNode(IsCloseEnoughToAttack, attackAction, chaseAction);
        // N� de decis�o raiz para verificar se o jogador pode ser visto
        // Se pode ser visto, verifica se o NPC est� perto o suficiente para atacar.
        // Se n�o est� perto para atacar e nem pode ser visto, patrulha a �rea.
        DecisionNode canSeePlayerDecision = new DecisionNode(CanSeePlayer, isCloseEnoughToAttack, patrolAction);

        // Retorna o n� raiz da �rvore
        return canSeePlayerDecision;
    }

    // Verifica se o NPC pode ver o jogador baseado na dist�ncia
    private bool CanSeePlayer()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        return playerDistance < 10.0f; // Verdadeiro se a dist�ncia for menor que 10
    }

    // Verifica se o NPC est� perto o suficiente do jogador para atacar
    private bool IsCloseEnoughToAttack()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        return playerDistance < 3.0f; // Verdadeiro se a dist�ncia for menor que 3
    }

    // Define a a��o de patrulha do NPC
    private void Patrol()
    {
        Debug.Log("Patrulhando a �rea...");
        // Implementa��o da l�gica de patrulha
    }

    // Define a a��o de persegui��o do NPC
    private void Chase()
    {
        Debug.Log("Perseguindo o jogador!");
        // Implementa��o da l�gica de persegui��o
    }

    // Define a a��o de ataque do NPC
    private void Attack()
    {
        Debug.Log("Atacando o jogador!");
        // Implementa��o da l�gica de ataque
    }
}
