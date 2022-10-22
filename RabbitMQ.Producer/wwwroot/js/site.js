function CreateModal(id, title, content, modalSize) {
    var size = "";
    switch (modalSize) {
        case "sm":
            size = "modal-sm";
            break;
        case "md":
            size = "modal-md";
            break;
        case "lg":
            size = "modal-lg";
            break;
        case "xl":
            size = "modal-xl";
            break;
        default:
            size = "";
    }

    var template = '<div class="modal fade" id="' + id + '" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="' + id + 'Label" aria-hidden="true">' +
        '<div class="modal-dialog modal-dialog-centered ' + size + '">' +
        '<div class="modal-content">' +
        '<div class="modal-header">' +
        '<h5 class="modal-title" id="' + id + '">' + title + '</h5>' +
        '<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>' +
        '</div>' +
        '<div class="modal-body">' +
        content +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>';

    if (document.getElementById(id) != null) {
        document.getElementById(id).remove();
    }
    $("body").append(template);
    var modal = new bootstrap.Modal(document.getElementById(id), {
        keyboard: false,
        focus: true,
        backdrop: 'static'
    });

    modal.show();
    document.getElementById(id).addEventListener('hidden.bs.modal',
        function (e) {
            document.getElementById(id).remove();
        });

    return modal;
}

function makeXHR(url, type, data) {
    return jQuery.ajax({
        url: url,
        cache: false,
        type: type,
        data: data,
        headers: {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        }
    });
};

function hideModal(id) {
    const modal = bootstrap.Modal.getInstance(document.getElementById(id));
    modal.hide();
}