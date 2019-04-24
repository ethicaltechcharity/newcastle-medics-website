var model: any;

$("#full-identity").hide();
$("#other-history").hide();
$("#other-identity").hide();

$("input[type=radio][name=PreRegistered]").on('change', function () {

    switch ($(this).val()) {
        case "true":
            $("#full-identity").hide();
            $("#other-identity").show();
            break;
        case "false":
            $("#other-identity").show();
            $("#full-identity").show();
            break;
    }
});

$("input[type=radio][name=PlayedBefore]").on('change', function () {

    switch ($(this).val()) {
        case "true":
            $("#history").show();
            break;
        case "false":
            $("#history").hide();
            break;
    }
});

$("input[type=radio][name=PlayedMedics]").on('change', function () {

    switch ($(this).val()) {
        case "true":
            $("#other-history").hide();
            $("#medics-history").show();
            break;
        case "false":
            $("#medics-history").hide();
            $("#other-history").show();
            break;
    }
});

$("input[type=radio][name=InterestedInUmpiring]").on('change', function () {

    switch ($(this).val()) {
        case "true":
            $("#umpiring-info").show();
            break;
        case "false":
            $("#umpiring-info").hide();
            break;
    }
});

$("input[type=radio][name=UmpireQualified]").on('change', function () {

    switch ($(this).val()) {
        case "true":
            $("#what-qualification").show();
            $("#umpire-number").show();
            break;
        case "false":
            $("#what-qualification").hide();
            $("#umpire-number").hide();
            break;
    }
});

$("input[type=radio][name=KnowsUmpireNumber]").on('change', function () {

    switch ($(this).val()) {
        case "true":
            $("#umpire-number-input").show();
            break;
        case "false":
            $("#umpire-number-input").hide();
            break;
    }
});

if (model.PreRegistered) {
    $("#full-identity").hide();
    $("#other-identity").show();
} else {
    $("#other-identity").show();
    $("#full-identity").show();
}

if (model.PlayedBefore) {
    $("#history").show();
} else {
    $("#history").hide();
}

if (model.PlayedMedics) {
    $("#other-history").hide();
    $("#medics-history").show();
} else {
    $("#medics-history").hide();
    $("#other-history").show();
}

if (model.InterestedInUmpiring) {
    $("#umpiring-info").show();
} else {
    $("#umpiring-info").hide();
}

if (model.UmpireQualified) {
    $("#what-qualification").show();
    $("#umpire-number").show();
} else {
    $("#what-qualification").hide();
    $("#umpire-number").hide();
}

if (model.KnowsUmpireNumber) {
    $("#umpire-number-input").show();
} else {
    $("#umpire-number-input").hide();
}