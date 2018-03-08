using System;
using System.Net;

namespace MagicApp
{
    public class MyHttpListener
    {
        public void TryHttp(string _login = "", string _password = "", string addr = "https://jira.esphere.ru/")
        {
            HttpWebRequest _request = (HttpWebRequest) WebRequest.Create(addr);
            _request.Credentials = new NetworkCredential(_login, _password);
            HttpWebResponse _response = (HttpWebResponse) _request.GetResponse();
            WebHeaderCollection _headers = _response.Headers;
            for (int i = 0; i < _headers.Count; i++)
            {
                Console.WriteLine("{0}: {1}", _headers.GetKey(i), _headers[i]);
            }
            Console.Beep();
            Console.ReadKey();
        }
    }

    class Animal
    {
        public string Name { get; set; }

        public virtual void GetRoar()
        {
            Console.WriteLine("Абстрактный клич");
        }

    }

    class Cat : Animal
    {
        public override void GetRoar()
        {
            Console.WriteLine("Мяф...");
        }
    }

    class Dog : Animal
    {
        
    }

    class Wolf : Animal
    {
        
    }

    class Alkash : Animal
    {
        public override void GetRoar()
        {
            Console.WriteLine("Ауууээээ , кто нассал в подъезде?! ...");
        }
    }

/*    class Animals
    {
        public void AnimalsHere
        { Animal [] animals = new Animal [4];
            Cat cat = new Cat();
        cat.Name = "Barsik";
        Dog dog = new Dog();
        dog.Name = "Tyzik";
        Wolf wolf = new Wolf();
        wolf.Name = "Seriy";
        Alkash alkash = new Alkash();
        alkash.Name = "Dadya Vitya";
        animals[0] = cat;
        animals[1] = dog;
        animals[2] = wolf;
        animals[3] = alkash;

        for (int i = 0; i<animals.Length;
        i++)
        {
            Console.WriteLine("Животное - {0} говорит: ", animals[i].Name);
            animals[i].GetRoar();
        }
        Alkash boozer = new Alkash();
        Animal ani = boozer;
        ani.Name = "another alkash";
        ani.GetRoar();
        Console.ReadKey();
    }
}*/

    public class Kitchen
    {
        public string Subject { get; set; }
        public Kitchen()
        {
            Subject = "Yes";
        }
        
    }

    public class Bake : Kitchen
    {
        public string Bakes { get; set; }
        public Bake() : base ()
        {   
            Console.WriteLine("в запекании  Используется конструктор родительского класса kitchen, т.к. действо происходлит на кухне");
            Bakes = "Yes";
        }
    }

    public class Mikrowave : Bake
    {
        public string Option { get; set; }

        public Mikrowave(string name, string option) : base ()
        {
            Option = option;
            Console.WriteLine("Получим {0} {1}", name, Option);
        }

    }

    public class Subject
    {
        public string Name { get; set; }  // may be ID
        public string Warrior { get; set; } //Yes or no
        public int HP { get; set; }  //
        protected Subject()
        {
            Name = "None";
            Warrior = "None";
            HP = 0;
        }
    }

    public class Human : Subject
    {
        public Human() : base()
        {
            Console.WriteLine("creation null object - it will be mistake");
        }

        /// <summary>
        /// <param name="name">any</param>
        /// <param name="warrior">y/n</param>
        /// <param name="hp">number of hitpoints</param>
        /// </summary>
        public Human(string name = "none", string warrior = "none", int hp = 100)
        {
            Name = name;
            Warrior = warrior;
            HP = hp;
            Console.WriteLine($"Creating human with name =  {0}, warior =  {1}, and hit points {2}", name, warrior, hp);
        }
    }

    public class mainProgram
    {
        public static void loool(string[] args)
        {
            Mikrowave actionMikrowave = new Mikrowave(" *BRAAAAAINS* ", " Запечёные ");
            Console.WriteLine($"Запекает? {actionMikrowave.Bakes} \nНаходится на Кухне? {actionMikrowave.Subject}");
            Human Vasya = new Human("Vasiliy", "Yes", 123);
        }
    }
}
