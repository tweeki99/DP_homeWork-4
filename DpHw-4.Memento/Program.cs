using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpHw_4.Memento
{
    // Originator
    // Класс героя имеет состояние, которое может со временем
    // меняться. Он также объявляет методы SaveState() и RestoreState().
    // С их помощью объект Hero может сохранить свое состояние 
    // в HeroMemento, а в будущем его восстановить.
    class Hero
    {
        private int potions = 10; // кол-во зелий
        private int healthPoints = 5; // кол-во HP

        public void Drink()
        {
            if (potions > 0)
            {
                potions--;
                healthPoints += 10;
                Console.WriteLine("Пью зелье лечения.");
                Console.WriteLine("НР - {0}", healthPoints);
                Console.WriteLine("Зелий - {0}\n", potions);
            }
            else
                Console.WriteLine("Зелий больше нет");
        }
        // сохранение состояния
        public HeroMemento SaveState()
        {
            Console.WriteLine("Сохранение игры. Параметры: {0} зелий, {1} НР\n", potions, healthPoints);
            return new HeroMemento(potions, healthPoints);
        }

        // восстановление состояния
        public void RestoreState(HeroMemento memento)
        {
            this.potions = memento.Patrons;
            this.healthPoints = memento.Lives;
            Console.WriteLine("Восстановление игры. Параметры: {0} зелий, {1} НР\n", potions, healthPoints);
        }
    }

    class HeroMemento
    {
        public int Patrons { get; private set; }
        public int Lives { get; private set; }

        public HeroMemento(int patrons, int lives)
        {
            this.Patrons = patrons;
            this.Lives = lives;
        }
    }

    // Класс GameHistory предназначен для хранения состояний, 
    // причем все состояния хранятся в стеке, что позволяет 
    // с легкостью извлекать последнее сохраненное состояние.
    class GameHistory
    {
        public Stack<HeroMemento> History { get; private set; }
        public GameHistory()
        {
            History = new Stack<HeroMemento>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Hero hero = new Hero();
            hero.Drink(); // пьем зелье, осталось 9
            GameHistory game = new GameHistory();

            game.History.Push(hero.SaveState()); // сохраняем игру

            hero.Drink(); //пьем зелье, осталось 8

            hero.RestoreState(game.History.Pop()); // востанавливаем игру

            hero.Drink(); //пьем зелье, осталось 8

            Console.ReadLine();
        }
    }
}
