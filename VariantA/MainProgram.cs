using System;

namespace VariantA
{
    class KeyEventArgs : EventArgs
    {
        public char ch;
    }

    delegate void KeyHandler(object source, KeyEventArgs arg);

    class KeyEvent
    {
        public event KeyHandler KeyPress;
        
        public void OnKeyPress(char key)
        {
            var eventArgs = new KeyEventArgs();

            if (KeyPress is not null)
            {
                eventArgs.ch = key;
                KeyPress(this, eventArgs);
            }
        }
    }

    class MainProgram
    {
        public static void Main()
        {
            var keyEvent = new KeyEvent();
            char key;
            var counter = 0;

            // Вместо отдельных классов использовано лямбды 
            keyEvent.KeyPress += (source, arg) => counter++;
            keyEvent.KeyPress += (source, arg) => Console.WriteLine("Получено сообщение о нажатии клавиши: " + arg.ch);

            Console.WriteLine("Введите несколько символов. Для останова введите точку.");

            do
            {
                key = (char)Console.Read();
                keyEvent.OnKeyPress(key);
            }
            while (key != '.');

            Console.WriteLine("Было нажато " + counter + " клавиш.");
        }
    }
}
