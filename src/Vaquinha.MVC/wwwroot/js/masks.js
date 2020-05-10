$(document).ready(function () {
    $("#telefone").mask("(00) 99999-9999");
    $("#cep").mask("99999-999");
    $("#cvv").mask("9999");
    $("#validade").mask("99/9999");
    $("#cardNumber").mask("9999 9999 9999 9999");                         

    $("#valor").inputmask('currency', {
        "autoUnmask": true,
        radixPoint: ",",
        groupSeparator: ".",
        allowMinus: false,
        prefix: '',
        digits: 2,
        digitsOptional: false,
        rightAlign: true,
        unmaskAsNumber: true
    });
});
