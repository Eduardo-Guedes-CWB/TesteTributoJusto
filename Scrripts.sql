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
	id_produto integer,
	qtd_produto integer,
	vlr_unit decimal,
	FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente),
	FOREIGN KEY (id_produto) REFERENCES produtos (id_produto)
);

INSERT INTO clientes (nome_cliente)
VALUES 
('Cliente01'),
('Cliente02')