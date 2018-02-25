/*
//singleton
//Одиночка (англ. Singleton) — порождающий шаблон проектирования,
//гарантирующий, что в однопроцессном приложении будет единственный
//экземпляр некоторого класса, и предоставляющий глобальную точку 
//доступа к этому экземпляру.
using System;

namespace ConsoleApplication3
{
    public sealed class Singleton
    {
        private static readonly Lazy<Singleton> instanceHolder = 
            new Lazy<Singleton>(() => new Singleton());

        private Singleton()
        {
            
        }

        public static Singleton Instance
        {
            get { return instanceHolder.Value; }
        }
    }
}*/