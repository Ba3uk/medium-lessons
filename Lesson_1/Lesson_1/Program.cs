using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2_5
{
    public interface ICost
    {
        int GetPrice { get; }
    }

    public abstract class SmartList<T>
    {
        public delegate bool Сorresponds(T value);

        protected List<T> _values { get; set; }

        public IEnumerable<T> GetValues { get => _values; }

        protected T GetElement(Сorresponds del)
        {
            foreach (var value in _values)
            {
                if (del.Invoke(value))
                    return value;
            }

            throw new Exception();
        }
    }

    public class Product:ICost
    {
        private static int _currentId { get; set; }
        public static int CurrentId { get => _currentId++; private set => _currentId = value; }

        public int Id { get; private set; }
        public string Name { get; private set; }
        private int _price { get; set; }

        public int GetPrice => _price;

        public Product(string name, int price)
        {
            Id = CurrentId;
            Name = name;
            _price = price;
        }
    }

    public class Discount
    {
        private static int _currentId { get; set; }
        public static int CurrentId { get => _currentId++; private set => _currentId = value; }

        public int Id { get; private set; }
        public int Percent { get; private set; }

        public Discount(int percent)
        {
            Percent = percent;
        }
    }

    public class ProductList : SmartList<Product>
    {
        public ProductList(params Product[] products)
        {
            _values = products.ToList();
        }

        public Product GetProduct(int id)
        {
            return GetElement((val) => val.Id == id);
        }
    }

    public class DiscountList : SmartList<Discount>
    {
        public DiscountList(params Discount[] discount)
        {
            _values = discount.ToList();
        }

        public Discount GetDiscount(int id)
        {
            return GetElement((val) => val.Id == id);
        }
    }

    public class Shop
    {
        private Dictionary<Product, Discount> _discountProducts;

        public DiscountList DiscountList { get; private set; }
        public ProductList ProductList { get; private set; }

        private void ReceivingInformation()
        {
            ReceivingProducts();
            ReceivingDiscounts();
            ReceivingDiscountProducts();
        }

        public bool CanDelivered(Product product) => !_discountProducts.ContainsKey(product);
        public bool IsDiscountProduct(Product product) => !_discountProducts.ContainsKey(product);

        private void ReceivingProducts()
        {
            DiscountList = new DiscountList
                (
                new Discount(10),
                new Discount(20),
                new Discount(30),
                new Discount(40),
                new Discount(50)
                );
        }

        private void ReceivingDiscounts()
        {
            ProductList = new ProductList
                (
                new Product("Телега1", 10),
                new Product("Телега2", 20),
                new Product("Телега3", 30),
                new Product("Телега4", 40),
                new Product("Телега5", 50)
                );
        }

        private void ReceivingDiscountProducts()
        {
            _discountProducts = new Dictionary<Product, Discount>();
            _discountProducts.Add(ProductList.GetProduct(1), DiscountList.GetDiscount(1));
            _discountProducts.Add(ProductList.GetProduct(3), DiscountList.GetDiscount(4));
        } 
    }
}
