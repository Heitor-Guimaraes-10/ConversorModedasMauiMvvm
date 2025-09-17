namespace ConversorModedasMauiMvvm.Models
{
    // A classe `RateTable` é a nossa "tabela de câmbio".
    // Ela armazena as taxas de conversão e contém a lógica para realizar o cálculo.
    public class RateTable
    {
        // Este é um dicionário privado que armazena as taxas de câmbio em relação ao BRL (Real Brasileiro).
        // A chave (string) é o código da moeda (ex: "USD"), e o valor (decimal) é a taxa.
        // O `M` no final dos números indica que são valores decimais (tipo `decimal`).
        private readonly Dictionary<string, decimal>
            _toBRL = new()
            {
                ["BRL"] = 1.00M,
                ["USD"] = 5.60m,
                ["EUR"] = 6.10m
            };

        // Esta propriedade pública expõe o dicionário de taxas, mas de forma segura.
        // O `IReadOnlyDictionary` permite que outras classes leiam as taxas, mas não as modifiquem,
        // garantindo que os dados permaneçam consistentes.
        public IReadOnlyDictionary<string, decimal>
            ToBRL => _toBRL;

        // Este método retorna uma lista de todos os códigos de moeda suportados ("BRL", "USD", "EUR").
        // Ele ordena os códigos alfabeticamente, o que é ótimo para preencher um `Picker` (dropdown) na UI.
        public IEnumerable<string> GetCurrencies()
            => _toBRL.Keys.OrderBy(k => k);

        // Este método de validação simples verifica se um código de moeda existe na nossa tabela.
        // Isso previne erros se alguém tentar converter uma moeda não suportada.
        public bool Supports(string code)
            => _toBRL.ContainsKey(code);

        // Este é o método principal que realiza a conversão.
        // Ele recebe o valor (`amount`), a moeda de origem (`from`) e a moeda de destino (`to`).
        public decimal Convert (decimal amount, string from, string to) 
        {
            // Valida se as moedas de origem e destino são suportadas.
            // Se qualquer uma delas não for, retorna 0 para indicar que a conversão não é possível.
            if (!Supports(from)|| !Supports(to)) return 0m;
            
            // Se a moeda de origem e destino são a mesma, não há necessidade de conversão.
            // Retorna o valor original para evitar cálculos desnecessários.
            if (from == to) return amount;  

            // A lógica de conversão é feita em duas etapas usando o BRL como moeda intermediária:
            // 1. Converte o valor original para BRL.
            var brl = amount * _toBRL[from];

            // 2. Converte o valor em BRL para a moeda de destino.
            return brl / _toBRL[to];
        }
    }
}
