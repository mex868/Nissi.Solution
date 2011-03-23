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
    showItensInsumo = function () {
        $("#cadastraritensInsumo").animate({
            height: "toggle",
            opacity: "toggle"
        }, 500, function () {

        });
    }

});

