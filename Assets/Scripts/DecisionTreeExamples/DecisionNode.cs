public class DecisionNode : DecisionTreeNode
{
    // Estas vari�veis armazenam refer�ncias para os n�s que representam os resultados "verdadeiro" e "falso" da decis�o.
    public DecisionTreeNode trueNode;
    public DecisionTreeNode falseNode;

    // Vari�vel que armazena uma fun��o de teste. Esta fun��o deve retornar um valor booleano.
    // System.Func<bool> � um delegado que representa um m�todo que retorna um valor booleano e n�o recebe par�metros.
    public System.Func<bool> test;

    // Construtor que inicializa o n� de decis�o com a fun��o de teste e os n�s "verdadeiro" e "falso".
    public DecisionNode(System.Func<bool> test, DecisionTreeNode trueNode, DecisionTreeNode falseNode)
    {
        this.test = test; // Armazena a fun��o de teste.
        this.trueNode = trueNode; // Armazena a refer�ncia para o n� a ser seguido se o teste for verdadeiro.
        this.falseNode = falseNode; // Armazena a refer�ncia para o n� a ser seguido se o teste for falso.
    }

    // Sobrescreve o m�todo MakeDecision da classe base. Este m�todo executa a fun��o de teste e,
    // dependendo do resultado, prossegue para o n� "verdadeiro" ou "falso", chamando MakeDecision em um desses n�s.
    public override DecisionTreeNode MakeDecision()
    {
        // Executa a fun��o de teste. Se o resultado for verdadeiro, segue para o n� "verdadeiro".
        // Caso contr�rio, segue para o n� "falso".
        // A opera��o condicional ? : � usada para selecionar qual n� seguir com base no resultado da fun��o de teste.
        return test() ? trueNode.MakeDecision() : falseNode.MakeDecision();
    }
}
