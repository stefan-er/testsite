function ChangeMainPicture (){
    var mainPicture = $("#imgMainPicture");
    mainPicture[0].src = this.src;
}

function UpdateColors(event) {
    $("#ProductSize").empty();
    $.ajax({
        type: 'POST',
        url: event.data.urlSizes,
        dataType: 'json',
        data: { productID: event.data.productID, colorHexCode: $("#ProductColor").val() },
        success: function (sizes) {
            $.each(sizes, function (i, size) {
                $("#ProductSize").append('<option value="' + size.Value + '">' +
                     size.Text + '</option>');
            });

            UpdateSizes(event);
            SetProductColorBackgrounds();
        },
        error: function (ex) {
            alert('Failed to retrieve sizes.' + ex);
        }
    });
    return false;
}

function UpdateSizes(event) {
    $("#ProductQuantity").empty();
    $.ajax({
        type: 'POST',
        url: event.data.urlQuantites,
        dataType: 'json',
        data: { productID: event.data.productID, colorHexCode: $("#ProductColor").val(),
            sizeID: $("#ProductSize").val() },
        success: function (result) {
            $.each(result.Quantities, function (i, quantity) {
                $("#ProductQuantity").append('<option value="'
                    + quantity.Value + '">' + quantity.Text + '</option>');
            });

            $("#PCSQuantityID").val(result.PCSQuantityID)
        },
        error: function (ex) {
            alert('Failed to retrieve quantities.' + ex);
        }
    });
    return false;
}

function SetProductColorBackgrounds() {
    var selectedColor = $("#ProductColor option:selected").val();
    $("#ProductColor").css("background-color", selectedColor);

    $("#ProductColor option").each(function(i){
        var that = $(this)
        var color = that.val();
        that.css("background-color", color);
    });
}