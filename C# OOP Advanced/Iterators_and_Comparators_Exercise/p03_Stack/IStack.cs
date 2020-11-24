namespace p03_Stack
{
    public interface IStack<T>
    {
        void Push(params T[] paramsArr);
        void Pop();
    }
}