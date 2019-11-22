using Store.Model.Entities;
using Store.Model.Infrastucture.Casts;
using Store.Model.Infrastucture.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Model.Infrastucture.DataAcess
{
    public class ClienteDao : BaseDao<Cliente>
    {
        public ClienteDao() { }
        public ClienteDao(Connection connection) : base(connection) { }

        public override List<Cliente> CastToObject(SqlDataReader Reader)
        {
            List<Cliente> Clientes = new List<Cliente>();
            while (Reader.Read())
            {
                Cliente cliente = new Cliente();
                cliente.Id = Convert.ToInt32(Reader["ID"]);
                cliente.Nome = Convert.ToString(Reader["NOME"]);
                cliente.Cpf = Convert.ToString(Reader["CPF"]);
                cliente.Email = Convert.ToString(Reader["EMAIL"]);
                cliente.Senha = Convert.ToString(Reader["SENHA"]);
                Clientes.Add(cliente);
            }
            return Clientes;
        }

        public Cliente Insert(Cliente cliente)
        {
            this.Sql.Append(" INSERT INTO TB_CLIENTE (NOME, CPF, EMAIL, SENHA) ");
            this.Sql.Append(" OUTPUT inserted.ID ");
            this.Sql.Append(" VALUES ( @NOME, @CPF, @EMAIL, @SENHA ) ");

            this.AddParameter("@NOME", cliente.Nome);
            this.AddParameter("@CPF", cliente.Cpf);
            this.AddParameter("@EMAIL", cliente.Email);
            this.AddParameter("@SENHA", cliente.Senha);

            cliente.Id = (int)this.ExecuteComand();

            return cliente;
        }

        public Cliente Update(Cliente cliente)
        {
            this.Sql.Append(" UPDATE TB_CLIENTE SET ");
            this.Sql.Append("        NOME = @NOME, ");
            this.Sql.Append("        CPF = @CPF, ");
            this.Sql.Append("        EMAIL = @EMAIL ");
            this.Sql.Append(" WHERE TB_CLIENTE.ID = @ID ");

            this.AddParameter("@NOME", cliente.Nome);
            this.AddParameter("@CPF", cliente.Cpf);
            this.AddParameter("@EMAIL", cliente.Email);
            this.AddParameter("@ID", cliente.Id);

            this.ExecuteComand();

            return cliente;
        }

        public Cliente UpdatePassword(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        protected override void SqlBase()
        {
            this.Sql.Append(" SELECT * FROM TB_CLIENTE ");
        }

        public List<Cliente> Select()
        {
            throw new NotImplementedException();
        }

        public Cliente Select(int id)
        {
            this.SqlBase();
            this.Sql.Append(" WHERE TB_CLIENTE.ID = @ID ");

            this.AddParameter("@ID", id);

            return ConverObject();
        }

        private Cliente ConverObject()
        {
            using (var DataReader = this.ExecuteReader())
                return this.CastToObject(DataReader).FirstOrDefault();
        }

        public Cliente Select(Cliente cliente)
        {
            this.SqlBase();

            this.Sql.Append(" WHERE TB_CLIENTE.EMAIL = @EMAIL AND ");
            this.Sql.Append("       TB_CLIENTE.SENHA = @SENHA ");

            this.AddParameter("@EMAIL", cliente.Email);
            this.AddParameter("@SENHA", cliente.Senha);

            using (var DataReader = this.ExecuteReader())
            {
                return this.CastToObject(DataReader).FirstOrDefault();
            }
        }
    }
}
