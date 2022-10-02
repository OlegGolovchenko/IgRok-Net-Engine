using IGNEngine;
using IGNEngine.ValidationRules.StringRules;
using System;

namespace Launcher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ValidationEngine engine = new ValidationEngine();
            engine.AddRuleChainFor("e-mail");
            engine.AddRuleFor<StringNullOrEmptyRule>("e-mail");
            engine.AddRuleFor<StringMaxLengthRule>("e-mail", 254);
            engine.AddRuleFor<StringContainsRule>("e-mail", "@");
            if (engine.ValidateChain("e-mail", "test@test.tst"))
            {
                Console.WriteLine("email valid");
            }
            Console.WriteLine("Hello World!");
        }
    }
}
