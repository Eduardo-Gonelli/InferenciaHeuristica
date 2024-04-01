public class DecisionNode : DecisionTreeNode
{
    // Estas variáveis armazenam referências para os nós que representam os resultados "verdadeiro" e "falso" da decisão.
    public DecisionTreeNode trueNode;
    public DecisionTreeNode falseNode;

    // Variável que armazena uma função de teste. Esta função deve retornar um valor booleano.
    // System.Func<bool> é um delegado que representa um método que retorna um valor booleano e não recebe parâmetros.
    public System.Func<bool> test;

    // Construtor que inicializa o nó de decisão com a função de teste e os nós "verdadeiro" e "falso".
    public DecisionNode(System.Func<bool> test, DecisionTreeNode trueNode, DecisionTreeNode falseNode)
    {
        this.test = test; // Armazena a função de teste.
        this.trueNode = trueNode; // Armazena a referência para o nó a ser seguido se o teste for verdadeiro.
        this.falseNode = falseNode; // Armazena a referência para o nó a ser seguido se o teste for falso.
    }

    // Sobrescreve o método MakeDecision da classe base. Este método executa a função de teste e,
    // dependendo do resultado, prossegue para o nó "verdadeiro" ou "falso", chamando MakeDecision em um desses nós.
    public override DecisionTreeNode MakeDecision()
    {
        // Executa a função de teste. Se o resultado for verdadeiro, segue para o nó "verdadeiro".
        // Caso contrário, segue para o nó "falso".
        // A operação condicional ? : é usada para selecionar qual nó seguir com base no resultado da função de teste.
        return test() ? trueNode.MakeDecision() : falseNode.MakeDecision();
    }
}
