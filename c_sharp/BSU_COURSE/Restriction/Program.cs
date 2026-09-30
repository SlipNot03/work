using System;

namespace Restriction
{
    interface IMessage
    {
        public string Text { get; }
    }

    
    class Message: IMessage
    {
        public string Text { get; } // текст сообщения
        public Message(string text)
        {
            Text = text;
        }
    }

    class Email: Message
    {
        public Email(string Text) : base(Text)
        {

        }

    }

    class Sms : Message
    {
        public Sms(string Text): base(Text)
        {

        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            SendMessage(new Sms("Текст"));
        }

        public static void SendMessage<T>(T message) where T: IMessage //ограничение на конкретный интерфейс
        {
            Console.WriteLine($"Сообщение: {message.Text}");
        }
    }
}
