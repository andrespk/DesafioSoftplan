using System;

namespace App2.Infra.IoC
{
    public class Attributes
    {
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public class InjectsTransientAttribute : Attribute
        {
        }

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public class InjectsScopedAttribute : Attribute
        {
        }

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public class InjectsSingletonAttribute : Attribute
        {
        }
    }
}