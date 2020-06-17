using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduardoGuedes.Models
{
    public class PedidoModel
    {
        public int IdPedido { get; set; }
        public ClienteModel Cliente { get; set; }
        public List<ProdutoPedidoModel> LstProdutos { get; set; }
    }
}