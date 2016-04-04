using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace BFInterpreter_2._0.Core.CompositionRoot
{
    public class Bootstrapper
    {
        public static ContainerBuilder Builder { get; private set; }

        public static IContainer Container { get; private set; }

        public static void InitializeBuilder()
        {
            Builder = new ContainerBuilder();
        }

        public static bool IsBuilded { get; set; }

        public static void SetAutofacContainer()
        {
            if (!IsBuilded)
            {
                Container = Builder.Build();
                IsBuilded = true;
            }
            else
            {
                Builder.Update(Container);
            }
        }

        public static T GetService<T>(string name = null)
        {
            return string.IsNullOrEmpty(name) ? Container.Resolve<T>() : Container.ResolveNamed<T>(name);
        }
    }
}
