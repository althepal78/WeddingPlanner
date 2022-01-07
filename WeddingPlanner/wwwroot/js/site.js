$.validator.addMethod('pastdate', function (value, element, params) {
    return value === value.PastDate();
});

$.validator.unobtrusive.adapters.addBool("pastdate");