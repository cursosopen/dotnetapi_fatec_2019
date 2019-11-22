using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Store.Model.Infrastucture.Sql;
using Store.Model.Entities;

namespace Store.Model.Infrastucture.DataAcess
{
    public class CategoriaDao : BaseDao<Categoria>
    {
        public CategoriaDao() { }
        public CategoriaDao(Connection connection) : base(connection) { }

        public override List<Categoria> CastToObject(SqlDataReader Reader)
        {
            List<Categoria> Categorias = new List<Categoria>();
            while (Reader.Read())
            {
                Categoria categoria = new Categoria();
                categoria.Id = Convert.ToInt32(Reader["ID"]);
                categoria.Descricao = Convert.ToString(Reader["DESCRICAO"]);
                Categorias.Add(categoria);
            }
            return Categorias;
        }

        public List<Categoria> Select()
        {
            this.SqlBase();
            using (var Reader = this.ExecuteReader())
            {
                return this.CastToObject(Reader);
            }
        }

        protected override void SqlBase()
        {
            this.Sql.Append(" SELECT * FROM TB_CATEGORIA ");
        }
    }
}
