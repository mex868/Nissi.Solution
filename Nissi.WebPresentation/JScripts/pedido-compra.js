$(document).ready(function () {

    $.maxZIndex = $.fn.maxZIndex = function (opt) {
        /// <summary>
        /// Returns the max zOrder in the document (no parameter)
        /// Sets max zOrder by passing a non-zero number
        /// which gets added to the highest zOrder.
        /// </summary>    
        /// <param name="opt" type="object">
        /// inc: increment value, 
        /// group: selector for zIndex elements to find max for
        /// </param>
        /// <returns type="jQuery" />
        var def = { inc: 900000, group: "*" };
        $.extend(def, opt);
        var zmax = 0;
        $(def.group).each(function () {
            var cur = parseInt($(this).css('z-index'));
            zmax = cur > zmax ? cur : zmax;
        });
        if (!this.jquery)
            return zmax;

        return this.each(function () {
            zmax += def.inc;
            $(this).css("z-index", zmax);
        });
    }

    loadDate = function () {
        $(".dataEmissao").datepicker({ changeMonth: true, changeYear: true, beforeShow: function () { $('#ui-datepicker-div').maxZIndex(); } });
        $(".dataPicker").datepicker({ changeMonth: true, changeYear: true, beforeShow: function () { $('#ui-datepicker-div').maxZIndex(); } });
    }

    loadTabs = function () {
        $('#tabs').tabs({ ajaxOptions: { cache: false },
            select: function (event, ui) {
                if (ui.index == 0) {
                    $('#ctl00_cphPrincipal_hdfTipoPedido').val(0);
                }
                else {
                    $('#ctl00_cphPrincipal_hdfTipoPedido').val(1);
                }
            }
        });

        $('#tabs').tabs("select", $('#ctl00_cphPrincipal_hdfTipoPedido').val() == "0" ? 0 : 1);
    }

    hiddenItens = function () {
        if ($("#ctl00_cphPrincipal_hdfTipoAcaoItemPedidoCompra").val() != "Editar") {
            $("#cadastraritens").hide();
        }
        if ($("#ctl00_cphPrincipal_hdfTipoAcaoItemPedidoCompraInsumo").val() != "Editar") {
            $("#cadastraritensInsumo").hide();
        }
    }

    limparitens = function () {
        $(".textNovo").val("");
        $("#ctl00_cphPrincipal_hdfTipoAcaoItemPedidoCompra").val("Incluir");
    }

    loadDate();
    loadTabs();
    hiddenItens();

    showItens = function () {
        $("#cadastraritens").animate({
            height: "toggle",
            opacity: "toggle"
        }, 500, function () {

        });
    }

    getSituacao = function (nota) {
        var retorno = "";
        if (nota >= 8 && nota <= 10) retorno = "Material aprovado";
        else
            if (nota >= 5 && nota <= 7) retorno = "Não conformidade em certificados/laudo";
            else
                if (nota >= 2 && nota <= 4) retorno = "Não conformidade sem devolução do material";
                else
                    if (nota <= 1) retorno = "Não conformidade com devolução da material";
        $(".situacao").val(retorno);
    }

    showItensInsumo = function () {
        $("#cadastraritensInsumo").animate({
            height: "toggle",
            opacity: "toggle"
        }, 500, function () {

        });
    }
});

