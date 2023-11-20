$(document).ready(function () {
    var tableData = [];

    //initial state of fields
    $("#ddProduct").prop("disabled", true);
    $("#divRate").css("display", "none");
    $("#dataDiv").css("display", "none");
    $("#txtRate").prop("disabled", true);
    var tempRate;


    // ---- On Change of Manufacturer DropDown
    $("#ddManufacturer").change(function () {
        var manufacturerId = $(this).val();
        $("#ddProduct").prop("disabled", false);
        $.ajax({
            type: "POST",
            url: "/PurchaseHistory/GetProducts",
            data: { selectedValue: manufacturerId },
            success: function (data) {
                $("#ddProduct").empty();
                $("#ddProduct").append('<option value="">Select Product</option>');
                $.each(data, function (index, item) {
                    $("#ddProduct").append('<option value="' + item.Id + '">' + item.Name + '</option>');
                });
            },
            error: function (error) {
                console.log(error);
            }
        });
    });



    //  ----On change of Product DropDown
    $("#ddProduct").change(function () {
        $("#divRate").css("display", "inline");
        var productId = $(this).val();
        $.ajax({
            type: "POST",
            url: "/PurchaseHistory/GetRate",
            data: { selectedValue: productId },
            success: function (result) {
                $("#txtRate").val(result[0].Amount)
                tempRate = result[0].Id;
                console.log(tempRate)
            },
            error: function (error) {
                console.log(error);
            }
        });
    });


    //  ---- On btn click og Generate Invoice Btn 
    $("#dataTable").on("click", "#btnDeleteRow", function () {
        //$(this).closest("tr").remove();
        console.log($(this).closest("tr"));
    });


    $("#btnGenerateInvoice").click(function () {
        //validate all input fields
        if (validateForm()) {

            //disable UserName,date field and change btn text to "Add to invoice"
            $("#ddUser").prop("disabled", true);
            $("#txtDate").prop("disabled", true);
            $("#btnGenerateInvoice").html("Add to Invoice")
            $("#dataDiv").css("display", "inline");

            //get values from input
            var manufacturer = $("#ddManufacturer option:selected").text();
            var product = $("#ddProduct option:selected").text();
            var rate = $("#txtRate").val();
            var date = $("#txtDate").val();
            var quantity = $("#txtQuantity").val();
            var total = parseFloat(rate) * parseInt(quantity);


            //reflect the stored values in table and labels
            //1. create a row
            var newRow = $('<tr>');
            newRow.append('<td>' + product + '</td>');
            newRow.append('<td>' + manufacturer + '</td>');
            newRow.append('<td>' + rate + '</td>');
            newRow.append('<td>' + quantity + '</td>');
            newRow.append('<td>' + total.toFixed(2) + '</td>');
            newRow.append('<td>' + ' <button type="button" id="btnEditRow" class="btn text-info">Edit</button>' +
                '<button type="button" id="btnDeleteRow" class="btn text-danger">Delete</button>'
                + '</td>');

            //2. append row to table body and show labels
            $("#lblInvoice").html(102);
            $("#lblUser").html($("#ddUser option:selected").text());
            $("lblDate").html($("#txtDate").val());
            $('#dataTable tbody').append(newRow);

            //update hidden field with table data
            var rowData = {
                InvoiceId: 103,
                UserId: $("#ddUser").val(),
                ManufacturerId: $("#ddManufacturer").val(),
                ProductId: $("#ddProduct").val(),
                RateId: tempRate,
                Date: $("#txtDate").val(),
                Quantity: $("#txtQuantity").val(),
            };
            tableData.push(rowData);
            $("#hdnTableData").val(JSON.stringify(tableData));


            //3. clear input fields
            $("#ddManufacturer").val('');
            $("#ddProduct").val('');
            $("#txtRate").val('');
            $("#divRate").css("display", "none");
            $("#txtQuantity").val(0);
        }
    })

    function validateForm() {

        $(".field-validation-error").remove();


        var isValid = true;

        // Validation for AspNetUserId
        var userId = $("#ddUser").val();
        if (!userId) {
            isValid = false;
            $("#ddUser").addClass("input-validation-error");
            $("#ddUser").after("<span class='field-validation-error'>Please select a user.</span>");
        }
        else {
            $("#ddUser").removeClass("input-validation-error");
        }

        // Validation for ManufacturerId
        var manufacturerId = $("#ddManufacturer").val();
        if (!manufacturerId) {
            isValid = false;
            $("#ddManufacturer").addClass("input-validation-error");
            $("#ddManufacturer").after('<span class="field-validation-error">Please select a manufacturer.</span>');
        }
        else {
            $("#ddManufacturer").removeClass("input-validation-error");
        }

        // Validation for ProductId
        var productId = $("#ddProduct").val();
        if (!productId) {
            isValid = false;
            $("#ddProduct").addClass("input-validation-error");
            $("#ddProduct").after("<span class='field-validation-error'>Please select a product.</span>");
        }
        else {
            $("#ddProduct").removeClass("input-validation-error");
        }

        // Validation for Rate
        var rate = $("#txtRate").val();
        if (!rate || isNaN(rate)) {
            isValid = false;
            $("#txtRate").addClass("input-validation-error");
            $("#txtRate").after("<span class='field-validation-error'>Please enter a valid rate.</span>");
        }
        else {
            $("#txtRate").removeClass("input-validation-error");
        }

        // Validation for Date
        var date = $("#txtDate").val();
        if (!date || !isValidDate(date)) {
            isValid = false;
            $("#txtDate").addClass("input-validation-error");
            $("#txtDate").after("<span class='field-validation-error'>Please enter a valid date (dd-MM-yyyy).</span>");
        } else {
            $("#txtDate").removeClass("input-validation-error");
        }

        // Validation for Quantity
        var quantity = $("#txtQuantity").val();
        if (!quantity || isNaN(quantity) || quantity <= 0 || quantity > 100) {
            isValid = false;
            $("#txtQuantity").addClass("input-validation-error");
            $("#txtQuantity").after("<span class='field-validation-error'>Please enter a valid quantity between 1 and 100.</span>");
        }
        else {
            $("#txtQuantity").removeClass("input-validation-error");
        }

        return isValid;
    }
    function isValidDate(dateString) {
        var regex = /^\d{2}-\d{2}-\d{4}$/;
        return dateString.match(regex);
    }
    
});