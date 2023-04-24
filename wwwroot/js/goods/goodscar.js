window.onload = function () {
    QueryTask();
}

function QueryTask() {

    let url = "/API";

    //向APICommand传递一个AdminIndex参数
    let parameter = {
        APICommand: "Admingoods",
    }

    //向页面渲染数据

    getAPICommandData(url, parameter).then(data => {
        let userList = data.Data;
        console.log(userList);
        let UserList = [];
        for (let i = 0; i < userList.length; i++) {
            if (userList[i].delstatus == '1') {
                UserList.push(userList[i]);
            }
        }

        console.log(UserList);

        let str = "";
        let strsum = "";
        let sum = [];
        let goodsum = 0;
        for (let i = 0; i < UserList.length; i++) {
            let num = UserList[i].num
            let price = UserList[i].price

            sum.push(num * price);

            str += `<div class="cart-item">`;
            str += `<div class="p-checkbox">`;
            str += `<input type="checkbox" name="" id="" class="j-checkbox">`;
            str += `</div>`;
            str += `<div class="p-goods">`;
            str += `<div class="p-img">`;
            str += `<img src="../../uploads/${UserList[i].goodid}.jpg" alt="">`;
            str += `</div>`;
            str += `<div class="p-msg"><h4>${UserList[i].goodname}</h4></div>`;
            str += `<div class="p-msg">${UserList[i].note}</div>`;
            str += `</div>`;
            str += `<div class="p-price">￥${UserList[i].price}</div>`;
            str += `<div class="p-num">`;
            str += `<div class="quantity-form">`;
            // str += ` <a href="javascript:;" class="decrement" onclick="decrement()">-</a>`;
            str += `<input type="text" class="itxt" id="itxt" value="${UserList[i].num}">`;
            str += `<button id="loginBut" class="button" onclick="goodsnum(this)" data-id="${UserList[i].goodid}">更改</button>`;
            str += `</div>`;
            str += `</div>`;
            str += `<div class="p-sum">￥${num * price}</div>`;
            str += `<div class="p-action"><button class="btn btn-primary" onclick="delgoods(${UserList[i].goodid})">删除</button></div>`;
            str += `</div>`;



        }
        // console.log(sum);
        for (let i = 0; i < sum.length; i++) {
            goodsum += sum[i];
        }
        // console.log(goodsum);
        for (let i = 0; i < 1; i++) {
            strsum += `<div class="amount-sum">已经选<em>${UserList.length}</em>件商品</div>`;
            strsum += `<div class="price-sum">总价： <em>￥${goodsum}</em></div>`;
            strsum += `<div class="btn-area" onclick="jump()">去支付</div>`;
        }

        document.getElementById("userList").innerHTML = str;
        document.getElementById("Listnum").innerHTML = strsum;



    })

}

//更改商品的数量
function goodsnum(res) {
    var number = parseInt(res.previousElementSibling.value);
    var id = res.getAttribute("data-id");
    // console.log(number, id);
    let url = "/API";

    let param = {
        APICommand: "goodsnum",
        ID: id,
        Num: number
    }
    getAPICommandData(url, param).then(data => {
        if (data.Data > 0) {
            alert("更改成功");
            QueryTask();
        } else {
            alert(data);
        }
    })
}

//删除商品
function delgoods(res) {
    let id = res;
    let url = "/API";

    let param = {
        APICommand: "goodsdel",
        ID: id
    }
    getAPICommandData(url, param).then(data => {
        if (data.Data > 0) {
            alert("删除成功");
            QueryTask();
        } else {
            alert(data);
        }
    })
}


//点击支付跳转页面
function jump(){
    location.href='Home?DashboardID=18'
}