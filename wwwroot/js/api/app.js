$(document).ready(function () {
    $('#search-city').click(function (event) {
        event.preventDefault(); // Prevent the default form submission

        var city = $('#city').val();
        console.log('Searching for city:', city);
        $.ajax({
            url: '/App/SearchCity',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(city), // Send the city as a JSON string
            success: function (data) {
                console.log('Success:', data);
                $('#results').html(data); // Update the results section with new data
            },
            error: function (xhr, status, error) {
                console.error('Error:', error);
            }
        });
    });
});
