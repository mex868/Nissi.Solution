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
    uploadFileAjax = function () {
        //Function to upload file.
        new AjaxUpload('#uploadFile', {
            action: 'FileUploadHandler.axd',
            name: 'upload',
            onComplete: function (file) {
                $('<div><img src="../../imagens/exclusao_Canc.png" style="width:16px;height:16px" alt="Excluir arquivo" class="delete"/>' + file + '</div>').appendTo('#fileList');
                $('#uploadStatus').html("Arquivo Carregado.");
            },
            onSubmit: function (file, ext) {
                if (!(ext && /^(pdf)$/i.test(ext))) {
                    Ext.Msg.alert('Fomato Inválido.');
                    return false;
                }
                $('#uploadStatus').html("Carregando...");
            }

        });
        //Function to delete uploaded file in server.
        $('img').live("click", function () { $('#uploadStatus').html("Deleting"); ; deleteFile($(this)); });
    }

    function deleteFile(objfile) {
        $.ajax({ url: 'FileUploadHandler.axd?del=' + objfile.parent().text(), success: function () { objfile.parent().hide(); } });
        $('#uploadStatus').html("Excluído");
    }

    loadDate();
    loadTabs();
    hiddenItens();
    uploadFileAjax();

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