$(document).ready(function () {
    GetCreateStockTable();
    GetCreateMutualTable();
    GetAllPortFolioDetails();
    GetStock();
});


function GetStock(){
    $("#stockView").show();
    $("#getMutualFundsbtn").show();
    $("#getStocktxt").show();
    $("#mutualfundView").hide();
    $("#getMutualtxt").hide();
    $("#getStockbtn").hide();
}

function GetMutualFunds() {
    $("#mutualfundView").show();
    $("#getStockbtn").show();
    $("#getMutualtxt").show();
    $("#stockView").hide();
    $("#getStocktxt").hide();
    $("#getMutualFundsbtn").hide();
}

$(".close").click(function () { });

function GetCreateStockTable() {
    $.ajax({
        type: "GET",
        url: "/PortFolio/GetAllStocks",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: "true",
        cache: "false",
        success: function (data) {
            console.log(data);
            $.each(data.data, function (i, v) {
                $('#tbl_stockListing').find('tbody')
                    .append('<tr>').append('<td width="25%">' + v.id + '</td>')
                    .append('<td width="25%" >' + v.stockName + '</td>')
                    .append('<td width="25%">' + v.stockPrice + '</td>')
                    .append('<td width="25%"><span class="view-icon" style="display:flex"><button type="button" data-toggle="modal" data-target="#exampleModal" onclick="AddCustomerStock(' + v.id + ')" class="btn btn-primary" style="background-color: #07A287;height: 20px;width: 30px;display:flex;align-items: center;justify-content: center;font-size: x-small;">Add</button><button type="button" data-toggle="modal" data-target="#exampleModal2" onclick="AddCustomerStock(' + v.id + ')" class="btn btn-danger" style="height: 20px;width: 40px;display:flex;align-items: center;justify-content: center;font-size: x-small;">Remove</button></span></td>')
                    .append('<tr>');
            });
        }
    });

    
}

function AddCustomerStock(id)
{
    $("#stockIdValue").val(id);
    
}

function AddCustomerMutual(id) {
    $("#mutualIdValue").val(id);

}

function AddStocks()
{
    debugger;
    console.log("User : "+$("#user").val());
    console.log("StockId : "+$("#stockIdValue").val());
    console.log("StockIdQuantity : " +$("#stockIdQuantity").val());

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: "/PortFolio/AddCustomerStocks",
        data: { CustomerId: $("#user").val(), StocksId: $("#stockIdValue").val(), StocksQuantity: $("#stockIdQuantity").val() },
        success: function (data) {
            console.log(data);
            $('#tbl_PortFolio1Listing tbody').empty();
            $('#tbl_PortFolio2Listing tbody').empty();
            GetAllPortFolioDetails();
        }
    });
}

function AddMutual() {
    debugger;
    console.log("User : " + $("#user").val());
    console.log("MutualFundId : " + $("#mutualIdValue").val());
    console.log("MutualFundQuantity : " + $("#mutualIdQuantity").val());

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: "/PortFolio/AddCustomerMutualFunds",
        data: { CustomerId: $("#user").val(), MutualFundId: $("#mutualIdValue").val(), MutualFundQuantity: $("#mutualIdQuantity").val() },
        success: function (data) {
            console.log(data);
            $('#tbl_PortFolio1Listing tbody').empty();
            $('#tbl_PortFolio2Listing tbody').empty();
            GetAllPortFolioDetails();
        }
    });
}

function RemoveStocks() {
    debugger;
    console.log("User : " + $("#user").val());
    console.log("StockId : " + $("#stockIdValue").val());
    console.log("StockIdQuantity : " + $("#stockremoveIdQuantity").val());

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: "/PortFolio/RemoveCustomerStocks",
        data: { CustomerId: $("#user").val(), StocksId: $("#stockIdValue").val(), StocksQuantity: $("#stockremoveIdQuantity").val() },
        success: function (data) {
            console.log(data);
            $('#tbl_PortFolio1Listing tbody').empty();
            $('#tbl_PortFolio2Listing tbody').empty();
            GetAllPortFolioDetails();
        }
    });
}

function RemoveMutual() {
    debugger;
    console.log("User : " + $("#user").val());
    console.log("MutualFundId : " + $("#mutualIdValue").val());
    console.log("MutualFundQuantity : " + $("#mutualremoveIdQuantity").val());

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: "/PortFolio/AddCustomerMutualFunds",
        data: { CustomerId: $("#user").val(), MutualFundId: $("#mutualIdValue").val(), MutualFundQuantity: $("#mutualremoveIdQuantity").val() },
        success: function (data) {
            console.log(data);
            $('#tbl_PortFolio1Listing tbody').empty();
            $('#tbl_PortFolio2Listing tbody').empty();
            GetAllPortFolioDetails();
        }
    });
}

function GetCreateMutualTable() {
    $.ajax({
        type: "GET",
        url: "/PortFolio/GetAllMutualFunds",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: "true",
        cache: "false",
        success: function (data) {
            console.log(data);
            $.each(data.data, function (i, v) {

                $('#tbl_MutualListing').find('tbody')
                    .append('<tr>').append('<td width="25%">' + v.id + '</td>')
                    .append('<td width="25%" >' + v.mutualFundName + '</td>')
                    .append('<td width="25%">' + v.mutualFundPrice + '</td>')
                    .append('<td width="25%"><span class="view-icon" style="display:flex;"><button type="button" data-toggle="modal" data-target="#exampleModal1" onclick="AddCustomerMutual(' + v.id + ')" class="btn btn-primary" style="background-color: #07A287;height: 20px;width: 30px;display:flex;align-items: center;justify-content: center;font-size: x-small;">Add</button><button type="button" data-toggle="modal" data-target="#exampleModal3" onclick="AddCustomerMutual(' + v.id + ')" class="btn btn-danger" style="height: 20px;width: 40px;display:flex;align-items: center;justify-content: center;font-size: x-small;">Remove</button></span></td>')
                    .append('<tr>');
            });
        }
    });
}

function GetAllPortFolioDetails() {
    debugger;
    console.log($("#user").val());
    $.ajax({
        type: 'POST',
        url: "/PortFolio/GetAllPortFolioDetails",
        data: { CustomerId : $("#user").val() },
        success: function (data) {
            console.log(data);
            $.each(data.data.stockDetails, function (i, v) {
                if (v.stockQuantity > 0) {
                    $('#tbl_PortFolio1Listing').find('tbody')
                        //.append('<tr>').append('<td width="25%">' + data.data.portfolioId + '</td>')
                        .append('<td width="25%">' + v.stockName + '</td>')
                        .append('<td width="25%">' + v.stockPrice + '</td>')
                        .append('<td width="25%">' + v.stockQuantity + '</td>')
                        .append('<td width="25%">' + v.totalStockPrice + '</td>')
                        .append('<tr>');
                }
            });
            $.each(data.data.mutualFundsDetails, function (i, v) {
                if (v.quantity > 0) {
                    $('#tbl_PortFolio2Listing').find('tbody')
                        //.append('<tr>').append('<td width="25%">' + data.data.portfolioId + '</td>')
                        .append('<td width="25%">' + v.mutualFundName + '</td>')
                        .append('<td width="25%">' + v.mutualFundPrice + '</td>')
                        .append('<td width="25%">' + v.quantity + '</td>')
                        .append('<td width="25%">' + v.totalMutualFundPrice + '</td>')
                        .append('<tr>');
                }
            });
        }
    });

}