
$('#aEdit').on('click', function (e) {
    //$(this).show();
    e.preventDefault();
    var btn = $(this),
        theme = btn.text();

    $.jAlert({
        'title': 'Edit',
        'content': 'Edit Done.',
        'theme': theme,
        'closeOnClick': true
    });
    return false;
});

$('#aCreate').on('click', function (e) {
    //$(this).show();
    e.preventDefault();
    var btn = $(this),
        theme = btn.text();

    $.jAlert({
        'title': 'Creat',
        'content': 'Create Done.',
        'theme': theme,
        'closeOnClick': true
    });
    return false;
});

$('#aError').on('click', function (e) {
    //$(this).show();
    e.preventDefault();
    var btn = $(this),
        theme = btn.text();

    $.jAlert({
        'title': 'Error Alert',
        'content': 'Action Error.',
        'theme': theme,
        'closeOnClick': true
    });
    return false;
});

function SaveData(status, datas, categoryName) {
    if (datas != null) {
        var isUpdate = status == "Update" ? true : false;
    }
    callUrl = "/api/web/" + categoryName + (isUpdate ? "/" + datas.id : "");
    $.ajax({
        type: isUpdate ? "PUT" : "POST",
        url: callUrl,
        data: datas
    }).done(function (res) {
        if (status == "Create") {
            console.log('create action');
        }
        if (isUpdate) {
            $('#aEdit').trigger('click');
        } else {
            $('#aCreate').trigger('click');
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.log('Status: ' + jqXHR.status + ', Status Text: ' + jqXHR.textStatus + ', ResponseText' + jqXHR.responseText);
        $('#aError').trigger('click');

        //$('#btn-loading').hide();
        //captchaObj.ReloadImage();

    });
};

function _uuid() {
    var d = Date.now();
    if (typeof performance !== 'undefined' && typeof performance.now === 'function') {
        d += performance.now(); //use high-precision timer if available
    }
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
}

function convertToStatus(status) {
    var outputStatus = "";
    switch (status) {
        case 1:
            outputStatus = "Enable";
            break;
        case 0:
            outputStatus = "Disable";
            break;
        default:
            outputStatus = "Disable";
    }
    return outputStatus;
}
