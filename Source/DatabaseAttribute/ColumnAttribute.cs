using System;

namespace ORM.DatabaseAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    class ColumnAttribute : Attribute
    {
        public string Name { get; private set; }
        public bool IsAutoGenerate { get; set; }

        public ColumnAttribute(string name)
        {
            this.Name = name;
        }

    }
}
