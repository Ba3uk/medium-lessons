using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2_5
{
    public interface IProperty { }
    public class Product
    {
        public string Name { get; private set; }
        public int Price { get; private set; }

        public Product(string name, int price, params IProperty[] properties)
        {
            Name = name;
            Price = price;
            _properties = properties.ToList();
        }

        private List<IProperty> _properties = new List<IProperty>();

        public IEnumerable<IProperty> GetPropertys { get => _properties; }

        public bool ContainsProperties(IProperty property)
        {
           return _properties.Contains(property);
        }

        public void AddProperties(IProperty property)
        {
            if (!ContainsProperties(property))
            {
                _properties.Add(property);
            }
        }
    }

    public class Shop
    {
        private List<Product> _products = new List<Product>();

        public IEnumerable<Product> GetPropertys { get => _products; }
    }
}
