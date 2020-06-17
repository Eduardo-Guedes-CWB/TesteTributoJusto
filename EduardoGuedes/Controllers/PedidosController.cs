using EduardoGuedes.Data;
using EduardoGuedes.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduardoGuedes.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult Index()
        
        {
            List<PedidoModel> lstPedidos = new List<PedidoModel>();
            SqlConnection conexao = DataBaseAccess.ConexaoDB();
            SqlCommand scpPedidos = new SqlCommand(@"SELECT 
	                                                pdd.id_pedido,
	                                                clt.id_cliente,
	                                                clt.nome_cliente
                                                FROM 
	                                                pedidos pdd
	                                                INNER JOIN clientes clt 
		                                                ON clt.id_cliente = pdd.id_cliente;", conexao);
            try
            {
                conexao.Open();
                SqlDataReader pedidos = scpPedidos.ExecuteReader();
                if (pedidos.HasRows)
                {
                    while (pedidos.Read())
                    {
                        PedidoModel pedido = new PedidoModel();
                        pedido.IdPedido = pedidos.GetInt32(0);
                        pedido.Cliente = new ClienteModel { IdCliente = pedidos.GetInt32(1), NomeCliente = pedidos.GetString(2) };
                        List<ProdutoPedidoModel> lstProdutos = new List<ProdutoPedidoModel>();
                        SqlCommand scpItensPedidos = new SqlCommand($@"SELECT
	                                                                    pdt.id_produto,
	                                                                    pdt.des_produto,
	                                                                    ipdd.qtd_produto,
	                                                                    ipdd.vlr_unit
                                                                    FROM
	                                                                    itens_pedido ipdd
	                                                                    INNER JOIN produtos pdt
		                                                                    ON pdt.id_produto = ipdd.id_produto
                                                                    WHERE
                                                                        ipdd.id_pedido = {pedidos.GetInt32(0)};",conexao);
                        SqlDataReader itensPedidos = scpItensPedidos.ExecuteReader();
                        if (itensPedidos.HasRows)
                        {
                            while (itensPedidos.Read())
                            {
                                ProdutoPedidoModel produtoPedido = new ProdutoPedidoModel();
                                produtoPedido.Produto = new ProdutoModel {IdProduto = itensPedidos.GetInt32(0), DesProduto = itensPedidos.GetString(1)};
                                produtoPedido.QtdProduto = itensPedidos.GetInt32(2);
                                produtoPedido.VlrUntProduto = itensPedidos.GetDecimal(3);
                                lstProdutos.Add(produtoPedido);
                            }
                            pedido.LstProdutos = lstProdutos;
                            scpItensPedidos.Dispose();                            
                        }
                        lstPedidos.Add(pedido);
                    }
                    scpPedidos.Dispose();
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                conexao.Dispose();
                conexao.Close();               
            }
            return View(lstPedidos);
        }

        // GET: Pedidos/Details/5
        public ActionResult Read(int id)
        {

            PedidoModel pedido = new PedidoModel();
            SqlConnection conexao = DataBaseAccess.ConexaoDB();
            SqlCommand scpPedido = new SqlCommand($@"SELECT 
	                                                    pdd.id_pedido,
	                                                    clt.id_cliente,
	                                                    clt.nome_cliente
                                                    FROM 
	                                                    pedidos pdd
	                                                    INNER JOIN clientes clt 
		                                                    ON clt.id_cliente = pdd.id_cliente
                                                    WHERE
                                                        pdd.id_pedido = {id};", conexao);
            try
            {
                conexao.Open();
                SqlDataReader sdrPedido = scpPedido.ExecuteReader();
                if (sdrPedido.HasRows)
                {
                    while (sdrPedido.Read())
                    {
                        pedido.IdPedido = sdrPedido.GetInt32(0);
                        pedido.Cliente = new ClienteModel { IdCliente = sdrPedido.GetInt32(1), NomeCliente = sdrPedido.GetString(2) };
                        List<ProdutoPedidoModel> lstProdutos = new List<ProdutoPedidoModel>();
                        SqlCommand scpItensPedido = new SqlCommand($@"SELECT
	                                                                    pdt.id_produto,
	                                                                    pdt.des_produto,
	                                                                    ipdd.qtd_produto,
	                                                                    ipdd.vlr_unit
                                                                    FROM
	                                                                    itens_pedido ipdd
	                                                                    INNER JOIN produtos pdt
		                                                                    ON pdt.id_produto = ipdd.id_produto
                                                                    WHERE
                                                                        ipdd.id_pedido = {id};", conexao);
                        SqlDataReader sdrItensPedido = scpItensPedido.ExecuteReader();
                        if (sdrItensPedido.HasRows)
                        {
                            while (sdrItensPedido.Read())
                            {
                                ProdutoPedidoModel produtoPedido = new ProdutoPedidoModel();
                                produtoPedido.Produto = new ProdutoModel { IdProduto = sdrItensPedido.GetInt32(0), DesProduto = sdrItensPedido.GetString(1) };
                                produtoPedido.QtdProduto = sdrItensPedido.GetInt32(2);
                                produtoPedido.VlrUntProduto = sdrItensPedido.GetDecimal(3);
                                lstProdutos.Add(produtoPedido);
                            }
                            pedido.LstProdutos = lstProdutos;
                            scpItensPedido.Dispose();
                        }                        
                    }
                    scpPedido.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexao.Dispose();
                conexao.Close();
            }
            return View(pedido);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            List<ClienteModel> lstClientes = new List<ClienteModel>();
            List<ProdutoModel> lstProdutos = new List<ProdutoModel>();
            SqlConnection conexao = DataBaseAccess.ConexaoDB();
            SqlCommand scpClientes = new SqlCommand(@"SELECT * FROM clientes;",conexao);
            SqlCommand scpProdutos = new SqlCommand(@"SELECT * FROM produtos;",conexao);
            try
            {
                conexao.Open();
                SqlDataReader sdrClientes = scpClientes.ExecuteReader();
                if (sdrClientes.HasRows)
                {
                    while (sdrClientes.Read())
                    {
                        lstClientes.Add(new ClienteModel{ IdCliente = sdrClientes.GetInt32(0), NomeCliente = sdrClientes.GetString(1) });
                    }
                }
                sdrClientes.Dispose();

                SqlDataReader sdrProdutos = scpProdutos.ExecuteReader();
                if (sdrProdutos.HasRows)
                {
                    while (sdrProdutos.Read())
                    {
                        lstProdutos.Add(new ProdutoModel { IdProduto = sdrProdutos.GetInt32(0), DesProduto = sdrProdutos.GetString(1) });
                    }
                }
                scpProdutos.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexao.Dispose();
                conexao.Close();
            }

            ViewBag.Clientes = new SelectList(lstClientes, "IdCliente", "NomeCliente");
            ViewBag.Produtos = new SelectList(lstProdutos, "IdProduto", "DesProduto");
            return View();
        }

        // POST: Pedidos/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                SqlConnection conexao = DataBaseAccess.ConexaoDB();
                SqlCommand scpAddPedido = new SqlCommand($@"INSERT INTO pedidos (id_cliente) VALUES ({Convert.ToInt32(collection[1])})", conexao);
                try
                {
                    conexao.Open();
                    scpAddPedido.ExecuteNonQuery();
                    scpAddPedido.Dispose();
                    SqlCommand scpRecIdPedido = new SqlCommand("SELECT TOP 1 pdd.id_pedido FROM pedidos pdd ORDER BY id_pedido DESC;", conexao);
                    int numPedido = Convert.ToInt32(scpRecIdPedido.ExecuteScalar().ToString());
                    scpRecIdPedido.Dispose();
                    SqlCommand scpAddItemPedido = new SqlCommand($@"INSERT INTO itens_pedido (id_pedido,id_produto,qtd_produto,vlr_unit) VALUES ({numPedido},{Convert.ToInt32(collection[2])},{Convert.ToInt32(collection[3])},{Convert.ToDecimal(collection[4])})", conexao);
                    scpAddItemPedido.BeginExecuteNonQuery();
                    scpAddItemPedido.Dispose();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int id)
        {
            PedidoModel pedido = new PedidoModel();
            SqlConnection conexao = DataBaseAccess.ConexaoDB();
            SqlCommand scpPedido = new SqlCommand($@"SELECT 
	                                                    pdd.id_pedido,
	                                                    clt.id_cliente,
	                                                    clt.nome_cliente
                                                    FROM 
	                                                    pedidos pdd
	                                                    INNER JOIN clientes clt 
		                                                    ON clt.id_cliente = pdd.id_cliente
                                                    WHERE
                                                        pdd.id_pedido = {id};", conexao);
            try
            {
                conexao.Open();
                SqlDataReader adrPedido = scpPedido.ExecuteReader();
                if (adrPedido.HasRows)
                {
                    while (adrPedido.Read())
                    {
                        pedido.IdPedido = adrPedido.GetInt32(0);
                        pedido.Cliente = new ClienteModel { IdCliente = adrPedido.GetInt32(1), NomeCliente = adrPedido.GetString(2) };
                        List<ProdutoPedidoModel> lstProdutos = new List<ProdutoPedidoModel>();
                        SqlCommand scpItensPedido = new SqlCommand($@"SELECT
	                                                                    pdt.id_produto,
	                                                                    pdt.des_produto,
	                                                                    ipdd.qtd_produto,
	                                                                    ipdd.vlr_unit
                                                                    FROM
	                                                                    itens_pedido ipdd
	                                                                    INNER JOIN produtos pdt
		                                                                    ON pdt.id_produto = ipdd.id_produto
                                                                    WHERE
                                                                        ipdd.id_pedido = {id};", conexao);
                        SqlDataReader sdrItensPedidos = scpItensPedido.ExecuteReader();
                        if (sdrItensPedidos.HasRows)
                        {
                            while (sdrItensPedidos.Read())
                            {
                                ProdutoPedidoModel produtoPedido = new ProdutoPedidoModel();
                                produtoPedido.Produto = new ProdutoModel { IdProduto = sdrItensPedidos.GetInt32(0), DesProduto = sdrItensPedidos.GetString(1) };
                                produtoPedido.QtdProduto = sdrItensPedidos.GetInt32(2);
                                produtoPedido.VlrUntProduto = sdrItensPedidos.GetDecimal(3);
                                lstProdutos.Add(produtoPedido);
                            }
                            pedido.LstProdutos = lstProdutos;
                            scpItensPedido.Dispose();
                        }
                    }
                    scpPedido.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexao.Dispose();
                conexao.Close();
            }
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                string[] idProdutos = collection[0].Split(',');
                string[] qtdProdutos = collection[1].Split(',');
                string[] vlrProdutos = collection[2].Split(',');
                for (int i = 0; i < idProdutos.Length; i++)
                {
                    SqlConnection conexao = DataBaseAccess.ConexaoDB();
                    SqlCommand scpAlteraItens = new SqlCommand($@"UPDATE itens_pedido
                                                                SET 
	                                                                qtd_produto = {qtdProdutos[i]},
	                                                                vlr_unit = {vlrProdutos[i]}
                                                                WHERE
	                                                                id_pedido = {id}
	                                                                AND id_produto = {idProdutos[i]};", conexao);
                    try
                    {
                        conexao.Open();
                        scpAlteraItens.ExecuteNonQuery();
                        scpAlteraItens.Dispose();                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        conexao.Dispose();
                        conexao.Close();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int id)
        {
            PedidoModel pedido = new PedidoModel();
            SqlConnection conexao = DataBaseAccess.ConexaoDB();
            SqlCommand scpPedido = new SqlCommand($@"SELECT 
	                                                    pdd.id_pedido,
	                                                    clt.id_cliente,
	                                                    clt.nome_cliente
                                                    FROM 
	                                                    pedidos pdd
	                                                    INNER JOIN clientes clt 
		                                                    ON clt.id_cliente = pdd.id_cliente
                                                    WHERE
                                                        pdd.id_pedido = {id};", conexao);
            try
            {
                conexao.Open();
                SqlDataReader sdrPedido = scpPedido.ExecuteReader();
                if (sdrPedido.HasRows)
                {
                    while (sdrPedido.Read())
                    {
                        pedido.IdPedido = sdrPedido.GetInt32(0);
                        pedido.Cliente = new ClienteModel { IdCliente = sdrPedido.GetInt32(1), NomeCliente = sdrPedido.GetString(2) };
                        List<ProdutoPedidoModel> lstProdutos = new List<ProdutoPedidoModel>();
                        SqlCommand scpItensPedido = new SqlCommand($@"SELECT
	                                                                    pdt.id_produto,
	                                                                    pdt.des_produto,
	                                                                    ipdd.qtd_produto,
	                                                                    ipdd.vlr_unit
                                                                    FROM
	                                                                    itens_pedido ipdd
	                                                                    INNER JOIN produtos pdt
		                                                                    ON pdt.id_produto = ipdd.id_produto
                                                                    WHERE
                                                                        ipdd.id_pedido = {id};", conexao);
                        SqlDataReader sdrItensPedido = scpItensPedido.ExecuteReader();
                        if (sdrItensPedido.HasRows)
                        {
                            while (sdrItensPedido.Read())
                            {
                                ProdutoPedidoModel produtoPedido = new ProdutoPedidoModel();
                                produtoPedido.Produto = new ProdutoModel { IdProduto = sdrItensPedido.GetInt32(0), DesProduto = sdrItensPedido.GetString(1) };
                                produtoPedido.QtdProduto = sdrItensPedido.GetInt32(2);
                                produtoPedido.VlrUntProduto = sdrItensPedido.GetDecimal(3);
                                lstProdutos.Add(produtoPedido);
                            }
                            pedido.LstProdutos = lstProdutos;
                            scpItensPedido.Dispose();
                        }
                    }
                    scpPedido.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexao.Dispose();
                conexao.Close();
            }
            return View(pedido);            
        }

        // POST: Pedidos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                PedidoModel pedido = new PedidoModel();
                SqlConnection conexao = DataBaseAccess.ConexaoDB();
                SqlCommand scpDeletaItens = new SqlCommand($@"DELETE FROM itens_pedido
                                                          WHERE
	                                                          id_pedido = {id};", conexao);
                SqlCommand scpDeletaPedido = new SqlCommand($@"DELETE FROM pedidos
                                                           WHERE
	                                                           id_pedido = {id};", conexao);
                try
                {
                    conexao.Open();
                    scpDeletaItens.ExecuteNonQuery();
                    scpDeletaItens.Dispose();
                    scpDeletaPedido.ExecuteNonQuery();
                    scpDeletaPedido.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conexao.Dispose();
                    conexao.Close();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
