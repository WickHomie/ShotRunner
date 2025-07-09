public interface IDataProvider
{
    void Save();

    bool TryLoad();

    public void ResetSave();
}
