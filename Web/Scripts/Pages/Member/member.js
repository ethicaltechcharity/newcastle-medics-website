$("#filter-box").hide();
$("#toggle-filter").on("click", function () {
    $("#filter-box").toggle();
});
var form = $("#filter-form");
$(form).submit(function (event) {
    event.preventDefault();
});
$(form).children("#apply-filter").on("click", function () {
    var formData = $(form).serialize();
    $.ajax({
        type: 'POST',
        url: $(form).attr('action'),
        data: formData
    }).done(function (response) {
        $("#results").replaceWith(response);
    });
});
//# sourceMappingURL=member.js.map