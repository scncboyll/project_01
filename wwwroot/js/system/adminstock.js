
window.onload = function () {
    QueryTask();
}

// 获取库存信息
function QueryTask() {
    let url = "/API";

    //向APICommand传递一个AdminIndex参数
    let parameter = {
        APICommand: "Adminstock",
    }

    //向页面渲染数据
    getAPICommandData(url, parameter).then(data => {
        console.log(data);
        let userList = data.Data;
        let str = "";
        for (let i = 0; i < userList.length; i++) {
            str += `<tr>`;
            str += `<td>${userList[i].goodsname}</td>`;
            str += `<td>${userList[i].type}</td>`;
            str += `<td>${userList[i].enternum}</td>`;
            str += `<td>${userList[i].happennum}</td>`;
            str += `<td>${userList[i].savetime}</td>`;
            str += `<td><button type="button" onclick="to_changestock(${userList[i].id})")">修改</button></td>`;
            str += `<tr>`;

        }
        document.getElementById("userList").innerHTML = str;



    })

}


//点击修改，修改库存信息
function to_changestock(res) {
    // console.log(res);
    let right = document.getElementsByClassName('right')[0];
    let wapper = document.getElementsByClassName('wapper')[0];
    right.style.display = 'none';
    wapper.style.display = 'block'

    let id = res;

    let url = "/API";

    //向APICommand传递一个AdminIndex参数
    let parameter = {
        APICommand: "findstock",
        ID: id
    }
    //向页面渲染数据
    getAPICommandData(url, parameter).then(data => {
        console.log(data);
        let userList = data.Data;
        let str = "";
        for (let i = 0; i < userList.length; i++) {
            str += `<h1>修改库存信息</h1>`;
            str += `<div class="top">`;
            str += `<ul>`;
            str += `<li><i>商品ID：</i> <input type="text" placeholder="请输入用户名" value = "${userList[i].gid}" id="stockgid"></li>`;
            str += `<li><i>商品名称：</i> <input type="text" placeholder="请输入用户名" value = "${userList[i].goodsname}" id="stockgoodsname"></li>`;
            str += `<li><i>商品类别：</i> <input type="text" placeholder="请输入密码" value = "${userList[i].type}" id="stocktype"></li>`;
            str += `<li><i>入库数量：</i> <input type="text" placeholder="请输入电子邮箱" value = "${userList[i].enternum}" id="stockenternum"></li>`;
            str += `<li><i>出库数量：</i> <input type="text" placeholder="请输入真实姓名" value = "${userList[i].happennum}" id="stockhappennum"></li>`;
            str += `</ul>`;
            str += `</div>`;
            str += `<div class="submit-w3l">`;
            str += `<button id="loginBut" class="button" onclick="stockChange(${userList[i].id})">提交修改信息</button>`;
            str += `</div>`;

        }
        document.getElementById("wapper").innerHTML = str;

    })

}

//提交修改信息
function stockChange(res) {
    let Stockgid = document.getElementById("stockgid").value;
    let Stockgoodsname = document.getElementById("stockgoodsname").value;
    let Stocktype = document.getElementById("stocktype").value;
    let Stockenternum = document.getElementById("stockenternum").value;
    let Stockhappennum = document.getElementById("stockhappennum").value;
    let StockUserID = res;
    console.log(res);


    let url = "/API";

    let parameter = {
        APICommand: "stockChange",
        stockgid: Stockgid,
        stockgoodsname: Stockgoodsname,
        stocktype: Stocktype,
        stockenternum: Stockenternum,
        stockhappennum: Stockhappennum,
        stockUserID: StockUserID
    }

    getAPICommandData(url, parameter).then(data => {
        console.log(data);
        if (data.Data > 0) {
            alert("修改成功！");
            location.href="Home?DashboardID=13"
        } else {
            alert("修改失败！");
        }
    })
}

