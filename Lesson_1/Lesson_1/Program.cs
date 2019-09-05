using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1_3
{
    class Bag
    {
        public List<Item> Items;
        public int MaxWeidth { private set; get; }

        public void AddItem(string name, int count)
        {
            int currentWeidth = Items.Sum(item => item.Count);
            Item targetItem = Items.FirstOrDefault(item => item.Name == name);

            if (targetItem == null)
                throw new InvalidOperationException();

            if (currentWeidth + count > MaxWeidth)
                throw new InvalidOperationException();

            targetItem.Count += count;
        }
    }

    class Item
    {
        private int _count;

        public int Count
        {
            set
            {
                _count = value;
                if (_count < 0)
                    _count = 0;
            }

            get { return _count; }
        }
        public string Name { private set; get; }
    }
}
