using System;

namespace ORM
{
    [AttributeUsage(AttributeTargets.Property)]
    class CollumnAttribute : Attribute
    {
        public string Name { get; private set; }
        public bool IsAutoGenerate { get; set; }

        public CollumnAttribute(string name)
        {
            this.Name = name;
        }

    }
}
