function excluirRow(cpf) {
    $(".row[id='row_" + cpf + "'").remove()
};

$(document).ready(function () {

    $("#btnBeneficiarios").click(function () {
        var textoCorpo = '<div class="row">' +
            '               <div class="col-md-5">' +
            '                   <div class="form-group">' +
            '                       <label for="Cpf">CPF:</label>' +
            '                       <input required="required" type="text" class="form-control" id="cpfBeneficiario" name="cpfBeneficiario" placeholder="Ex.: 708.777.458-52" maxlength="13" data-mask="000.000.000-00"/>' +
            '                   </div>' +
            '               </div >' +
            '               <div class="col-md-5">' +
            '                   <div class="form-group" > ' +
            '                       <label for="Nome">Nome:</label>' +
            '                       <input required="required" type="text" class="form-control" id="nomeBeneficiario" name="nomeBeneficiario" placeholder="Ex.: João" maxlength="50">' +
            '                   </div>' +
            '               </div >' +
            '               <div class="col-md-2" style="padding-top: 28px !important">' +
            '                   <button type="button" id="btnIncluir" class="btn btn-sm btn-success">Incluir</button>' +
            '               </div >' +
            '           </div >' +
            '           <div class="row">' +
            '               <div class="col-md-4" style="padding-left: 20px !important"><label> CPF</label></div>' +
            '               <div class="col-md-8"><label> Nome</label></div>' +
            '           </div>' +
            '           <hr style="margin-top: 5px !important; margin-bottom: 5px !important;">' +
            '           <span></span>';


        ModalDialog("Beneficiários", textoCorpo);
        
        $('#cpfBeneficiario').mask("999.999.999-99");
        $('#cpfBeneficiario').attr("data-mask", "000.000.000-00");

        //$('.excluir').on("click", function (e) {
        //    e.preventDefault();
        //    var btnExcluir = $(this);
        //    var rowDelete = btnExcluir.attr("cpf");
        //    alert(rowDelete);
        //});

        $('#btnIncluir').click(function (e) {
            e.preventDefault();

            var $cpfBeneficiario = $('#cpfBeneficiario');

            $.ajax(
                {
                    type: 'GET',
                    url: urlGet,
                    data: "cpf=" + $cpfBeneficiario.val(),
                    success: function (data) {
                        if (data == "OK") {
                            insertNewRow();
                        } else {
                            ModalDialog("Beneficiário", data);
                        }
                    },
                    error:
                        function (r) {
                            debugger;
                            if (r.status == 400)
                                ModalDialog("Ocorreu um erro", r.responseJSON);
                            else if (r.status == 500)
                                ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                        }
                });

            function insertNewRow() {
                var $nomeBeneficiario = $('#nomeBeneficiario');
                var $span = $('span');
                var func = "onclick=excluirRow('" + $cpfBeneficiario.val() + "')";
                var idRow = "id=row_" + $cpfBeneficiario.val();
                var newRow = '<div class="row" ' + idRow + ' style="margin-top: 10px !important;">' +
                    '           <div class="col-md-4" style="padding-left: 20px !important">' + $cpfBeneficiario.val() + '</div>' +
                    '           <div class="col-md-4" style="padding-left: 20px !important">' + $nomeBeneficiario.val() + '</div>' +
                    '           <div class="col-md-4">' +
                    '               <button type="button" class="btn btn-sm btn-primary">Alterar</button>&nbsp;' +
                    '               <button type="button" cpf="' + $cpfBeneficiario.val() + '" class="btn btn-sm btn-primary excluir" ' + func + '>Excluir</button>' +
                    '           </div > ' +
                    '         </div>' +
                    '         <span id="span-"' + $cpfBeneficiario.val() + '"></span>';

                $span.last().after(newRow);
            }
        });

    });

  
    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "CPF": $(this).find("#Cpf").val()
            },
            error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success:
            function (r) {
                ModalDialog("Sucesso!", r)
                $("#formCadastro")[0].reset();
            }
        });
    })
    
})

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}
