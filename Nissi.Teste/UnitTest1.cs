using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nissi.Model;
using Nissi.Util;
using System.Diagnostics;

namespace Nissi.Teste
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {

        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            ProdutoVO produto = new ProdutoVO();
            produto.ICMS = new List<ICMSVO>();

            produto.ICMS.Add(new ICMSVO()
            {
                Aliquota = 1,
                AliquotaST = 2,
                CodBaseCalculo = 3,
                CodBaseCalculoICMSST = 5,
                CodOrigem = 3,
                CodProduto = 4,
                CodTipoTributacao = "00",
                PercentualMargemST = 12,
                PercentualReducao = 25,
                PercentualReducaoST = 22
            });

            produto.ICMS.Add(new ICMSVO()
            {
                Aliquota = 1,
                AliquotaST = 2,
                CodBaseCalculo = 3,
                CodBaseCalculoICMSST = 5,
                CodOrigem = 3,
                CodProduto = 4,
                CodTipoTributacao = "00",
                PercentualMargemST = 12,
                PercentualReducao = 25,
                PercentualReducaoST = 22
            });

            
            
        }
    }
}
