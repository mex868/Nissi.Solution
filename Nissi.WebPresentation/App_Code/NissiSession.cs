using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nissi.Model;

    public static class NissiSession
    {
        private const string CachePdf = "PDF";
        
        public static byte[] ArquivoPdf
        {
            get
            {
                object o = HttpContext.Current.Cache[CachePdf];

                return o == null ? new byte[0] : (byte[]) o;
            }
            set
            {
                HttpContext.Current.Cache[CachePdf] = value;
            }
        }

        private const string SessionComposicaoMateriaPrima = "ComposicaoMateriaPrima";
        public static List<ComposicaoMateriaPrimaVO> ComposicaoMateriaPrima
        {
            get
            {
                object o = HttpContext.Current.Session[SessionComposicaoMateriaPrima];

                return o == null ? new List<ComposicaoMateriaPrimaVO>() : (List<ComposicaoMateriaPrimaVO>) o;
            }
            set
            {
                HttpContext.Current.Session[SessionComposicaoMateriaPrima] = value;
            }
            
        }

        private const string SessionResistenciaTracao = "ResistenciaTracao";
        public static ResistenciaTracaoVO ResistenciaTracao
        {
            get
            {
                object o = HttpContext.Current.Session[SessionResistenciaTracao];

                return o == null ? new ResistenciaTracaoVO() : (ResistenciaTracaoVO) o;
            }
            set
            {
                HttpContext.Current.Session[SessionResistenciaTracao] = value;
            }
            
        }

        private const string SessionlstItemEntradaEstoque = "lstItemEntradaEstoque";
        public static List<ItemEntradaEstoqueVO> ItemEntradaEstoques
        {
            get
            {
                object o = HttpContext.Current.Session[SessionlstItemEntradaEstoque];

                return o == null ? new List<ItemEntradaEstoqueVO>() : (List<ItemEntradaEstoqueVO>)o;
            }
            set
            {
                HttpContext.Current.Session[SessionlstItemEntradaEstoque] = value;
            }
        }

        private const string SessionlstItemEntradaEstoqueInsumo = "lstItemEntradaEstoqueInsumo";
        public static List<ItemEntradaEstoqueInsumoVO> ItemEntradaEstoqueInsumos
        {
            get
            {
                object o = HttpContext.Current.Session[SessionlstItemEntradaEstoqueInsumo];

                return o == null ? new List<ItemEntradaEstoqueInsumoVO>() : (List<ItemEntradaEstoqueInsumoVO>)o;
            }
            set
            {
                HttpContext.Current.Session[SessionlstItemEntradaEstoqueInsumo] = value;
            }
            
        }

    }