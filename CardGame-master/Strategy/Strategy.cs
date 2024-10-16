﻿using CardGame.GameEngine;
using CardGame.Sources;
using CardGame.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Strategy
{

    public interface ISelectionStrategy
    {
        bool Select(int Score);
        string Print();
    }

    /// <summary>
    /// Стратегия для рандомной игры дилера
    /// </summary>
    public class RandomDealerStrategy : ISelectionStrategy
    {
        public string Print()
        {
            return "Random";
        }

        public bool Select(int Score)
        {
            Random random = new Random();
            return random.Next(1,10) % 2 == 0;
        }

    }

    /// <summary>
    /// Стратегия при которой дилер играет максимально осторожно
    /// </summary>
    public class DefenciveDealerStrategy : ISelectionStrategy
    {
        public string Print()
        {
            return "Defencive";
        }

        public bool Select(int Score)
        {

            Console.WriteLine("Dealer's Turn:");

            while (Score < 14)
            {
                //Console.WriteLine("Decision: Not draw a card");
                return true;
            }
            return false;

        }
    }
    /// <summary>
    /// Стратегия при которой дилер играет максимально агрессивно
    /// </summary>
    public class AgressiveDealerStrategy : ISelectionStrategy
    {

        public string Print()
        {
            return "Agressive";
        }

        public bool Select(int Score)
        {

            while (Score < 18)
            {

                //Console.WriteLine("Decision: Draw a card");
                return true;
            }
            return false;
        }
    }
}
/// <summary>
/// Стратегия монте-карло для дилера
/// </summary>
public class MonteCarloSelectStrategy : ISelectionStrategy
{
    public ISelectionStrategy strategy;
    public BlackjackGame engine;
    bool ISelectionStrategy.Select(int Score)
    {
        return AnalizeMonteCarlo(Score) > 70;
    }

    public int AnalizeMonteCarlo(int score)
    {

        int numberOfIterations = 10000;

        int monteCarloWins = 0;


        for (int i = 0; i < numberOfIterations; i++)
        {
            Console.WriteLine($"Iteration: {i + 1}");
            List<Card> deckCopy = new List<Card>();
            engine.ShuffleDeck();
            engine.Deck.ForEach(
                x => deckCopy.Add(new Card(x.Rank)));
            int dealerScore = 0;

            List<Card> dealerCards = new List<Card>();
            while (strategy.Select(dealerScore))
            {
                dealerCards.Add(deckCopy[0]);
                deckCopy.RemoveAt(0);
                dealerScore = engine.CalculateScore(dealerCards);

            }
            Console.WriteLine($"Dealer Score: {dealerScore}, monteCarloWins: {monteCarloWins}");
            Console.WriteLine($"Remaining cards: {deckCopy.Count}");


            if (dealerScore > 21 || dealerScore <= score)
            {
                monteCarloWins++;
            }

        }

        return monteCarloWins;

    }

    string ISelectionStrategy.Print()
    {
        return "BestSelectStrategy";
    }
}



