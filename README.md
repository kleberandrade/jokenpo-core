# jokenpo-core

Classes para criação de um jogo de Jokenpo (Pedra - Papel - Tesoura - Lagarto - Spock). Criado no dia 24/09/2015 durante a disciplina de Estudos Avançados em Jogos Digitais do cursos de Jogos Digitais da FATEC-AM.

Criando os jogadores

```csharp
IJokenpoHand player = new JokenpoHand();
IJokenpoHand playerAI = new JokenpoHandAI();
```

Definindo sua jogada

```csharp
player.Hand = JokenpoHandType.Paper;
```

Computador fazendo sua jogada

```csharp
((JokenpoHandAI)playerAI).Next();
```

Verificando quem ganhou

```csharp
int result = JokenpoRules.Compare(player, playerAI);
```

Os possíveis resultados são:
* -1 -> Você perdeu
*  0 -> O jogo empatou
*  1 -> Você ganhou