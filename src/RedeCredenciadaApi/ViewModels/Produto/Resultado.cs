namespace RedeCredenciadaApi.ViewModels.Produto
{
    public class Resultado
    {
        public Produtos Produtos { get; set; }

        public Resultado()
        {
            Produtos = new Produtos();
        }
    }
}
