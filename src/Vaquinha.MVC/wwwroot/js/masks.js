$(document).ready(function () {
    $("#telefone").mask("(00) 99999-9999");
    $("#cep").mask("99999-999");
    $("#cvv").mask("9999");
    $("#validade").mask("99/9999");
    $("#cardNumber").mask("9999 9999 9999 9999");                         
    
    $("#estado").inputmask({ mask: "[AA]" });

    $("#valor").inputmask('currency', {
        "autoUnmask": true,
        radixPoint: ",",
        groupSeparator: ".",
        allowMinus: false,
        prefix: '',
        digits: 0,
        digitsOptional: true,
        rightAlign: true,
        unmaskAsNumber: true
    });
});
