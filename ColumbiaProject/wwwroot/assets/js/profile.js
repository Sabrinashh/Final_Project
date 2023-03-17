let accountInfoBtn=document.getElementById("account_info_btn");
let ordersBtn=document.getElementById("orders_btn")

let accountInfo=document.getElementById("account_info");
let orders=document.getElementById("orders")

let textAccount=document.getElementById("text_account");
let textOrders=document.getElementById("text_orders")


orders.style.display="none";
accountInfoBtn.style.backgroundColor=" rgb(58, 58, 58)";
textAccount.style.color="white"

accountInfoBtn.addEventListener("click",()=>{
accountInfo.style.display="block";
orders.style.display="none";
accountInfoBtn.style.backgroundColor=" rgb(58, 58, 58)";
ordersBtn.style.backgroundColor="white";
textAccount.style.color="white";
textOrders.style.color="black";
})

ordersBtn.addEventListener("click",()=>{
    accountInfo.style.display="none";
    ordersBtn.style.backgroundColor=" rgb(58, 58, 58)";
    orders.style.display="block";
    accountInfoBtn.style.backgroundColor="white";
    textAccount.style.color="black";
    textOrders.style.color="white";
})
