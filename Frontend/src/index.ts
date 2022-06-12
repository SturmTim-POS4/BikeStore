import {
    CategoryDto,
    OrderDto,
    StoreDto,
    ProductDto,
    PostOrderDto,
    StoreApi,
    CategoryApi,
    ProductApi,
    OrderItemDto,
    OrderApi,
    OrderItemApi
} from "./swagger/index"

const baseUrl = "http://localhost:5000";
const storeAPI = new StoreApi(baseUrl)
const categoryAPI = new CategoryApi(baseUrl)
const productAPI = new ProductApi(baseUrl)
const orderAPI = new OrderApi(baseUrl)
const orderItemAPI = new OrderItemApi(baseUrl)

let orderItems: OrderItemDto[] = []
let unFilteredProducts: ProductDto[]

function activateFilter() {
  $('#txtFilter')
      .on("change",() => {
          fillWithFilter()
  })
}
function setChanged() {
    $('#categories').on("change", () =>{
        getUnfilteredProducts(Number($("#stores option:selected").val()))
    })
    $('#stores').on("change", () =>{
        console.log("changed")
        $('#storeName').text($("#stores option:selected").text())
        fillCategories(Number($("#stores option:selected").val()))
    })
}
function clearButton() {
    $("#btnClearBasket").on("click", () => {
        $("#tblBasketBody").empty();
        orderItems = []
        refreshSum()
    })

}
function saveButton(){
    $('#btnSaveBasket').on("click", () => {
        const date = new Date();
        date.setDate(date.getDate()+7)
        orderAPI.orderPost(new class implements PostOrderDto {
            customerId = 1;
            orderDate = new Date().toJSON();
            orderStatus = 3;
            requiredDate = date.toJSON();
            staffId = 1;
            storeId = Number($('#stores option:selected').val());
        }).then(x => x.body)
            .then(order => orderItems.forEach(item => {
                item.itemId = orderItems.indexOf(item);
                item.orderId = order.orderId;
                orderItemAPI.orderItemPost(item)
            }))
            .then(() => {
            $("#tblBasketBody").empty();
            orderItems = [];
            refreshSum()
        })
    })
}

$(_ => {
    setChanged();
    activateFilter();
    clearButton();
    saveButton();
    fillStores();
});

function fillStores() {
  storeAPI.storeGet()
      .then(x => x.body)
      .then((stores: StoreDto[]) => {
          stores.forEach(store => {
              const option = $("<option>").html(store.storeName).val(store.storeId)
              $('#stores').append(option)
          })
      })
      .then(() => {
          $('#storeName').text($("#stores option:selected").text())
          fillCategories(Number($('#stores option:selected').val()))
      })
}

function fillCategories(storeId: number) {
    categoryAPI.categoryStoreIdGet(storeId)
        .then(x => x.body)
        .then((categories: CategoryDto[]) => {
            $('#categories').empty()
            categories.forEach(category => {
                const option = $("<option>").html(category.categoryName).val(category.categoryId)
                $('#categories').append(option)
            })
        })
        .then(() => getUnfilteredProducts(storeId))
}

function getUnfilteredProducts(storeId: number) {
    unFilteredProducts = []
    productAPI.productStoreIdGet(storeId)
        .then(x => x.body)
        .then((products: ProductDto[]) => {
            products.filter(x => x.categoryId == $('#categories').val())
                .forEach(product => {
                    unFilteredProducts.push(product)
                })
        })
        .then(fillWithFilter)
        
}

function fillWithFilter() {
    $('#tblProductsBody').empty()
    unFilteredProducts.filter(x => x.productName.startsWith(String($('#txtFilter').val())))
        .forEach(product => {
            const row = $("<tr>")
            row.append($("<td>").html(" "))
            const img = $("<img>").attr("src", "img/plus.png").attr("height", 30)
                .on("click", () => {
                    if(Number($(`#${product.productId}-amount`).val()) > 0){
                        addProductToOrder(product)
                    }
                })
            row.append($("<td>").append(img))

            row.append($("<td>").append($("<input>")
                .attr("type", "number")
                .attr("id", `${product.productId}-amount`)))

            row.append($("<td>").html(product.productName))
            row.append($("<td>").html(product.brandName))
            row.append($("<td>").html(product.categoryName))
            row.append($("<td>").html(`${product.modelYear}`))
            row.append($("<td>").html(`${product.listPrice}`))
            $('#tblProductsBody').append(row)
        })
}

function addProductToOrder(product: ProductDto) {
    const newOrderItem: OrderItemDto = new class implements OrderItemDto {
        itemId = orderItems.length + 1;
        listPrice = product.listPrice;
        orderId = 0;
        productId = product.productId;
        quantity = Number($(`#${product.productId}-amount`).val());
    }
    orderItems.push(newOrderItem);
    const row = $("<tr>").attr("id", `${newOrderItem.itemId}-OrderItem`)
    const img = $("<img>").attr("src", "img/trash-bin.png").attr("height", 30)
        .on("click", () => {
            orderItems.splice(orderItems.indexOf(newOrderItem), 1);
            $(`#${newOrderItem.itemId}-OrderItem`).remove()
        })
    row.append($("<td>").append(img))
    row.append($("<td>").html(`${newOrderItem.quantity}`))
    row.append($("<td>").html(product.productName))
    row.append($("<td>").html(product.brandName))
    row.append($("<td>").html(product.categoryName))
    row.append($("<td>").html(`${product.modelYear}`))
    row.append($("<td>").html(`${(newOrderItem.quantity ?? 0) * (newOrderItem.listPrice ?? 0)}`))
    $('#tblBasketBody').append(row)
    refreshSum()
}

function refreshSum() {
    let sum = 0;
    orderItems.forEach(x => {
        sum += (x.listPrice ?? 0) * (x.quantity ?? 0)
    })
    $('#basketSum').text(sum)
}





