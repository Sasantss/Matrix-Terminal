
# Matrix RPG

## Sobre o Jogo

Matrix RPG é um jogo de RPG baseado em console desenvolvido em C#. Ele apresenta uma loja para compra de personagens, diferentes níveis de jogabilidade e um sistema de combate. O jogo incorpora conceitos como encapsulamento, herança e polimorfismo.

## Funcionalidades

* **Seleção de Personagens**: Escolha entre personagens da Matrix como Neo, Trinity e Morpheus, cada um com atributos únicos.
* **Loja no Jogo**: Compre novos personagens usando moedas do jogo.
    * Preços de personagens: Neo (2000 moedas), Trinity (1500 moedas), Morpheus (600 moedas).
* **Múltiplos Níveis**:
    * **Nível 1 - Iniciante**: Um jogo estilo "galeria de tiro" onde você derrota agentes para ganhar moedas e subir de nível.
    * **Nível 2 - Intermediário**: Um desafio de descriptografia onde você deve inserir a chave correta para desbloquear o próximo nível, com uma chance de bônus para Trinity.
    * **Nível 3 - Avançado**: Um desafio de memória onde você deve lembrar e inserir sequências de códigos para desarmar uma bomba.
* **Status do Jogador**: Acompanhe sua saúde, nível, moedas e inventário.
* **Encapsulamento**: A saúde do jogador é encapsulada com uma propriedade para garantir que seja sempre um valor não negativo.
* **Herança**: Neo, Trinity e Morpheus são subclasses que herdam da classe `Player`.
* **Polimorfismo**: O método `ActivateBonus()` é sobrescrito na classe `Trinity` para fornecer um bônus único (ganha 500 de saúde adicional).

## Como Jogar

1.  **Clone o Repositório**:
    ```bash
    git clone  
    ```
2.  **Navegue até o Diretório do Projeto**:
    ```bash
    cd NovoGame/GameRpg
    ```
3.  **Execute o Jogo**:
    ```bash
    dotnet run
    ```

## Jogabilidade

Ao iniciar o jogo, você será apresentado a um menu principal:

* **Jogar**: Inicia um novo jogo ou continua seu progresso pelos níveis.
* **Sair**: Sai do jogo.
* **Manual**: Exibe os controles do jogo e os objetivos dos níveis.
* **Loja**: Permite comprar novos personagens com suas moedas.
* **Personalizar Personagem**: Permite escolher um personagem desbloqueado para jogar.
* **Detalhes do Personagem**: Exibe as estatísticas e o inventário do seu personagem atual.

### Controles (Nível 1)

* `↑` (Seta para Cima): Move o jogador para cima.
* `↓` (Seta para Baixo): Move o jogador para baixo.
* `Espaço`: Atira.

## Estrutura do Projeto

* `Program.cs`: Contém a lógica central do jogo, incluindo as classes `Game`, `Player`, `Shop` e as classes de personagens (Neo, Trinity, Morpheus), juntamente com métodos para os níveis do jogo, navegação do menu e funções utilitárias.
* `obj/`: Contém artefatos de construção intermediários.
* `bin/`: Contém o executável compilado e arquivos relacionados.

## Requisitos

* .NET SDK 8.0 ou superior.


