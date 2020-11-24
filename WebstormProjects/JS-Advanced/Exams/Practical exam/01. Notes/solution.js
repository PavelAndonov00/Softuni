function addSticker() {
        let title = $('.title');
        let text = $('.content');

        let titleVal = title.val();
        let textVal = text.val();
    console.log(textVal);
    console.log(textVal);
    if(titleVal && textVal) {
            let li = $('<li class="note-content">');
            let a = $('<a class="button">').text("x").on("click", function () {
                li.remove();
            });
            let h2 = $("<h2>").text(titleVal);
            let p = $("<p>").text(textVal);
            let hr = $("<hr>");

            li.append([a, h2, hr, p]);
            $('#sticker-list').append(li);

            title.val("");
            text.val("");
        }
}