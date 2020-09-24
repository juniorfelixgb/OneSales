(function(DeleteDialog) {
    var methods = {
        "openModal": openModal,
        "deleteItem": deleteItem
    };

    var item_to_delete;

    function openModal(modalName, classOrId, sourceEvent, deletePath, eventClassOrId) {
        var textEvent;
        if (classOrId) {
            textEvent = `.${modalName}`;
        }
        else {
            textEvent = `#${modalName}`;
        }
        $(textEvent).click((e) => {
            item_to_delete = e.currentTarget.dataset.id;
            deleteItem(sourceEvent, deletePath, eventClassOrId);
        });
    }

    function deleteItem(sourceEvent, deletePath, eventClassOrId) {
        var textEvent;
        if (eventClassOrId) {
            textEvent = `.${sourceEvent}`;
        }
        else {
            textEvent = `#${sourceEvent}`;
        }
        $(textEvent).click((e) => {
            window.location.href = deletePath + item_to_delete;
        });
    }

    DeleteDialog.sc_deleteDialog = methods;
})(window);