$(document).ready(function () {
    $(".search-user-box").select2({
        ajax: {
            url: location.protocol + "//" + location.host + "/Player/Interested/SearchByLastName",
            dataType: "json"
        },
        minimumInputLength: 2,
        width: "resolve"
    });

    $("input[type=radio][name=PreRegistered]").on('change', function() {

        switch ($(this).val()) {
            case "true":
                $(".create-user").hide();
                $(".search-user").show();
                break;
            case "false":
                $(".search-user").hide();
                $(".create-user").show();
                break;
        }
    });
    
    $(".create-user").hide();
});