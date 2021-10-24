using System.Collections;
using System.Collections.Generic;
using LetterSpace;
using UserSpace;

namespace CollectionOfUsers
{
    delegate List<User> SortingAlgorithmDelegate(List<User> colection);

    partial class UsersCollection 
    {
        // ключ - пользователь, значение - список писем
        private static Dictionary<User, List<Letter>> _dictionary;
        public static Dictionary<User, List<Letter>> Dictionary 
        {
            get => new Dictionary<User, List<Letter>>(_dictionary);
        }

        // статический к-ор для инициализации словаря 
        static UsersCollection() => _dictionary = new Dictionary<User, List<Letter>>();

        public static void AddUser(User user) => _dictionary.Add(user, user.Letters);

        // индексатор, возвращает копию письма пользователя по указанному индексу
        public Letter this[User user, int letterIndex]
        {
            get => new Letter(_dictionary [user][letterIndex]);
        }

        // индексатор, возвращает копию списка писем указанного пользователя
        public List<Letter> this[User user]
        {
            get => new List<Letter>(_dictionary[user]);
        }

        // итератор по пользователям
        public IEnumerator GetEnumerator() => _dictionary.Keys.GetEnumerator();

        /*
         * сортировка пользователей с возвратом отсортированного списка 
         * по алгоритму заданным поздним связыванием
        */
        public static List<User> SpecificUsersSort(SortingAlgorithmDelegate sorting)
        {
            return sorting(new List<User>(_dictionary.Keys));
        }
    }
}
