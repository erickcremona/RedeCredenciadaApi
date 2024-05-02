using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Produto
{
    public class ProdutoViewModelResponse
    {
        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        [JsonPropertyName("ordem")]
        public string Ordem { get; set; }
    }
}
