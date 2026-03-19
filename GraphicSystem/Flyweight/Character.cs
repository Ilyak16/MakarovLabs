using GraphicSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicSystem.Flyweight
{
    public class Character : ICharacter
    {
        // Внутреннее состояние (неизменяемое, общее для всех)
        private char _symbol;
        private string _font;
        private string _color;

        public Character(char symbol, string font, string color)
        {
            _symbol = symbol;
            _font = font;
            _color = color;
        }

        public void Display(int x, int y)
        {
            Console.WriteLine($"[Flyweight] Символ '{_symbol}' ({_font}, {_color}) отрисован в координатах [{x}, {y}]");
        }
    }

    // Фабрика приспособленцев
    public class CharacterFactory
    {
        private Dictionary<string, ICharacter> _characters = new Dictionary<string, ICharacter>();

        public ICharacter GetCharacter(char symbol, string font, string color)
        {
            string key = $"{symbol}-{font}-{color}";

            if (!_characters.ContainsKey(key))
            {
                Console.WriteLine($"[Flyweight] Создание нового объекта для '{symbol}'...");
                _characters[key] = new Character(symbol, font, color);
            }
            else
            {
                Console.WriteLine($"[Flyweight] Использование существующего объекта для '{symbol}' (Кэширование)");
            }

            return _characters[key];
        }

        public int GetTotalObjectsCreated() => _characters.Count;
    }
}
