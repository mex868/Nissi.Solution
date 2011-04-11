$(document).ready(function () {
    loadDate = function () {
        $(".dataEmissao").datepicker({ changeMonth: true, changeYear: true });
        $(".dataPicker").datepicker({ changeMonth: true, changeYear: true });
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
<<<<<<< HEAD
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

=======
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
    showItensInsumo = function () {
        $("#cadastraritensInsumo").animate({
            height: "toggle",
            opacity: "toggle"
        }, 500, function () {

        });
    }
<<<<<<< HEAD
=======

>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
});

