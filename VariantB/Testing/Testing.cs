using System;
using UserSpace;
using LetterSpace;
using CollectionOfUsers;
using System.Collections.Generic;

namespace VariantB
{
    class Testing
    {
        static void Main(string[] args)
        {
            User userAndrey = new User("Andrey", "Syradoev", new DateTime(2001, 12, 31));
            User userSony = new User("Sony", "Podmogilnay", new DateTime(2002, 4, 10));
            User userIvan = new User("Ivan", "Ivanov", new DateTime(2005, 2, 3));
            User userDima = new User("Dima", "Pogrib", new DateTime(2000, 9, 17));
            User userRoman = new User("Roman", "Romanov", new DateTime(1989, 7, 1));

            Letter letter1 = new Letter("Congratulations", "Happy Birthday!");
            Letter letter2 = new Letter("Work", "When will the report be submitted?");
            Letter letter3 = new Letter("Concert", "Are you going to go to the concert tonight?");
            Letter letter4 = new Letter("Study", "Can you share your homework?");
            Letter letter5 = new Letter("Study", "Link to videoconference");

            /* 
             * (Проверка работы события)
             * После каждой отправки письма Романом, будет выводиться строка именем получателя
             */
            userRoman.LetterSent += (source, args) => Console.WriteLine("Roman sent letter to " + args.Letter.Recipient.Name);

            // Теперь после отправки еще будет выводиться строка, с подтверждением успеха
            userRoman.LetterSent += (source, args) => Console.WriteLine("Email sent successfully!");

            Console.WriteLine("Проверка работы событий." + "\n");

            userAndrey.SendLetter(userSony, letter1);
            userDima.SendLetter(userIvan, letter2);
            userIvan.SendLetter(userAndrey, letter3);
            userRoman.SendLetter(userDima, letter4);
            // Направить письмо заданного человека с заданной темой всем адресатам.
            userRoman.SendToAll(letter5);

            Console.WriteLine(new string('*', 40));

            CallMainMethods(userSony, userAndrey);

            CallMethodsWithDelegates();
        }

        private static void CallMethodsWithDelegates()
        {
            /* Проверка правильности работы передачи делегата сортировки пользователей
             * Методы в SortingAlgorithms сортируют в алфавитном порядке по фамилиям
             * Но также можно и определить другую логику сортировки пользователей (например по именам или длинам писем)
             */
            List<User>[] arrayOfSortedLists = new List<User>[4];

            // сортировка list.Sort
            arrayOfSortedLists[0] = UsersCollection.SpecificUsersSort(SortingAlgorithms.DefSorting);
            // Гномья сортировка
            arrayOfSortedLists[1] = UsersCollection.SpecificUsersSort(SortingAlgorithms.GnomeSort);
            // Сортировка Шелла
            arrayOfSortedLists[2] = UsersCollection.SpecificUsersSort(SortingAlgorithms.ShellSort);
            // Болотная (случайная) сортировка
            arrayOfSortedLists[3] = UsersCollection.SpecificUsersSort(SortingAlgorithms.BogoSort);

            Console.WriteLine("Проверка работы делегатов." + "\n");

            // Вывод отсортированных списков
            foreach (var list in arrayOfSortedLists)
            {
                foreach (var item in list)
                {
                    Console.WriteLine(item.Surname);
                }

                Console.WriteLine();
            }
        }

        private static void CallMainMethods(User userSony, User userAndrey)
        {
            // Найти пользователя, длина писем которого наименьшая.
            Console.WriteLine("Пользователь, длина писем которого наименьшая" + "\n");
            Console.WriteLine(UsersCollection.FindUserWithTheShortesLetter());
            Console.WriteLine(new string('*', 40));

            // Вывести информацию о пользователях, а также количестве полученных и отправленных ими письмах.
            Console.WriteLine("Информация о пользователях, а также количестве полученных и отправленных ими письмах." + "\n");
            Console.WriteLine(UsersCollection.GetUsersInfo());
            Console.WriteLine(new string('*', 40));

            // Вывести информацию о пользователях, которые получили хотя бы одно сообщение с заданной темой.
            Console.WriteLine("Информацию о пользователях, которые получили хотя бы одно сообщение с заданной темой." + "\n");
            Console.WriteLine(UsersCollection.GetUsersWithSuchTopic("Work"));
            Console.WriteLine(new string('*', 40));

            // Вывести информацию о пользователях, которые не получали сообщения с заданной темой.
            Console.WriteLine("Информация о пользователях, которые не получали сообщения с заданной темой." + "\n");
            Console.WriteLine(UsersCollection.GetUsersWithoutSuchTopic("Study"));
            Console.WriteLine(new string('*', 40));

            // проверка работы индексаторов
            UsersCollection usersCollection = new UsersCollection();

            // вывод самого первого письма указанного пользователя
            Console.WriteLine("Индексаторы" + "\n");
            Console.WriteLine(usersCollection[userSony][0] + "\n");
            Console.WriteLine(new string('*', 40));

            // получить список писем указанного пользователя
            var list = usersCollection[userAndrey];

            foreach (var letter in list)
            {
                Console.WriteLine(letter + "\n");
            }
            Console.WriteLine(new string('*', 40));

            Console.WriteLine("Обработка исклюений." + "\n");
            // использование механизма обработки исключительных ситуаций
            try
            {
                User notValidUser = new User("123", "   ", new DateTime());
            }
            catch
            {
                Console.WriteLine("Error!");
            }
            Console.WriteLine(new string('*', 40) + "\n");

            Console.WriteLine("Итератор" + "\n");
            // проверка работы итератора
            foreach (User item in usersCollection)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(new string('*', 40) + "\n");
        }
    }
}
