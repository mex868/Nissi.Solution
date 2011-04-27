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
    uploadFileAjax();

});

