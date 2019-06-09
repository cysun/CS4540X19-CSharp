using System;

namespace EventExample
{
    class MockTextField
    {
        private string text = "";

        public void EnterText(string text)
        {
            this.text = text;
            if (EventHandlers1 != null) EventHandlers1(this.text);
            if (EventHandlers2 != null) EventHandlers2(this.text);
        }

        // Event Handling Using Delegate

        private Action<string> EventHandlers1;
        public void RegisterEventHandler(Action<string> eventHandler)
        {
            EventHandlers1 += eventHandler;
        }

        // Event Handling Using Event

        public event Action<string> EventHandlers2;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var textField = new MockTextField();
            textField.RegisterEventHandler(msg => Console.WriteLine("Handler1: {0}", msg));
            textField.RegisterEventHandler(msg => Console.WriteLine("Handler2: {0}", msg));

            textField.EventHandlers2 += msg => Console.WriteLine("Handler3: {0}", msg);
            textField.EventHandlers2 += msg => Console.WriteLine("Handler4: {0}", msg);

            Console.Write("Please enter some text: ");
            textField.EnterText(Console.ReadLine());

            /* The following line will cause a compliation error
             * because EventHandlers2 is declared as an event.
             */
            // textField.EventHandlers2 = null;
        }
    }
}
