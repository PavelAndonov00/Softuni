function summary(selector) {
    $(selector).click(actionFunc);

    function actionFunc() {
        // Get parent and strongTags
        let contentParent = $("#content").parent();
        let strongTags =  $("#content strong").text();

        // Create summary
        let summary = $("<div>");
        summary.attr("id", "summary");
        summary.append($("<h2>").text("Summary"));

        // Create paragraph and append it to summary
        let summaryPar = $("<p>");
        summaryPar.text(strongTags);
        summary.append(summaryPar);
        contentParent.append(summary);
    }
}

window.onload = function () {
    summary("#generate");
};