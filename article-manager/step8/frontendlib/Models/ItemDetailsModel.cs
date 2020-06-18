namespace frontendlib.Models
{
    public class ItemDetailsModel<TItem>
    {
        public string ItemName { get; set; }
        public TItem Item { get; set; }
    }
}