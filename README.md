# Projeto base para bancoframework.console
Meu primeiro projeto PDI(bancoframework.console) - c#

# Script Bd

SELECT * FROM Clientes WHERE Id = @ClientId


INSERT INTO Clientes (Id,Nome, Cpf, Saldo) VALUES (@Id, @Nome, @Cpf, @Saldo)


UPDATE Clientes SET Saldo = @NovoSaldo WHERE Id = @ClientId
