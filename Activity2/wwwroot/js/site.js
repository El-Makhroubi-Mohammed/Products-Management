$(function () {
    console.log("document ready");
    $(document).on("click", ".edit-product-button", function () {
        console.log("You just clicked button number " + $(this).val());

        // store the product id number
        var productID = $(this).val();
        $.ajax({
            type: 'json',
            data: {
                "id": productID
            },
            url: "/products/ShowOneProductJSON",
            success: function (data) {
                console.log(data)

                // fill in the input fields in the modal
                $("#modal-input-id").val(data.id);
                $("#modal-input-name").val(data.name);
                $("#modal-input-price").val(data.price);
                $("#modal-input-description").val(data.description);
            }
        })

    });
});

$("#save-button").click(function () {

    // get the values from the input fields and create a json object to submit to the controller.
    var product = {
        "Id": $("#modal-input-id").val(),
        "Name": $("#modal-input-name").val(),
        "Price": $("#modal-input-price").val(),
        "Description": $("#modal-input-description").val()
    };

    console.log("Saved...");
    console.log(product);

    // save the updated product record in the database using the controller
    $.ajax({
        type: 'json',
        data: product,
        url: "/products/ProcessEditReturnPartial",
        success: function (data) {
            console.log(data);
            $("#card-number-" + product.Id).html(data).hide().fadeIn(2000);
        }
    })
});
