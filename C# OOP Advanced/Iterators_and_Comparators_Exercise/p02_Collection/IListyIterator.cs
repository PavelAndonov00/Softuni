namespace p02_Collection
{
    public interface IListyIterator<T>
    {
        bool Move();
        bool HasNext();
        void Print();
    }
}