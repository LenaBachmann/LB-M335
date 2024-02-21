using SQLite;
using MauiForBeginners.models;

namespace MauiForBeginners
{
    public class QuoteRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteConnection conn;
        private void Init()
        {
            if (conn != null)
            {
                return;
            }
            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<SavedQuote>();
        }

        public QuoteRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddNewQuote(string quote)
        {

            int result = 0;
            Init();

            var existingQuote = conn.Table<SavedQuote>().FirstOrDefault(q => q.Quote == quote);

            if (existingQuote == null)
            {
                conn.Insert(new SavedQuote { Quote = quote });
            }

        }

        public List<SavedQuote> GetAllQuotes()
        {
            try
            {
                Init();
                return conn.Table<SavedQuote>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<SavedQuote>();
        }
    }
}
