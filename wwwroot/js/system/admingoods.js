
window.onload = function () {
    QueryTask();
}

//查看商品信息
function QueryTask() {
    let url = "/API";

    //向APICommand传递一个AdminIndex参数
    let parameter = {
        APICommand: "Admingoods",
    }

    //向页面渲染数据
    getAPICommandData(url, parameter).then(data => {
        console.log(data);
        let userList = data.Data;
        let str = "";
        for (let i = 0; i < userList.length; i++) {
            str += `<tr>`;
            str += `<td>${userList[i].goodid}</td>`;
            str += `<td>${userList[i].goodname}</td>`;
            str += `<td>${userList[i].price}</td>`;
            str += `<td>${userList[i].note}</td>`;
            str += `<td>${userList[i].istj}</td>`;
            str += `<td>${userList[i].salestatus}</td>`;
            str += `<td><button type="button" onclick="to_evaluate(${userList[i].goodid})")">查看评价</button></td>`;
            str += `<td><button type="button" onclick="to_changedata(${userList[i].goodid})")">修改</button></td>`;
            str += `<td><button type="button" onclick="del(${userList[i].goodid})">删除</button></td>`;
            str += `<tr>`;

        }
        document.getElementById("userList").innerHTML = str;
    })

}


//删除商品信息
function del(id) {
    const confirm = window.confirm("确定删除这个Task吗？");
    console.log(id);
    if (confirm) {
        let url = "/API";

        let param = {
            APICommand: "Admindelgoods",
            ID: id
        }
        getAPICommandData(url, param).then(data => {
            //window.location.reload();

            if (data.Data > 0) {
                alert("删除成功");
                QueryTask();
            } else {
                alert(data);
            }
        })
    }
}


//修改用户
//编辑跳转页面
function to_changedata(res) {
    let right = document.getElementsByClassName('right')[0];
    let wapper = document.getElementsByClassName('wapper')[0];
    let message = document.getElementsByClassName('message')[0];
    right.style.display = 'none';
    message.style.display = 'none'
    wapper.style.display = 'block'

    let id = res;

    let url = "/API";

    //向APICommand传递一个AdminIndex参数
    let parameter = {
        APICommand: "findmessage",
        ID: id
    }

    //向页面渲染数据
    getAPICommandData(url, parameter).then(data => {
        console.log(data);
        let userList = data.Data;
        let str = "";
        for (let i = 0; i < userList.length; i++) {
            str += `<h1>修改商品信息</h1>`;
            str += `<div class="top">`;
            str += `<ul>`;
            str += `<li><i>商品编号：</i> <input type="text" placeholder="请输入商品编号" value = "${userList[i].goodid}" id="goodid"></li>`;
            str += `<li><i>商品名称：</i> <input type="text" placeholder="请输入商品名称" value = "${userList[i].goodname}" id="goodname"></li>`;
            str += `<li><i>商品价格：</i> <input type="text" placeholder="请输入商品价格" value = "${userList[i].price}" id="price"></li>`;
            str += `<li><i>商品描述：</i> <input type="text" placeholder="请输入商品描述" value = "${userList[i].note}" id="note"></li>`;
            str += `<li><i>是否推荐：</i> <input type="text" placeholder="是否推荐" value = "${userList[i].istj}" id="istj"></li>`;
            str += `<li><i>销售状态：</i> <input type="text" placeholder="请输入销售状态" value = "${userList[i].salestatus}" id="salestatus"></li>`;
            str += `</ul>`;
            str += `</div>`;
            str += `<div class="submit-w3l">`;
            str += `<button id="loginBut" class="button" onclick="goodsChange(${userList[i].goodid})">提交修改信息</button>`;
            str += `</div>`;

        }
        document.getElementById("wapper").innerHTML = str;

    })

}


// 点击按钮，修改商品的信息
function goodsChange(res) {
    let Goodsid = document.getElementById("goodid").value;
    let Goodsname = document.getElementById("goodname").value;
    let Goodsprice = document.getElementById("price").value;
    let Goodsnote = document.getElementById("note").value;
    let Goodsistj = document.getElementById("istj").value;
    let Goodssalestatus = document.getElementById("salestatus").value;
    let GoodsUserID = res;
    console.log(res);


    let url = "/API";

    let parameter = {
        APICommand: "goodsChange",
        goodid: Goodsid,
        goodname: Goodsname,
        price: Goodsprice,
        note: Goodsnote,
        istj: Goodsistj,
        salestatus: Goodssalestatus,
        goodsUserID: GoodsUserID
    }

    getAPICommandData(url, parameter).then(data => {
        console.log(data);
        if (data.Data > 0) {
            alert("修改成功！");
            location.href="Home?DashboardID=11"
        } else {
            alert("修改失败！");
        }
    })

}


//点击查看评价
function to_evaluate(res) {
    let right = document.getElementsByClassName('right')[0];
    let wapper = document.getElementsByClassName('wapper')[0];
    let message = document.getElementsByClassName('message')[0];
    right.style.display = 'none';
    message.style.display = 'block'
    wapper.style.display = 'none'

    let id = res;

    let url = "/API";

    //向APICommand传递一个AdminIndex参数
    let parameter = {
        APICommand: "findevalu",
        ID: id
    }

    //向页面渲染数据
    getAPICommandData(url, parameter).then(data => {
        console.log(data);
        let userList = data.Data;
        let str = "";
        for (let i = 0; i < userList.length; i++) {
            str += `<tr>`;
            str += `<td>${userList[i].goodid}</td>`;
            str += `<td>${userList[i].jb}</td>`;
            str += `<td>${userList[i].msg}</td>`;
            str += `<td>${userList[i].hf}</td>`;
            str += `<td>${userList[i].evaluname}</td>`;
            str += `<td>${userList[i].savetime}</td>`;
            str += `<tr>`;

        }
        document.getElementById("message").innerHTML = str;

    })

}

function updatainfo(){
    let right = document.getElementsByClassName('right')[0];
    let wapper = document.getElementsByClassName('wapper')[0];
    let message = document.getElementsByClassName('message')[0];
    let updata = document.getElementsByClassName('updata')[0];
    right.style.display = 'none';
    message.style.display = 'none'
    wapper.style.display = 'none'
    updata.style.display = 'block'
}


// 添加商品
function updata() {
    let Upgoodname = document.getElementById("upgoodname").value;
    let Upprice = document.getElementById("upprice").value;
    let Upnote = document.getElementById("upnote").value;
    let Upistj = document.getElementById("upistj").value;
    let Upsalestatus = document.getElementById("upsalestatus").value;
    let Uptprice = document.getElementById("uptprice").value;
    let Uptype = document.getElementById("uptype").value;

    console.log(Upgoodname,Upprice,Upnote,Upistj,Upsalestatus,Uptprice,Uptype);

    if (Upgoodname === '') {
        alert("请输入商品信息")
     } else {

        let url = "/API";

        let parameter = {
            APICommand: "goodsupdata",
            upgoodname: Upgoodname,
            upprice: Upprice,
            upnote: Upnote,
            upistj: Upistj,
            upsalestatus: Upsalestatus,
            uptprice: Uptprice,
            uptype: Uptype,
        }

        getAPICommandData(url, parameter).then(data => {
            console.log(data);
            if (data.Data > 0) {
                alert("添加成功！");
                location.href="Home?DashboardID=11"
            } else {
                alert("添加失败！");
            }
        })
    }


}