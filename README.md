# jokenpo-core

Classes para criação de um jogo de Jokenpo (Pedra - Papel - Tesoura - Lagarto - Spock). Criado no dia 24/09/2015 durante a disciplina de Estudos Avançados em Jogos Digitais do cursos de Jogos Digitais da FATEC-AM.

## Jokenpo

Classes criadas para o projeto. Faça um Build da DLL e use em seus jogos dentro da Unity 3D.

![alt tag](http://prntscr.com/8kdyu7)

## JokenpoSample

Exemplo de jogo no modo ConsoleApplication

![alt tag](http://prntscr.com/8kdwxn)

## Como Programar

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