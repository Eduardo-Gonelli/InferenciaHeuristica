public class ActionNode : DecisionTreeNode
{
    // Declara uma vari�vel do tipo System.Action.
    // Esta vari�vel pode conter uma refer�ncia a qualquer m�todo que n�o tenha par�metros e n�o retorne um valor.
    public System.Action action;

    // Construtor que recebe um m�todo do tipo System.Action como par�metro e o armazena na vari�vel action.
    public ActionNode(System.Action action)
    {
        this.action = action;
    }

    // Sobrescreve o m�todo MakeDecision da classe base DecisionTreeNode.
    // Quando chamado, executa o m�todo referenciado por action usando Invoke().
    // Se action for null (n�o apontar para nenhum m�todo), o operador ?. evita a chamada do m�todo, prevenindo erros de NullReferenceException.
    // Retorna a pr�pria inst�ncia do n� de a��o, permitindo encadeamento ou termina��o de decis�es na �rvore.
    public override DecisionTreeNode MakeDecision()
    {
        action?.Invoke(); // Tenta invocar a a��o, se houver uma definida.
        return this; // Retorna este mesmo n�, indicando que a a��o foi realizada e n�o h� mais decis�es a serem tomadas.
    }
}
