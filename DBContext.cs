namespace StroySite.WPF
{
    public static class DBContext
    {
        private static AppDbContext _context;

        public static AppDbContext GetContext()
        {
            if (_context == null)
                _context = new AppDbContext();
            return _context;
        }
    }
}