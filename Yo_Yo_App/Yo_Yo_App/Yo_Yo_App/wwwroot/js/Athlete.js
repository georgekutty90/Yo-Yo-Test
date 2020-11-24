fnWarnAthlete = (id) => {
    $.ajax({
        type: "POST",
        async: false,
        url: "/Athlete/UpdateAthleteWarnStatus",
        data: { id: id },
        success: function (data) {
            $("#inpwrn-" + id).prop("disabled", true);
            $("#inpwrn-" + id).val("Warned");
            document.getElementById("warningToster").innerHTML = "Athlete warned!";
            $('#warningToster').hide().fadeIn(800).delay(3000).fadeOut(800);
        },
        error: function (data) {
            document.getElementById("errorTosetr").innerHTML = "Something went wrong!."
            $('#errorTosetr').hide().fadeIn(800).delay(3000).fadeOut(800);
        },
    });
};

fnStopAthlete = (id) => {
    let result = $("#hdnPreviousLevel").val() + "-" + $("#hdnPreviousShuttle").val();
    $.ajax({
        type: "POST",
        async: false,
        url: "/Athlete/UpdateAthleteStopStatus",
        data: { id: id, testResult: result },
        success: function (data) {
            $("#inpwrn-" + id).hide();
            $("#inpstop-" + id).hide();
            $("#" + id).val(data.testResult);
            $("#spnDdl-" + id).show();
            $("#hdnStatus").val(data.status);
            document.getElementById("errorTosetr").innerHTML = `Athlete terminated from Yo Yo Test.Resut is ${result}`
            $('#errorTosetr').hide().fadeIn(800).delay(3000).fadeOut(800);
        },
        error: function (data) {
            document.getElementById("errorTosetr").innerHTML = "Something went wrong!."
            $('#errorTosetr').hide().fadeIn(800).delay(3000).fadeOut(800);
        },
    });
};

fnLoadAthlete = () => {
    $.get("/Athlete/GetAllAthletes", {}, function (content) {
        $("#dvAthleteContentContainer").html(content);
    });
};

fnAthleteCompleteResult = () => {
    $.get(
        "/Athlete/GetAllAthletesCompleteResult",
        {
            shuttle: $("#hdnCurrentShuttle").val(),
            level: $("#hdnCurrentLevel").val(),
        },
        function (content) {
            $("#dvAthleteContentContainer").html(content);
        }
    );
};

fnGetFitnessRatingData = () => {
    $.get(
        "/FitnessTest/GetFitnessRatingTestData",
        {
            level: $("#hdnCurrentLevel").val(),
            shuttle: $("#hdnCurrentShuttle").val(),
            time: $("#hdnTime").val(),
            distance: $("#hdnTotalDistance").val(),
        },
        function (content) {
            $("#dynamicContentContainer").html(content);
        }
    );
};

progressBarStart = () => {
    let duration = parseInt($('#hdnNextLevelStartTime').val()) * 1000; // it should finish in x seconds !
    $("#box").stop().css("width", 0).animate({
        width: 200
    }, {
        duration: duration,
        progress: function (promise, progress, ms) {
        }
    });
}
