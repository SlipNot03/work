using System;

namespace GenericInterfaces
{
    interface IMessage
    {
        string Text { get; }
    }

    interface IPrintable
    {
        void Print();
    }

    class Message: IMessage, IPrintable
    {
        public string Text { get; }
        public void Print() => Console.WriteLine(Text);
        public Message(string text) => Text = text;
    }

    class Messanger<T> where T: IMessage, IPrintable
    {
        
        public void Send(T message)
        {
            Console.WriteLine("Отправка сообщений");
            message.Print();
        }
    }

    interface IUser<T>
    {
        T Id { get; }
    }

    class User<T>: IUser<T>
    {
        public T Id { get; }
        public User(T id) => Id = id;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var messanger = new Messanger<Message>();
            messanger.Send(new Message("Hello World!"));

            User<int> u = new User<int>(1);
            User<string> us = new User<string>("one");
            
        }
    }
}
