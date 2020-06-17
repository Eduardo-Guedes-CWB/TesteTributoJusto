using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduardoGuedes.Models
{
    public class ProdutoPedidoModel
    {
        public ProdutoModel Produto { get; set; }
        public int QtdProduto { get; set; }
        public decimal VlrUntProduto { get; set; }
        public decimal VlrTtlProduto { get { return QtdProduto * VlrUntProduto;}}
    }
}