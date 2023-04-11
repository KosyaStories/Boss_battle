using System;

namespace MyStart
{
    internal class Program
    {
        static void Main(string[] args)
        {          
            const int HeroMaxHealthPoints = 50;
            const int BossMaxHealthPoints = 100;
            const int HeroMaxManaPoints = 20;

            int heroHealthPoints = HeroMaxHealthPoints;
            int bossHealthPoints = BossMaxHealthPoints;
            int heroManaPoints = HeroMaxManaPoints;
            int spellCost = 10;
            int fireBall = 15;
            int fireStorm = 20;
            int healPortal = 15;
            bool fireStormChance = false;
            bool portalChance = false;

            Console.WriteLine("Да начнется бой с Боссом! Ниже представлены показатели жизней и маны.");

            FightBoss(ref heroHealthPoints, ref bossHealthPoints, ref heroManaPoints, HeroMaxHealthPoints, HeroMaxManaPoints, BossMaxHealthPoints, fireBall, spellCost,
            healPortal, fireStorm, ref portalChance, ref fireStormChance);

            ShowWinner(heroHealthPoints, bossHealthPoints);
        }

        static void FightBoss(ref int heroHP, ref int bossHP, ref int heroMP, int HeroMaxHP, int HeroMaxMP, int BossMaxHP, int fireBall, int spellCost,
            int healPortal, int fireStorm, ref bool portalChance, ref bool fireStormChance)
        {
            while (heroHP > 0 && bossHP > 0)
            {
                ShawDrawBar(heroHP, heroMP, bossHP, HeroMaxHP, HeroMaxMP, BossMaxHP);

                ShowMenuInfo(fireBall, spellCost, healPortal, fireStorm);

                TryUseSpell(ref heroMP, fireBall, ref bossHP, spellCost, healPortal, ref heroHP, ref portalChance, ref fireStormChance, fireStorm);

                ManaRecovery(ref heroMP);

                ExchangeOfBlows(ref bossHP, ref portalChance, ref heroHP);

                ShawDrawBar(heroHP, heroMP, bossHP, HeroMaxHP, HeroMaxMP, BossMaxHP);

                BlowDeathHero(heroHP, ref bossHP);

                ShawDrawBar(heroHP, heroMP, bossHP, HeroMaxHP, HeroMaxMP, BossMaxHP);

                ChanceBossSpell(bossHP, ref heroHP, ref fireStormChance);

                BlowDeathBoss(bossHP, ref heroHP);

                ShawDrawBar(heroHP, heroMP, bossHP, HeroMaxHP, HeroMaxMP, BossMaxHP);

                Console.SetCursorPosition(0, 20);
                Console.WriteLine("Нажмите 'Enter' для следующего раунда.");
                Console.ReadKey();
                Console.Clear();
            }
        }
        
        static void ShowMenuInfo(int fireBall, int spellCost, int healPortal, int fireStorm)
        {
            const int Spell1 = 1;
            const int Spell2 = 2;
            const int Spell3 = 4;
            const int Spelloff = 3;
            const int PositionY = 70;
            const int PositionX8 = 8;
            const int PositionX9 = 9;
            const int PositionX10 = 10;
            const int PositionX12 = 12;
            const int PositionX13 = 13;
            const int PositionX16 = 16;
            const int PositionX18 = 18;
            const int PositionX20 = 20;

            Console.SetCursorPosition(PositionY, PositionX8);
            Console.Write("Заклинания: ");
            Console.SetCursorPosition(PositionY, PositionX9);
            Console.Write($"{Spell1} - Fireball - {fireBall} урона. Стоимость {spellCost} маны.");
            Console.SetCursorPosition(PositionY, PositionX10);
            Console.Write($"{Spell2} - Healing - + {healPortal} очков здоровья, Герой на ход прячется в портал и неуязвим. Стоимость {spellCost} маны.");
            Console.SetCursorPosition(PositionY, PositionX12);
            Console.Write($"{Spelloff} - Не использовать заклинания.");
            Console.SetCursorPosition(PositionY, PositionX13);
            Console.Write($"{Spell3} - Firestorm - {fireStorm} урона. Стоимость {spellCost} маны. Можно использовать, только если босс зацепил Героя острым когтем.");
            Console.SetCursorPosition(PositionY, PositionX16);
            Console.WriteLine("Если У Героя меньше 5 очков здоровья, он использует мощный удар.");
            Console.SetCursorPosition(PositionY, PositionX18);
            Console.WriteLine("Если У Босса меньше 15 очков здоровья, он использует мощный удар.");
            Console.SetCursorPosition(PositionY, PositionX20);
            Console.WriteLine("Если У Босса меньше 50 очков здоровья, он попытается нанести удар острым когтем.");
            Console.SetCursorPosition(0, PositionX8);
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

        static void ShawDrawBar(int heroHP, int heroMP, int bossHP, int HeroMaxHP, int HeroMaxMP, int BossMaxHP)
        {
            const int PositionX2 = 2;
            const int PositionX4 = 4;
            const int PositionX6 = 6;
                        
            DrawBar($"Жизни Героя: {heroHP}", heroHP, HeroMaxHP, ConsoleColor.Green, 0, PositionX2);
            DrawBar($"Мана Героя: {heroMP}", heroMP, HeroMaxMP, ConsoleColor.Blue, 0, PositionX4);
            DrawBar($"Жизни Босса: {bossHP}", bossHP, BossMaxHP, ConsoleColor.Red, 0, PositionX6);
        }

        static bool TryUseSpell(ref int heroMP, int fireBall, ref int bossHP, int spellCost, int healPortal,
           ref int heroHP, ref bool portalChance, ref bool fireStormChance, int fireStorm)
        {
            const int Spell1 = 1;
            const int Spell2 = 2;
            const int Spell3 = 4;
            const int Spelloff = 3;

            if (CheckManaPoint(heroMP))
            {
                Console.Write("У Вас достаточно маны, выберите заклинание: ");

                switch (GetNumber())
                {
                    case Spell1:
                        CastDestructiveSpell("Герой использует Fireball.", fireBall, ref bossHP, spellCost, ref heroMP);
                        break;

                    case Spell2:
                        CastSavingSpell("Герой использует Healing.", healPortal, ref heroHP, spellCost, ref heroMP, ref portalChance);
                        break;

                    case Spelloff:
                        Console.WriteLine("Заклинание не используется.");
                        break;

                    case Spell3:
                        FireStormSpellActivate(ref fireStormChance, fireStorm, ref bossHP, spellCost, ref heroMP);
                        break;

                    default:
                        Console.WriteLine("Неверный выбор заклинания!");
                        break;
                }

                return true;
            }
            else
            {
                Console.WriteLine("Заклинание не используется.");
                return false;
            }
        }
        
        static void CastDestructiveSpell(string text, int spell, ref int HP, int cost, ref int mana)
        {
            Console.WriteLine(text);
            HP -= spell;
            mana -= cost;
        }

        static void CastSavingSpell(string text, int spell, ref int HP, int cost, ref int mana, ref bool portalChance)
        {
            Console.WriteLine(text);
            HP += spell;
            mana -= cost;
            portalChance = true;
            int HeroMaxHP = 50;

            if (HP > HeroMaxHP)
            {
                HP = HeroMaxHP;
            }
        }
        
        static void FireStormSpellActivate(ref bool fireStormChance, int fireStorm, ref int bossHP, int spellCost, ref int heroMP)
        {
            if (fireStormChance)
            {
                CastDestructiveSpell("Герой использует Firestorm.", fireStorm, ref bossHP, spellCost, ref heroMP);
                fireStormChance = false;
            }
            else
            {
                Console.WriteLine("Нет возможности для мести. Босс должен Вас задеть острым когтем.");
            }
        }

        static bool CheckManaPoint(int manapoint)
        {
            return manapoint >= 10;
        }

        static void ManaRecovery(ref int heroMP)
        {
            if (heroMP < 20)
                heroMP++;
        }

        static void ExchangeOfBlows(ref int bossHP, ref bool portalChance, ref int heroHP)
        {
            Random rand = new Random();

            int bossDamage;
            int heroDamage = rand.Next(5, 10);
            Console.WriteLine($"Урон Героя - {heroDamage}");
            bossHP -= heroDamage;

            if (portalChance)
            {
                bossDamage = 0;
                portalChance = false;
            }
            else
            {
                bossDamage = rand.Next(1, 5);
            }

            Console.WriteLine($"Урон ББ - {bossDamage}");
            heroHP -= bossDamage;
        }

        static void BlowDeathHero(int heroHP, ref int bossHP)
        {
            int avadaKedavra = 30;

            if (heroHP < 5 && heroHP > 0)
            {
                Console.SetCursorPosition(0, 16);
                Console.WriteLine("Вы на последнем исдыхании - Аааавада Кедавра!");
                bossHP -= avadaKedavra;
            }
        }

        static void ChanceBossSpell(int bossHP, ref int heroHP, ref bool fireStormChance)
        {
            Random random = new Random();
            int chanceBossSpell;
            int sharpClaw = 7;

            if (bossHP < 50 && bossHP > 0)
            {
                Console.SetCursorPosition(0, 13);
                Console.WriteLine("Босс впал в ярость и попытается задеть Героя острым когтем.");
                
                chanceBossSpell = random.Next(1, 6);

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
                        fireStormChance = true;
                        break;
                    case 4:
                        Console.WriteLine("Промах!");
                        break;
                    case 5:
                        Console.WriteLine("Промах!");
                        break;
                }
            }
        }
        
        static void BlowDeathBoss(int bossHP, ref int heroHP)
        {
            int butcherSheet = 10;

            if (bossHP < 15 && bossHP > 0)
            {
                Console.SetCursorPosition(0, 16);
                Console.WriteLine("Босс использует Супер-Удар - Разложение!");
                heroHP -= butcherSheet;
            }
        }

        static void ShowWinner(int heroHP, int bossHP)
        {
            Console.SetCursorPosition(0, 22);

            if (heroHP < 0 && bossHP < 0)
                Console.WriteLine("Ничья.");
            else if (heroHP < 0)
                Console.WriteLine("Герой проиграл!");
            else if (bossHP < 0)
                Console.WriteLine("Босс повержен!");
        }
        
        static int GetNumber()
        {
            bool isWorking = true;
            int number = 0;

            while (isWorking)
            {
                if (int.TryParse(Console.ReadLine(), out number))
                {
                    isWorking = false;
                }
                else
                {
                    Console.WriteLine("Число не принято, попробуйте еще раз.");
                    isWorking = true;
                }
            }

            return number;
        }
    }
}
