using SQLite;

namespace MauiForBeginners.models
{
    [Table("quotes")]
    public class SavedQuote
    {
        [PrimaryKey]
        public string Quote {  get; set; }
    }
}
