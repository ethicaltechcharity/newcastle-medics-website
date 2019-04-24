var form = $("#filter-form");

$(form).submit(function (event) {

    event.preventDefault();
});

$(form).children("#apply-filter").on("click", function () {

    postFilter();
});

var postFilter = function () {
    var formData = $(form).serialize();

    $.ajax({
        type: 'POST',
        url: $(form).attr('action'),
        data: formData
    }).done(function (response) {

        $("#results").replaceWith(response);
        pageNumbers();
    });
}


var pageNumbers = function () {
    $(".page-link").on("click", function (event) {

        event.preventDefault();

        $.ajax({
            type: 'POST',
            url: (<any> event.currentTarget).href,
            data: $(form).serialize()
        }).done(function (response) {

            $("#results").replaceWith(response);
            pageNumbers();
        })
    });
};


pageNumbers();