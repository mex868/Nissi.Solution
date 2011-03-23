$(document).ready(function () {
    loadDate = function () {
        $(".dataEmissao").datepicker({ changeMonth: true, changeYear: true });
        $(".dataSaida").datepicker({ changeMonth: true, changeYear: true });
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
        if ($("#ctl00_cphPrincipal_hdfTipoAcaoItemPedidoCompraInsumo").val() != "Editar") {
            $("#cadastraritensInsumo").hide();
        }
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
    loadDate();
    loadTabs();
    hiddenItens();
    showItensInsumo = function () {
        $("#cadastraritensInsumo").animate({
            height: "toggle",
            opacity: "toggle"
        }, 500, function () {

        });
    }

    limparitens = function () {
        $(".textNovo").val("");
        $("#ctl00_cphPrincipal_hdfTipoAcaoItemPedidoCompra").val("Incluir");
    }
});  