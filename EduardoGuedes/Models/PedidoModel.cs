using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduardoGuedes.Models
{
    public class PedidoModel
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public int QtdProduto { get; set; }
        public decimal VlrUntProduto { get; set; }
        public decimal VlrTtlProduto 
        { 
            get { return QtdProduto * VlrUntProduto; } 
        }
    }
}