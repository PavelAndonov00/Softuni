namespace p01_ListyIterator
{
    public interface IListyIterator<T>
    {
        bool Move();
        bool HasNext();
        void Print();
    }
}