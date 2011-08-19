using System.Collections.Generic;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;

namespace F101.Scratchpad
{
    public class F101Registry : FubuRegistry
    {
        public F101Registry()
        {
            Applies
                .ToThisAssembly();

            this.Validation(validation =>
                                {
                                });

            Actions
                .IncludeTypesNamed(name => name.Equals(typeof (ActionCallStuff).Name))
                .IncludeMethods(c => c.Method.Name.Equals("DoSomething"))
                .FindWith<MyActionSource>();
        }
    }

    public class RegistryConsumer
    {
        public RegistryConsumer()
        {
            var graph = new F101Registry()
                .BuildGraph();

            graph
                .Behaviors
                .Each(chain =>
                          {
                          });

        }
    }


    public class MyUrlPolicy : IUrlPolicy
    {
        public bool Matches(ActionCall call, IConfigurationObserver log)
        {
            return call.Method.Name.Equals("DoSomething");
        }

        public IRouteDefinition Build(ActionCall call)
        {
            var route = call.ToRouteDefinition();
            route.Append("f101/dostuff");
            return route;
        }
    }

    public class MyActionSource : IActionSource
    {
        public IEnumerable<ActionCall> FindActions(TypePool types)
        {
            var call = ActionCall.For<ActionCallStuff>(c => c.DoSomething(new MyInputModel()));
            yield return call;
        }
    }

    public class ActionCallStuff
    {
        public ActionCallStuff()
        {
            var call = ActionCall.For<ActionCallStuff>(c => c.DoSomething(new MyInputModel()));
            var controller = call.HandlerType;
            var method = call.Method;

            var chain = new BehaviorChain();
            chain.AddToEnd(call);
        } 

        public MyOutputModel DoSomething(MyInputModel input)
        {
            return new MyOutputModel
                       {
                           Message = "Hello, {0}!".ToFormat(input.Name)
                       };
        }

        public MyOutputModel DoSomethingElse(MyOtherInputModel input)
        {
            return new MyOutputModel();
        }
    }

    public class MyOtherInputModel { }

    public class MyInputModel
    {
        public string Name { get; set; }
    }
    public class MyOutputModel
    {
        public string Message { get; set; }
    }
}