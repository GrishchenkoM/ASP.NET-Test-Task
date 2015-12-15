function slideToggleDiv(divId) {
    if (arguments.length > 1) {
        $(divId).slideToggle(arguments[2]);
    } else {
        $(divId).slideToggle(1000);
    }
}