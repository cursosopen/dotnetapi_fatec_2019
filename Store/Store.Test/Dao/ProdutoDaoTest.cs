﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Model.Infrastucture.DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Test.Dao
{
    [TestClass]
    public class ProdutoDaoTest
    {
        [TestMethod]
        public void Select()
        {
            using(var dao = new ProdutoDao())
            {
                var produtos = dao.Select();
            }
        }

        [TestMethod]
        public void SelectById()
        {
            using (var dao = new ProdutoDao())
            {
                var produto = dao.Select(1);
            }
        }

        [TestMethod]
        public void SelectByCategoria()
        {
            using (var dao = new ProdutoDao())
            {
                //var produtos = dao.SelectByCategoria(1);
            }
        }
    }
}
