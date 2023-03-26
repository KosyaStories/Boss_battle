using System;

namespace MyStart
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();

            const int spell1 = 1;
            const int spell2 = 2;
            const int spell3 = 4;
            const int spelloff = 3;
            int heroDamage;
            int bossDamage;
            int heroHP = 50;
            const int heroMaxHP = 50;
            int bossHP = 100;
            const int bossMaxHP = 100;
            int heroMP = 20;
            const int heroMaxMP = 20;
            int spellCost = 10;
            int fireBall = 15;
            int fireStormChance = 0;
            int fireStorm = 20;
            int avadaKedavra = 30;
            int sharpClaw = 7;
            int butcherSheet = 10;
            int healPortal = 15;
            int portalChance = 0;
            int userChoiseSpell;
            int chanceBossSpell;

            Console.WriteLine("Да начнется бой с Боссом! Ниже представлены показатели жизней и маны.");

            for (int i = 0; i < int.MaxValue; i++)
            {
                while (heroHP > 1 && bossHP > 1)
                {
                    DrawBar($"Жизни Героя: {heroHP}", heroHP, heroMaxHP, ConsoleColor.Green, 0, 2);
                    DrawBar($"Мана Героя: {heroMP}", heroMP, heroMaxMP, ConsoleColor.Blue, 0,4);
                    DrawBar($"Жизни Босса: {bossHP}", bossHP, bossMaxHP, ConsoleColor.Red, 0, 6);

                    Console.SetCursorPosition(70, 8);
                    Console.Write("Заклинания: ");
                    Console.SetCursorPosition(70, 9);
                    Console.Write($"{spell1} - Fireball - {fireBall} урона. Стоимость {spellCost} маны.");
                    Console.SetCursorPosition(70, 10);
                    Console.Write($"{spell2} - Healing - + {healPortal} очков здоровья, Герой на ход прячется в портал и неуязвим. Стоимость {spellCost} маны.");
                    Console.SetCursorPosition(70, 12);
                    Console.Write($"{spelloff} - Не использовать заклинания.");
                    Console.SetCursorPosition(70, 13);
                    Console.Write($"{spell3} - Firestorm - {fireStorm} урона. Стоимость {spellCost} маны. Можно использовать, только если босс зацепил Героя острым когтем.");
                    Console.SetCursorPosition(70, 16);
                    Console.WriteLine("Если У Героя меньше 5 очков здоровья, он использует мощный удар.");
                    Console.SetCursorPosition(70, 18);
                    Console.WriteLine("Если У Босса меньше 15 очков здоровья, он использует мощный удар.");
                    Console.SetCursorPosition(70, 20);
                    Console.WriteLine("Если У Босса меньше 50 очков здоровья, он попытается нанести удар острым когтем.");
                    Console.SetCursorPosition(0, 8);

                    if (heroMP >= 10)
                    {
                        Console.Write("У Вас достаточно маны, выберите заклинание: ");
                        userChoiseSpell = Convert.ToInt32(Console.ReadLine());

                        switch (userChoiseSpell)
                        {
                            case spell1:
                                CastDestructiveSpell("Герой использует Fireball.", fireBall, ref bossHP, spellCost, ref heroMP);
                                break;

                            case spell2:
                                CastSavingSpell("Герой использует Healing.", healPortal, ref heroHP, spellCost, ref heroMP);
                                portalChance = 1;
                                break;

                            case spelloff:
                                break;

                            case spell3:
                                if (fireStormChance == 1)
                                {
                                    CastDestructiveSpell("Герой использует Firestorm.", fireStorm, ref bossHP, spellCost, ref heroMP);
                                    fireStormChance = 0;
                                }
                                else
                                {
                                    Console.WriteLine("Нет возможности для мести. Босс должен Вас задеть острым когтем.");
                                }
                                break;

                            default:
                                Console.WriteLine("Неверный выбор заклинания!");
                                break;

                        }
                    }

                    if (heroMP < 20)
                        heroMP++;

                    heroDamage = rand.Next(5, 10);
                    Console.WriteLine($"Урон Героя - {heroDamage}");
                    bossHP -= heroDamage;

                    if (portalChance == 1)
                    {
                        bossDamage = 0;
                        portalChance = 0;
                    }
                    else
                    {
                        bossDamage = rand.Next(1, 5);
                    }
                    Console.WriteLine($"Урон ББ - {bossDamage}");
                    heroHP -= bossDamage;

                    DrawBar($"Жизни Героя: {heroHP}", heroHP, heroMaxHP, ConsoleColor.Green, 0, 2);
                    DrawBar($"Мана Героя: {heroMP}", heroMP, heroMaxMP, ConsoleColor.Blue, 0, 4);
                    DrawBar($"Жизни Босса: {bossHP}", bossHP, bossMaxHP, ConsoleColor.Red, 0, 6);

                    if (heroHP < 0 || bossHP < 0)
                    {
                        break;
                    }

                    if (heroHP < 5)
                    {
                        Console.SetCursorPosition(0, 16);
                        Console.WriteLine("Вы на последнем исдыхании - Аааавада Кедавра!");
                        bossHP -= avadaKedavra;
                    }

                    DrawBar($"Жизни Героя: {heroHP}", heroHP, heroMaxHP, ConsoleColor.Green, 0, 2);
                    DrawBar($"Мана Героя: {heroMP}", heroMP, heroMaxMP, ConsoleColor.Blue, 0, 4);
                    DrawBar($"Жизни Босса: {bossHP}", bossHP, bossMaxHP, ConsoleColor.Red, 0, 6);

                    if (heroHP < 0 || bossHP < 0)
                    {
                        break;
                    }

                    if (bossHP < 50)
                    {
                        Console.SetCursorPosition(0, 13);
                        Console.WriteLine("Босс впал в ярость и попытается задеть Героя острым когтем.");
                        chanceBossSpell = rand.Next(1, 6);

                        switch (chanceBossSpell)
                        {
                            case 1:
                                Console.WriteLine("Промах!");
                                break;
                            case 2:
                                Console.WriteLine("Промах!");
                                break;
                            case 3:
                                Console.WriteLine("Оу, больно! У Героя появилась возможность отомстить огненным дождем.");
                                heroHP -= sharpClaw;
                                fireStormChance = 1;
                                break;
                            case 4:
                                Console.WriteLine("Промах!");
                                break;
                            case 5:
                                Console.WriteLine("Промах!");
                                break;
                        }
                    }
                    else if (bossHP < 15)
                    {
                        Console.SetCursorPosition(0, 16);
                        Console.WriteLine("Босс использует Супер-Удар - Разложение!");
                        heroHP -= butcherSheet;
                    }

                    DrawBar($"Жизни Героя: {heroHP}", heroHP, heroMaxHP, ConsoleColor.Green, 0, 2);
                    DrawBar($"Мана Героя: {heroMP}", heroMP, heroMaxMP, ConsoleColor.Blue, 0, 4);
                    DrawBar($"Жизни Босса: {bossHP}", bossHP, bossMaxHP, ConsoleColor.Red, 0, 6);

                    if (heroHP < 0 || bossHP < 0)
                    {
                        break;
                    }

                    Console.SetCursorPosition(0, 20);
                    Console.WriteLine("Нажмите 'Enter' для следующего раунда.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.SetCursorPosition(0, 22);

            if (heroHP < 1 && bossHP < 1)
                Console.WriteLine("Ничья.");
            else if (heroHP < 1)
                Console.WriteLine("Герой проиграл!");
            else if (bossHP < 1)
                Console.WriteLine("Босс повержен!");
        }

        static void DrawBar(string text, int value, int maxValue, ConsoleColor color, int positionY, int positionX, char symbol = '_')
        {
            Console.SetCursorPosition(positionY, positionX - 1);
            Console.Write(text);
            ConsoleColor defaultColor = Console.BackgroundColor;
            Console.SetCursorPosition(positionY, positionX);

            string bar = "";

            for (int i = 0; i < value; i++)
            {
                bar += symbol;
            }

            Console.Write('[');
            Console.BackgroundColor = color;
            Console.Write(bar);
            Console.BackgroundColor = defaultColor;

            bar = "";

            for (int i = value; i < maxValue; i++)
            {
                bar += symbol;
            }

            Console.Write(bar + ']');
        }

        static void CastDestructiveSpell(string text, int spell, ref int HP, int cost, ref int mana)
        {
            Console.WriteLine(text);
            HP -= spell;
            mana -= cost;
        }

        static void CastSavingSpell(string text, int spell, ref int HP, int cost, ref int mana)
        {
            Console.WriteLine(text);
            HP += spell;
            mana -= cost;
        }
    }
}
