CREATE TABLE clientes (
    id_cliente integer PRIMARY KEY IDENTITY (1, 1),
    nome_cliente VARCHAR (50) NOT NULL
);

CREATE TABLE produtos (
    id_produto integer PRIMARY KEY IDENTITY (1, 1),
    des_produto VARCHAR (50) NOT NULL
);

CREATE TABLE pedidos (
    id_pedido integer PRIMARY KEY IDENTITY (1, 1),
    id_cliente integer,	
	FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente)	
);

CREATE TABLE itens_pedido (
    id_item_pedido integer PRIMARY KEY IDENTITY (1, 1),
	id_pedido integer ,
    id_produto integer,
	qtd_produto integer,
	vlr_unit decimal,
	FOREIGN KEY (id_pedido) REFERENCES pedidos (id_pedido),
	FOREIGN KEY (id_produto) REFERENCES produtos (id_produto)
);



INSERT INTO clientes (nome_cliente)
VALUES 
('Cliente 01'),
('Cliente 02'),
('Cliente 03'),
('Cliente 04'),
('Cliente 05'),
('Cliente 06'),
('Cliente 07'),
('Cliente 08'),
('Cliente 09'),
('Cliente 10');

INSERT INTO produtos(des_produto)
VALUES 
('Produto 01'),
('Produto 02'),
('Produto 03'),
('Produto 04'),
('Produto 05'),
('Produto 06'),
('Produto 07'),
('Produto 08'),
('Produto 09'),
('Produto 10');

