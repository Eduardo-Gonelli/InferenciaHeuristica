public class ActionNode : DecisionTreeNode
{
    // Declara uma variável do tipo System.Action.
    // Esta variável pode conter uma referência a qualquer método que não tenha parâmetros e não retorne um valor.
    public System.Action action;

    // Construtor que recebe um método do tipo System.Action como parâmetro e o armazena na variável action.
    public ActionNode(System.Action action)
    {
        this.action = action;
    }

    // Sobrescreve o método MakeDecision da classe base DecisionTreeNode.
    // Quando chamado, executa o método referenciado por action usando Invoke().
    // Se action for null (não apontar para nenhum método), o operador ?. evita a chamada do método, prevenindo erros de NullReferenceException.
    // Retorna a própria instância do nó de ação, permitindo encadeamento ou terminação de decisões na árvore.
    public override DecisionTreeNode MakeDecision()
    {
        action?.Invoke(); // Tenta invocar a ação, se houver uma definida.
        return this; // Retorna este mesmo nó, indicando que a ação foi realizada e não há mais decisões a serem tomadas.
    }
}
