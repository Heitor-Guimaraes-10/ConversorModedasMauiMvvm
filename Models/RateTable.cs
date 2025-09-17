namespace ConversorModedasMauiMvvm.Models
{
     public class RateTable
    {
        private readonly Dictionary<string, decimal>
            _toBRL = new()
            {
                ["BRL"] = 1.00M,
                ["USD"] = 5.60m,
                ["EUR"] = 6.10m
            };

        public IReadOnlyDictionary<string, decimal>
            ToBRL => _toBRL;

        public IEnumerable<string> GetCurrencies()
            => _toBRL.Keys.OrderBy(k => k);

        public bool Supports(string code)
            => _toBRL.ContainsKey(code);

        public decimal Convert (decimal amount, string from, string to) 
        {
        if (!Supports(from)|| !Supports(to)) return 0m;
        if (from == to) return amount;  


        var brl = amount * _toBRL[from];

            return brl / _toBRL[to];
        }
    }
}
