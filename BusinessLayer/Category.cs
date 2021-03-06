using System;
using System.Drawing;
using Wallets.DataStorage;

namespace Wallets.BusinessLayer
{
    public class Category : EntityBase, IComparable<Category>, IStorable
    {
        private string _name;
        private string _description;
        private Color _color;
        private string _icon;

        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public Color Color { get => _color; set => _color = value; }
        public string Icon { get => _icon; set => _icon = value; }

        public Guid Guid { get; }

        public Category(Color color, string name = "Default name", string description = "Default description", 
            string icon = "default.png")
        {
            Name = name;
            Description = description;
            Color = color;
            Icon = icon;
            Guid = Guid.NewGuid();
        }

        public override bool Validate()
        {
            return !String.IsNullOrWhiteSpace(Name) &&
                !String.IsNullOrWhiteSpace(Description);
        }

        public override string ToString()
        {
            return $"Category \"{Name}\"\nDescription: {Description}\n" +
                $"Color: {Color}\nIcon: {Icon}";
        }

        public int CompareTo(Category other)
        {
            if (other == null)
                return 1;

            return Name.CompareTo(other.Name);
        }

        public Category Copy()
        {
            return new Category(Color, Name, Description, Icon);
        }
    }
}
