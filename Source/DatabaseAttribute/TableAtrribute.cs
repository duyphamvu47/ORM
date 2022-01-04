using System;

namespace ORM
{
    [AttributeUsage(AttributeTargets.Class)]
    class TableAtrribute : Attribute
    {
        public string Name { get; private set; }

        public TableAtrribute(string name)
        {
            this.Name = name;
        }
    }
}
