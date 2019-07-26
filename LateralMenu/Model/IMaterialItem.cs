namespace LateralMenu.Model
{
    public interface IMaterialItem
    {
        IMaterialContainer ParentContainer { get; }

        void SetParent(IMaterialContainer container);
    }
}
