using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Nissi.Model
{
   [Serializable()]
   [XmlRoot(ElementName = "Produto")]
   public class ProdutoVO: NissiBaseVO
   {
       #region Variaveis
       private int? _codProduto;
       private string _descricao;
       private string _codigo;
       private UnidadeVO _unidade;
       private string _ncm;


       private List<ICMSVO> _icms;

       #endregion

       #region Propriedades

       public int? CodProduto
       {
           get { return _codProduto; }
           set { _codProduto = value; }
       }

       public string Descricao
       {
           get { return _descricao; }
           set { _descricao = value; }
       }

       public string Codigo
       {
           get { return _codigo; }
           set { _codigo = value; }
       }

       public UnidadeVO Unidade
       {
           get
           {
               if (_unidade == null)
                   _unidade = new UnidadeVO();
               return _unidade; 
           }
           set { _unidade = value; }
       }

       public string NCM
       {
           get { return _ncm; }
           set { _ncm = value; }
       }

       [XmlElement(ElementName="ProdutoICMS")]
       public List<ICMSVO> ICMS
       {
           get
           {
               if (_icms == null)
                   _icms = new List<ICMSVO>();
               return _icms;
           }
           set { _icms = value; }
       }

       #endregion
   }

   /*public class ClassificacaoFiscal
   {
       public ClassificacaoFiscal(string codigoNCM, string descricaoNCM)
       {
           Codigo = codigoNCM;
           Descricao = descricaoNCM;
       }

       public string Codigo { get; private set; }
       public string Descricao { get; private set; }

       public static List<ClassificacaoFiscal> GetListaClassificacaoFiscal()
       {
           List<ClassificacaoFiscal> listaClassificacaoFiscal = new List<ClassificacaoFiscal>();
           listaClassificacaoFiscal.Add(new ClassificacaoFiscal("73170020", "A - 73170020"));
           listaClassificacaoFiscal.Add(new ClassificacaoFiscal("73182400", "B - 73182400"));
           listaClassificacaoFiscal.Add(new ClassificacaoFiscal("73202090", "C - 73202090"));
           listaClassificacaoFiscal.Add(new ClassificacaoFiscal("73209000", "D - 73209000"));
           listaClassificacaoFiscal.Add(new ClassificacaoFiscal("73181500", "E - 73181500"));
           listaClassificacaoFiscal.Add(new ClassificacaoFiscal("72230000", "F - 72230000"));
           return listaClassificacaoFiscal;
       }
   }
    */
}
