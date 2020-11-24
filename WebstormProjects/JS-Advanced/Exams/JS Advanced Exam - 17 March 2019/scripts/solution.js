function solve() {
    const jsFundamentalsPrice = 170;
    const jsAdvancedPrice = 180;
    const jsApplicationsPrice = 190;
    const jsWebPrice = 490;
    const jsFundamentalsName = "JS-Fundamentals";
    const jsAdvancedName = "JS-Advanced";
    const jsApplicationsName = "JS-Applications";
    const jsWebName = "JS-Web";
    const htmlAndCssName = "HTML and CSS";

    $(".courseFoot").click(function () {
            let courses = [];

            $("input[type='checkbox']:checked").each(function () {
                courses.push(this.value);
            });

            let educationForm = $("input[type='radio']:checked").val();

            let fundamentalsSelected;
            let advancedSelected;
            let applicationsSelected;
            let webSelected;
            for (let course of courses) {
                if (course === "js-fundamentals") {
                    fundamentalsSelected = true;
                } else if (course === "js-advanced") {
                    advancedSelected = true;
                } else if (course === "js-applications") {
                    applicationsSelected = true;
                } else if (course === "js-web") {
                    webSelected = true;
                }
            }
            let allAreSelected = fundamentalsSelected && advancedSelected && applicationsSelected && webSelected;

            $("#myCourses ul").empty();

            let totalPrice = 0;
            if (fundamentalsSelected) {
                $("#myCourses ul").append(`<li>${jsFundamentalsName}</li>`);

                totalPrice += jsFundamentalsPrice;
            }

            if (advancedSelected) {
                $("#myCourses ul").append(`<li>${jsAdvancedName}</li>`);

                totalPrice += jsAdvancedPrice;
            }

            if (applicationsSelected) {
                $("#myCourses ul").append(`<li>${jsApplicationsName}</li>`);

                totalPrice += jsApplicationsPrice;
            }

            if (fundamentalsSelected && advancedSelected) {
                totalPrice -= parseInt('1.8');
            }

            if (fundamentalsSelected && advancedSelected && applicationsSelected) {
                totalPrice -= parseInt('32.4')
            }

            if (webSelected) {
                $("#myCourses ul").append(`<li>${jsWebName}</li>`);

                totalPrice += jsWebPrice;
            }

            if (allAreSelected) {
                $("#myCourses ul").append(`<li>${htmlAndCssName}</li>`);
            }

            if (educationForm === "online") {
                totalPrice *= 0.94;
            }

            let formattedPrice = parseInt(totalPrice).toFixed(2);
            $("#myCourses .courseFoot p").text(`Cost: ${formattedPrice} BGN`);
        }
    )
}

solve();