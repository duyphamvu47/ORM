using System;

namespace ORM.DatabaseAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    class TableAttribute : Attribute
    {
        public string Name { get; private set; }

        public TableAttribute(string name)
        {
            this.Name = name;
        }
    }
}
