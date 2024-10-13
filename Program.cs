using Xadrez.Draw;
using Xadrez.Enemie;
using Xadrez.Business;
using Xadrez.User;

Console.WriteLine("XADREZ");

var tabuleiro = new Board();

/*
    matriz do tabuleiro:
        * 8 espaço (vertical 1-8)
        * 8 peças (horizontal a-h)
    logica:
        * turnos
        * ganhar perder
            * travar as peças
            * acabar as peças
            * o rei for tomado
        * logica das peças
        * peao no final
    
    classes:
        * desenhar tabuleiro
        * tratar o input do jogador
        * peças
            * proliferação
        * logica e regras
        * "IA" do oponente
*/

tabuleiro.DrawTable();
