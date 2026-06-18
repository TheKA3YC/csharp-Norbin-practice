using System.Collections;

internal class Program
{
    private static void Main()
    {
        var stack = new SmartStack<int>();
        stack.Push(10);
        stack.Push(20);
        stack.Push(30);

        Console.WriteLine($"Стек создан. Элементов: {stack.Count}, Емкость: {stack.Capacity}");

        var extendItems = new[] { 40, 50, 60 };
        stack.PushRange(extendItems);
        Console.WriteLine($"Добавил коллекцию. Теперь элементов: {stack.Count}");

        Console.WriteLine("\nЭлементы стека (от вершины к основанию):");
        foreach (var item in stack)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine($"Элемент на глубине 0: {stack[0]}");
        Console.WriteLine($"Элемент на глубине {stack.Count - 1}: {stack[stack.Count - 1]}");

        var pop = stack.Pop();
        Console.WriteLine($"\nВзял с вершины: {pop}");
        Console.WriteLine($"Новая вершина: {stack.Peek()}");
    }
}

public class SmartStack<T> : IEnumerable<T>
{
    private T[] _items;
    private int _count;
    public SmartStack()
    {
        _items = new T[4];
        _count = 0;
    }

    public SmartStack(int capacity)
    {
        if(capacity >= 0)
        {
            _items = new T[capacity];
            _count = 0;
        }
        else
        {
            throw new ArgumentOutOfRangeException("Ёмкость должна быть положительной");
        }
    }

    public SmartStack(IEnumerable<T> collection)
    {
        if (collection == null)
        {
            throw new ArgumentNullException("Коллекция должна существовать");
        }

        var collectionCapacity = 0;
        foreach (var item in collection)
        {
            collectionCapacity++;
        }

        _items = new T[collectionCapacity];
        _count = 0;

        foreach(var item in collection)
        {
            Push(item);
        }
    }

    public void Push(T item) 
    {
        if (_items.Length == _count)
        {
            int newCapacity;
            if (_items.Length == 0)
            {
                newCapacity = 4;
            }
            else
            {
                newCapacity = _items.Length * 2;
            }
            var newItems = new T[newCapacity];

            for(var i = 0; i < _count; i++)
            {
                newItems[i] = _items[i];
            }

            _items = newItems;
        }

        _items[_count] = item;
        _count++;
    }

    public void PushRange(IEnumerable<T> collection)
    {
        if (collection == null)
        {
            throw new ArgumentNullException("Коллекция должна существовать");
        }

        var collectionCapacity = 0;
        foreach (var item in collection)
        {
            collectionCapacity++;
        }

        int newCapacity;
        if (collectionCapacity == 0)
        {
            return;
        }

        else if(collectionCapacity + _count > _items.Length)
        {
            if(_items.Length == 0)
            {
                newCapacity = 4;
            }
            else
            {
                newCapacity = _items.Length * 2;
            }

            while(newCapacity < _count + collectionCapacity)
            {
                newCapacity *= 2;
            }

            var newItems = new T[newCapacity];

            for (var i = 0; i < _count; i++)
            {
                newItems[i] = _items[i];
            }

            _items = newItems;
        }
        
        foreach (var item in collection)
        {
            _items[_count] = item;
            _count++;
        }
    }

    public T Pop()
    {
        if(_count == 0)
        {
            throw new InvalidOperationException("Стек пуст");
        }

        var lastItem = _items[_count - 1];
        _items[_count - 1] = default;
        _count--;

        return lastItem;
    }

    public T Peek()
    {
        if (_count == 0)
        {
            throw new InvalidOperationException("Стек пуст");
        }

        var lastItem = _items[_count - 1];

        return lastItem;
    }

    public bool Contains(T item)
    {
        for(var i = 0; i < _count; i++)
        {
            if (object.Equals(_items[i], item))
            {
                return true;
            }
        }

        return false;
    }

    public int Count
    {
        get
        {
            return _count;
        }
    }

    public int Capacity
    {
        get
        {
            return _items.Length;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var i = _count-1; i >= 0; i--)
        {
            yield return _items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public T this[int depth]
    {
        get
        {
            if (depth < 0 || depth >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(depth), "Глубина выходит за границы стека.");
            }

            return _items[_count - 1 - depth];
        }
        set
        {
            if (depth < 0 || depth >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(depth), "Глубина выходит за границы стека.");
            }

            _items[_count - 1 - depth] = value;
        }
    }
}