public interface IPassiveData
{
    public string Index { get; }
    public string DisplayName { get; }
    public bool IsBase { get; }
    public int PointCost { get; }
    public Modifier Modifier { get; }
    public string[] IndexLinkedPassives { get; }
}