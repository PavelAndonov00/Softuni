function attachEvents() {
    const baseUrl = 'https://baas.kinvey.com/';
    const auth = {Authorization: 'Basic ' + btoa('guest' + ':' + 'pass')};
    let venueInfoDiv = $('#venue-info');

    $('#getVenues').click(function () {
        venueInfoDiv.empty();
        venueInfoDiv.append($('<p class="load">').text('Loading...'));
        let date = $('#venueDate').val();
        $.post({
            url: baseUrl + 'rpc/kid_BJ_Ke8hZg/custom/calendar?query=' + date,
            headers: auth
        }).then(function (ids) {
            let ul = $('<ul>');
            for (let id of ids) {
                $.get({
                    url: baseUrl + 'appdata/kid_BJ_Ke8hZg/venues/' + id,
                    headers: auth
                }).then(function (venue) {
                    let template = `<div class="venue" id="${venue._id}">
                                  <span class="venue-name"><input class="info" type="button" value="More info">${venue.name}</span>
                                  <div class="venue-details" style="display: none;">
                                    <table>
                                      <tr><th>Ticket Price</th><th>Quantity</th><th></th></tr>
                                      <tr>
                                        <td class="venue-price">${venue.price} lv</td>
                                        <td><select class="quantity">
                                          <option value="1">1</option>
                                          <option value="2">2</option>
                                          <option value="3">3</option>
                                          <option value="4">4</option>
                                          <option value="5">5</option>
                                        </select></td>
                                        <td><input class="purchase" type="button" value="Purchase"></td>
                                      </tr>
                                    </table>
                                    <span class="head">Venue description:</span>
                                    <p class="description">${venue.description}</p>
                                    <p class="description">Starting time: ${venue.startingHour}</p>
                                  </div>
                                </div>
                                `;


                    ul.append($('<li>').append(template));

                    let parent = $('#' + id);
                    parent.find('.info').click(function () {
                        parent.find('.venue-details').toggle();
                    });

                    parent.find('.purchase').click(function () {
                        venueInfoDiv.empty();

                        let quantity = parent.find('.quantity option:selected').val();

                        let purchasePage = `<span class="head">Confirm purchase</span>
                        <div class="purchase-info" >
                            <span>${venue.name}</span>
                            <span>${quantity} x ${venue.price}</span>
                        <span>Total: ${quantity * venue.price} lv</span>
                        <input type="button" value="Confirm">
                            </div>`;

                        venueInfoDiv.append(purchasePage);

                        $('.purchase-info').find('input').click(function () {
                            venueInfoDiv.empty();
                            venueInfoDiv.append($('<p class="load">').text('Loading...'));
                            $.post({
                                url: baseUrl + 'rpc/kid_BJ_Ke8hZg/custom/purchase?venue=' + venue._id + '&qty=' + quantity,
                                headers: auth
                            }).then(function (confirmPage) {
                                venueInfoDiv.empty();
                                venueInfoDiv.append('You may print this page as your ticket');
                                venueInfoDiv.append(confirmPage.html);
                            }).catch(handleError)
                        })
                    });

                }).catch(handleError);
            }

            venueInfoDiv.find('p').remove();
            ul.appendTo($('#venue-info'));
        })
    });


    function handleError(Error) {
        console.log(Error);
    }
}