using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpHw_4.Mediator
{
    // Класс менеджера в методе Send() проверяет, от кого пришло сообщение, 
    // и в зависимости от отправителя перенаправляет его другому объекту с 
    // помощью методов Notify(), определенных в классе Colleague.
    class ManagerMediator : Mediator
    {
        public Colleague Customer { get; set; }
        public Colleague Cook { get; set; }
        public Colleague Tester { get; set; }
        public override void Send(string msg, Colleague colleague)
        {
            // если отправитель - заказчик, значит есть новый заказ
            // отправляем сообщение повару - выполнить заказ
            if (Customer == colleague)
                Cook.Notify(msg);
            // если отправитель - повар, то можно приступать к дегустации
            // отправляем сообщение дегустатору
            else if (Cook == colleague)
                Tester.Notify(msg);
            // если отправитель - дегустатор, значит блюдо готов
            // отправляем сообщение заказчику
            else if (Tester == colleague)
                Customer.Notify(msg);
        }
    }

    abstract class Mediator
    {
        public abstract void Send(string msg, Colleague colleague);
    }
    abstract class Colleague
    {
        protected Mediator mediator;

        public Colleague(Mediator mediator)
        {
            this.mediator = mediator;
        }

        public virtual void Send(string message)
        {
            mediator.Send(message, this);
        }
        public abstract void Notify(string message);
    }
    // Класс заказчика
    class CustomerColleague : Colleague
    {
        public CustomerColleague(Mediator mediator)
            : base(mediator)
        { }

        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение заказчику: " + message);
        }
    }
    // Класс повара
    class CookColleague : Colleague
    {
        public CookColleague(Mediator mediator)
            : base(mediator)
        { }

        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение повару: " + message);
        }
    }
    // Класс дегустатора
    class TesterColleague : Colleague
    {
        public TesterColleague(Mediator mediator)
            : base(mediator)
        { }

        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение дегустатору: " + message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ManagerMediator mediator = new ManagerMediator();
            Colleague customer = new CustomerColleague(mediator);
            Colleague cook = new CookColleague(mediator);
            Colleague tester = new TesterColleague(mediator);
            mediator.Customer = customer;
            mediator.Cook = cook;
            mediator.Tester = tester;
            customer.Send("Есть заказ, надо приготовить блюдо");
            cook.Send("Блюдо готово, надо продегустировать");
            tester.Send("Блюдо продегустировано и готово к подаче на стол");

            Console.ReadLine();
        }
    }
}
