namespace Model.Data
{
    public interface ICanAddInInventory
    {
        bool TryAddInInventory(string id, int value);
    }
}
